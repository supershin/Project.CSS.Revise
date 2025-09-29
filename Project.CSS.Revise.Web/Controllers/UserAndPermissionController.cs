using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.UserAndPermission;
using Project.CSS.Revise.Web.Service;
using static Project.CSS.Revise.Web.Models.Pages.UserAndPermission.UserAndPermissionModel;

namespace Project.CSS.Revise.Web.Controllers
{
    public class UserAndPermissionController : BaseController
    {

        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;

        public UserAndPermissionController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IUserAndPermissionService userAndPermissionService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
        }

        public IActionResult Index()
        {
            var listDepartments = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 9 });
            ViewBag.listDepartments = listDepartments;

            var listRoles = _masterService.GetlisDDl(new GetDDLModel { Act = "listRole" });
            ViewBag.listRoles = listRoles;

            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;

            return View();
        }


        [HttpPost]
        [Route("UserAndPermission/GetlistUser")]
        public JsonResult GetlistUser([FromForm] UserAndPermissionModel.FiltersGetlistUser filters)
        {
            filters ??= new UserAndPermissionModel.FiltersGetlistUser();
            var data = _userAndPermissionService.GetlistUser(filters);
            return Json(new { data });
        }


        [HttpPost]
        [Route("UserAndPermission/Create")]
        public IActionResult CreateUser([FromForm] UserAndPermissionModel.CreateUserRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string loginId = User.FindFirst("LoginID")?.Value;
            string decoded = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(decoded);

            var dup = _userAndPermissionService.CheckDuplicate(
                model.Email,
                model.UserID,
                model.FirstName, model.LastName,
                model.FirstName_Eng, model.LastName_Eng,
                excludeId: null);

            if (dup.HasAnyConflict)
            {
                var dupMessages = new List<string>();

                if (dup.EmailExists)
                    dupMessages.Add("Email (" + model.Email + ") ซ้ำ");
                if (dup.UserIdExists)
                    dupMessages.Add("UserID (" + model.UserID + ") ซ้ำ");
                if (dup.FullNameThExists)
                    dupMessages.Add("ชื่อ–นามสกุล (ไทย): " + model.FirstName + " " + model.LastName + " ซ้ำ");
                if (dup.FullNameEnExists)
                    dupMessages.Add("ชื่อ–นามสกุล (อังกฤษ): " + model.FirstName_Eng + " " + model.LastName_Eng + " ซ้ำ");

                return Json(new
                {
                    success = false,
                    message = "ข้อมูลซ้ำ: " + string.Join(" , ", dupMessages),
                    duplicate = dup
                });
            }

            var id = _userAndPermissionService.InsertUser(model, currentUserId);
            return Json(new { success = true, id });
        }


        [HttpPost]
        [Route("UserAndPermission/Update")]
        public IActionResult UpdateUser([FromForm] UserAndPermissionModel.UpdateUserRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string LoginID = User.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);
            int CurrentUserID = Commond.FormatExtension.Nulltoint(UserID);

            var dup = _userAndPermissionService.CheckDuplicate(
                    model.Email,
                    model.UserID,
                    model.FirstName, model.LastName,
                    model.FirstName_Eng, model.LastName_Eng,
                    excludeId: model.ID);

            if (dup.HasAnyConflict)
            {
                var dupMessages = new List<string>();

                if (dup.EmailExists)
                    dupMessages.Add("Email (" + model.Email + ") ซ้ำ");
                if (dup.UserIdExists)
                    dupMessages.Add("UserID (" + model.UserID + ") ซ้ำ");
                if (dup.FullNameThExists)
                    dupMessages.Add("ชื่อ–นามสกุล (ไทย): " + model.FirstName + " " + model.LastName + " ซ้ำ");
                if (dup.FullNameEnExists)
                    dupMessages.Add("ชื่อ–นามสกุล (อังกฤษ): " + model.FirstName_Eng + " " + model.LastName_Eng + " ซ้ำ");

                return Json(new
                {
                    success = false,
                    message = "ข้อมูลซ้ำ: " + string.Join(" , ", dupMessages),
                    duplicate = dup
                });
            }

            var ok = _userAndPermissionService.UpdateUser(model, CurrentUserID);
            return Json(new { success = ok });
        }


        [HttpPost]
        [Route("UserAndPermission/GetDetailsUser")]
        public JsonResult GetDetailsUser([FromForm] UserAndPermissionModel.FiltersGetlistUser filters)
        {
            try
            {
                var data = _userAndPermissionService.GetDetailsUser(filters);
                if (data == null)
                {
                    return Json(new { success = false, message = "ไม่พบผู้ใช้" });
                }

                return Json(new { success = true, data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาด", detail = ex.Message });
            }
        }


        [HttpPost]
        [Route("UserAndPermission/GetProjectsUserMapping")]
        public JsonResult GetProjectsUserMapping(int UserID)
        {
            try
            {
                var filters = new UserAndPermissionModel.FiltersGetlistUser();
                filters.L_UserID = Commond.FormatExtension.NullToString(UserID);
                var data = _userAndPermissionService.GetlistProjects(filters);
                if (data == null)
                {
                    return Json(new { success = false, message = "ไม่พบผู้ใช้" });
                }

                return Json(new { success = true, data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาด", detail = ex.Message });
            }
        }


        [HttpPost]
        [Route("UserAndPermission/IUDProjectUserMapping")]
        public IActionResult IUDProjectUserMapping([FromBody] UserAndPermissionModel.IUDProjectUserMapping model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string loginID = User.FindFirst("LoginID")?.Value;
            string userID = SecurityManager.DecodeFrom64(loginID);
            int currentUserID = Commond.FormatExtension.Nulltoint(userID);

            var ok = _userAndPermissionService.IUDProjectUserMapping(model, currentUserID);
            return Json(new { success = ok });
        }


        [HttpPost]
        [Route("UserAndPermission/GetListPermissionMatrix")]
        public JsonResult GetListPermissionMatrix()
        {
            var data = _userAndPermissionService.GetPermissionMatrix(10);
            return Json(new { data });
        }

        [HttpPost]
        [Route("UserAndPermission/SaveRolePermissions")]
        public JsonResult SaveRolePermissions([FromBody] UserAndPermissionModel.SaveRolePermissionRequest req)
        {
            if (req == null) return Json(new { success = false, message = "Invalid payload" });

            string loginId = User.FindFirst("LoginID")?.Value;
            string userId = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(userId);

            try
            {
                var ok = _userAndPermissionService.SaveRolePermissions(req, currentUserId);
                //var ok = true;
                return Json(new { success = ok });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Save failed", detail = ex.Message });
            }
        }

        [HttpPost]
        [Route("UserAndPermission/GetRolePermissions")]
        public JsonResult GetRolePermissions([FromBody] UserAndPermissionModel.GetRolePermissionRequest req)
        {
            if (req == null || req.DepartmentID <= 0 || req.RoleID <= 0)
                return Json(new { success = false, message = "Invalid Department/Role" });

            var data = _userAndPermissionService.GetPermissionMatrixFor(req.QCTypeID, req.DepartmentID, req.RoleID);
            return Json(new { success = true, data });
        }

        // Create
        [HttpPost]
        [Route("UserAndPermission/Department/Create")]
        public JsonResult DepartmentCreate([FromBody] DeptCreateRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Name))
                return Json(new { success = false, message = "ชื่อแผนกต้องไม่ว่าง" });

            string loginId = User.FindFirst("LoginID")?.Value;
            string userId = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(userId);

            try
            {
                var id = _userAndPermissionService.DepartmentCreate(req.Name.Trim(), currentUserId);
                return Json(new { success = true, id, name = req.Name.Trim() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "บันทึกไม่สำเร็จ", detail = ex.Message });
            }
        }

        // Update
        [HttpPost]
        [Route("UserAndPermission/Department/Update")]
        public JsonResult DepartmentUpdate([FromBody] DeptUpdateRequest req)
        {
            if (req == null || req.ID <= 0 || string.IsNullOrWhiteSpace(req.Name))
                return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

            string loginId = User.FindFirst("LoginID")?.Value;
            string userId = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(userId);

            try
            {
                var ok = _userAndPermissionService.DepartmentUpdate(req.ID, req.Name.Trim(), currentUserId);
                return Json(new { success = ok, name = req.Name.Trim() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "อัปเดตไม่สำเร็จ", detail = ex.Message });
            }
        }

        // Delete (soft)
        [HttpPost]
        [Route("UserAndPermission/Department/Delete")]
        public JsonResult DepartmentDelete([FromBody] DeptDeleteRequest req)
        {
            if (req == null || req.ID <= 0)
                return Json(new { success = false, message = "ข้อมูลไม่ถูกต้อง" });

            string loginId = User.FindFirst("LoginID")?.Value;
            string userId = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(userId);

            try
            {
                var result = _userAndPermissionService.DepartmentSoftDelete(req.ID, currentUserId);
                return Json(result); // { success, message? }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "ลบไม่สำเร็จ", detail = ex.Message });
            }
        }


        // Create
        [HttpPost]
        [Route("UserAndPermission/Role/Create")]
        public JsonResult RoleCreate([FromBody] RoleCreateRequest req)
        {
            if (string.IsNullOrWhiteSpace(req?.Name))
                return Json(new { success = false, message = "Role name is required" });

            string loginId = User.FindFirst("LoginID")?.Value;
            string userId = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(userId);

            try
            {
                var id = _userAndPermissionService.RoleCreate(req.Name.Trim(), currentUserId, qcTypeId: 10);
                return Json(new { success = true, id, name = req.Name.Trim() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Update
        [HttpPost]
        [Route("UserAndPermission/Role/Update")]
        public JsonResult RoleUpdate([FromBody] RoleUpdateRequest req)
        {
            if (req == null || req.ID <= 0 || string.IsNullOrWhiteSpace(req.Name))
                return Json(new { success = false, message = "Invalid request" });

            string loginId = User.FindFirst("LoginID")?.Value;
            string userId = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(userId);

            try
            {
                var ok = _userAndPermissionService.RoleUpdate(req.ID, req.Name.Trim(), currentUserId, qcTypeId: 10);
                return Json(new { success = ok, name = req.Name.Trim() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Delete (soft)
        [HttpPost]
        [Route("UserAndPermission/Role/Delete")]
        public JsonResult RoleDelete([FromBody] RoleDeleteRequest req)
        {
            if (req == null || req.ID <= 0)
                return Json(new { success = false, message = "Invalid request" });

            string loginId = User.FindFirst("LoginID")?.Value;
            string userId = SecurityManager.DecodeFrom64(loginId);
            int currentUserId = Commond.FormatExtension.Nulltoint(userId);

            try
            {
                var result = _userAndPermissionService.RoleSoftDelete(req.ID, currentUserId, qcTypeId: 10);
                return Json(result); // { success, message? }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
