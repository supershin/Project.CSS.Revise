using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Service;
using static Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture.FurnitureAndUnitFurnitureModel;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class FurnitureAndUnitFurnitureController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly IFurnitureAndUnitFurnitureService _furnitureAndUnitFurnitureService;

        public FurnitureAndUnitFurnitureController(IHttpContextAccessor httpContextAccessor
                                                 , IMasterService masterService
                                                 , IUserAndPermissionService userAndPermissionService
                                                 , IFurnitureAndUnitFurnitureService furnitureAndUnitFurnitureService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _furnitureAndUnitFurnitureService = furnitureAndUnitFurnitureService;
        }

        public IActionResult Index()
        {
            int menuId = Constants.Menu.FurnitureAndUnitFurniture;
            string? dep64 = User.FindFirst("DepartmentID")?.Value;
            string? rol64 = User.FindFirst("RoleID")?.Value;

            int departmentId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(dep64));
            int roleId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(rol64));

            var perms = _userAndPermissionService.GetPermissions(10, menuId, departmentId, roleId);
            if (!perms.View) return RedirectToAction("NoPermission", "Home");
            ViewBag.Permission = perms;

            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;


            var ListFuniture = _masterService.GetlisDDl(new GetDDLModel { Act = "ListFuniture" });
            ViewBag.ListFuniture = ListFuniture;

            ViewBag.FurnitureCount = ListFuniture.Count;

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetUnitTypeListByProjectID(string ProjectID)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "ListUnitTypeByProject", IDString = ProjectID });
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetListTableUnitFurniture([FromForm] UnitFurnitureFilter model)
        {
            var result = _furnitureAndUnitFurnitureService.GetlistUnitFurniture(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public async Task<IActionResult> SaveFurnitureProjectMapping([FromBody] SaveFurnitureProjectMappingRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.ProjectID))
                return Json(new { success = false, message = "ProjectID is required" });

            if (req.Furnitures == null || req.Furnitures.Count == 0
                || req.Units == null || req.Units.Count == 0)
                return Json(new { success = false, message = "Need at least 1 furniture and 1 unit" });

            try
            {
                if (!(User?.Identity?.IsAuthenticated ?? false))
                    return Unauthorized(new { success = false, message = "Unauthorized" });

                string? loginId64 = User.FindFirst("LoginID")?.Value;
                string userIdDecoded = SecurityManager.DecodeFrom64(loginId64);
                int userId = Commond.FormatExtension.Nulltoint(userIdDecoded);

                var ok = await _furnitureAndUnitFurnitureService.SaveFurnitureProjectMappingAsync(req, userId);
                return Json(new { success = ok, message = ok ? null : "Save failed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }





    }
}
