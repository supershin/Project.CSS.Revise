using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Service;
using static Project.CSS.Revise.Web.Models.Security.PermissionGuard;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class FurnitureAndUnitFurnitureController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        public FurnitureAndUnitFurnitureController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IUserAndPermissionService userAndPermissionService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
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
    }
}
