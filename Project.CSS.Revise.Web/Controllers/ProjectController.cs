using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Project;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjectController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly IHostEnvironment _hosting;
        private readonly IProjectService _projectService;
        public ProjectController(IHttpContextAccessor httpContextAccessor
                                 , IMasterService masterService
                                 , IUserAndPermissionService userAndPermissionService
                                 , IHostEnvironment hosting
                                 , IProjectService projectService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _hosting = hosting;
            _projectService = projectService;
        }

        public IActionResult Index()
        {

            var listCompany = _masterService.GetlisDDl(new GetDDLModel { Act = "listCompany"});
            ViewBag.listCompany = listCompany;

            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;

            var listProjectPartner = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 59 });
            ViewBag.listProjectPartner = listProjectPartner;

            var listProjectStatus = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 54 });
            ViewBag.listProjectStatus = listProjectStatus;

            var listLandOffice = _masterService.GetlisDDl(new GetDDLModel { Act = "listLandOffice" });
            ViewBag.listLandOffice = listLandOffice;

            var listProjectZone= _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 38 });
            ViewBag.listProjectZone = listProjectZone;

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetListProjectTable([FromForm] ProjectSettingModel.ProjectFilter filter)
        {
            var result = _projectService.GetlistProjectTable(filter);
            return Json(new { success = true, data = result });
        }
    }
}
