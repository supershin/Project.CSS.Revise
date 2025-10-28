using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CSResponseController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly ICSResponseService _csResponseServic;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly MasterManagementConfigProject _configProject;
        public CSResponseController(IHttpContextAccessor httpContextAccessor
                                  , IMasterService masterService
                                  , ICSResponseService csResponseServic
                                  , MasterManagementConfigProject configProject
                                  , IUserAndPermissionService userAndPermissionService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _csResponseServic = csResponseServic;
            _configProject = configProject;
            _userAndPermissionService = userAndPermissionService;
        }

        public IActionResult Index()
        {
            int menuId = Constants.Menu.CsResponse;
            string? dep64 = User.FindFirst("DepartmentID")?.Value;
            string? rol64 = User.FindFirst("RoleID")?.Value;

            int departmentId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(dep64));
            int roleId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(rol64));

            var perms = _userAndPermissionService.GetPermissions(10, menuId, departmentId, roleId);
            if (!perms.View) return RedirectToAction("NoPermission", "Home");
            ViewBag.Permission = perms;

            var filter1 = new GetDDLModel
            {
                Act = "listAllCSUser"
            };
            var listAllCSUser = _masterService.GetlisDDl(filter1);
            ViewBag.listAllCSUser = listAllCSUser;

            var filter2 = new GetDDLModel
            {
                Act = "listDDlAllProject"
            };
            var listDDlAllProject = _masterService.GetlisDDl(filter2);
            ViewBag.listDDlAllProject = listDDlAllProject;

            var filter = new SPGetDataCSResponse.FilterData
            {
                Act = "GetListCSSummary",
                ProjectID = "", // or ""
                BUID = "",                  // or ""
                CsName = "",
                UserID = 506
            };

            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu2 = listBu;

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }


        [HttpPost]
        public JsonResult GetListCSSummary([FromForm] SPGetDataCSResponse.FilterData filter)
        {
            filter.Act = "GetListCSSummary";
            var result = _configProject.sp_GetDataCSResponse(filter);
            return Json(new { success = true, data = result.CSSummary });
        }

        [HttpPost]
        public JsonResult GetListUnitStatusCS()
        {
            var filter = new GetDDLModel
            {
                Act = "Ext",
                ID = 16
            };
            var result = _masterService.GetlisDDl(filter);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetListCountUnitStatus([FromForm] int UserID , string BUID , string ProjectID)
        {
            var filter = new SPGetDataCSResponse.FilterData
            {
                Act = "GetListCountUnitStatus",
                UserID = UserID,
                BUID = BUID,
                ProjectID = ProjectID
            };

            var result = _configProject.sp_GetDataCSResponse(filter);
            return Json(new { success = true, data = result.CountUnitStatus });
        }

        [HttpPost]
        public JsonResult GetlistBuildInProject(string ProjectID)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "listBuildInProject", IDString = ProjectID });
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetListFloorInBuildInProject(string ProjectID, string Builds)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "ListFloorInBuildInProject", IDString = ProjectID , IDString2 = Builds});
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetListUnitInFloorInBuildInProject(string ProjectID, string Builds , string Floors)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "ListUnitInFloorInBuildInProject", IDString = ProjectID, IDString2 = Builds , IDString3 = Floors});
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetlistDataTableUnitCSResponse(string USerID, string Showtype, string UnitStatus, string ProjectID, string Builds, string Floors , string Units)
        {
            var result = _csResponseServic.GetlistUnitCSResponse(new GetlistUnitCSResponseModel.FilterData { L_UserID = USerID, L_TypeUserShow = Showtype, L_ProjectID = ProjectID,L_UnitStatus = UnitStatus, L_Build = Builds , L_Floor = Floors , L_Room = Units });
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult UpdateorInsertCsmapping([FromForm] UpdateInsertCsmapping model)
        {

            int menuId = Constants.Menu.CsResponse;
            int qcTypeId = 10;

            string? dep64 = User.FindFirst("DepartmentID")?.Value;
            string? rol64 = User.FindFirst("RoleID")?.Value;

            int departmentId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(dep64));
            int roleId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(rol64));

            var perms = _userAndPermissionService.GetPermissions(qcTypeId, menuId, departmentId, roleId);
            if (perms is null || !perms.Update)
            {
                return Json(new { success = false, message = "No Permission" });
            }

            string LoginID = User.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);

            model.UpdateBy = Commond.FormatExtension.Nulltoint(UserID);
            var result = _csResponseServic.UpdateorInsertCsmapping(model);
            return Json(new { success = result.Issuccess, message = result.Message });
        }

    }
}
