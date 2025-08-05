using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjecttargetrollingController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IProjectAndTargetRollingService _projectAndTargetRollingService;
        public ProjecttargetrollingController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IProjectAndTargetRollingService projectAndTargetRollingService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _projectAndTargetRollingService = projectAndTargetRollingService;
        }

        public IActionResult Index()
        {
            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;

            var filter = new GetDDLModel
            {
                Act = "Ext",
                ID = 32,

            };
            var ListPlanType = _masterService.GetlisDDl(filter);
            ViewBag.ListPlanType = ListPlanType;

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetDataTableProjectAndTargetRolling([FromForm] RollingPlanSummaryModel model)
        {
            var result = _projectAndTargetRollingService.GetListTargetRollingPlan(model);
            return Json(new { success = true, data = result });
        }

    }
}
