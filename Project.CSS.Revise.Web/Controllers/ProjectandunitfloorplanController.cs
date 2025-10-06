using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Service;
using static Project.CSS.Revise.Web.Models.Pages.Projectandunitfloorplan.ProjectandunitfloorplanModel;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjectandunitfloorplanController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly IProjectandunitfloorplanService _projectandunitfloorplanService;

        public ProjectandunitfloorplanController(IHttpContextAccessor httpContextAccessor
                                         , IMasterService masterService
                                         , IUserAndPermissionService userAndPermissionService
                                         , IProjectandunitfloorplanService projectandunitfloorplanService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _projectandunitfloorplanService = projectandunitfloorplanService;
        }

        public IActionResult Index()
        {
            int menuId = Constants.Menu.Projectandunitfloorplan;
            string? dep64 = User.FindFirst("DepartmentID")?.Value;
            string? rol64 = User.FindFirst("RoleID")?.Value;

            int departmentId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(dep64));
            int roleId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(rol64));

            var perms = _userAndPermissionService.GetPermissions(10, menuId, departmentId, roleId);
            if (!perms.View) return RedirectToAction("NoPermission", "Home");
            ViewBag.Permission = perms;

            var listDDlAllProject = _masterService.GetlisDDl(new GetDDLModel { Act = "listDDlAllProject" });
            ViewBag.listDDlAllProject = listDDlAllProject;

            return View();
        }

        [HttpPost]
        public JsonResult GetUnitTypeListByProjectID(string ProjectID)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "ListUnitTypeByProject", IDString = ProjectID });
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetlistProjectFloorPlan(string ProjectID)
        {
            var result = _projectandunitfloorplanService.GetlistProjectFloorPlan(new ListProjectFloorplan { ProjectID = ProjectID});
            var Count  = result.Count;
            return Json(new { success = true, data = result , count = Count});
        }

        [HttpPost]
        public JsonResult GetlistUnit(string ProjectID , string UnitType)
        {
            var result = _projectandunitfloorplanService.GetlistUnit(new ListUnit { ProjectID = ProjectID , UnitType = UnitType });
            var Count = result.Count;
            return Json(new { success = true, data = result, count = Count });
        }
    }
}
