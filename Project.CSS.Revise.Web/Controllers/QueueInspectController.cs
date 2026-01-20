using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Hubs;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class QueueInspectController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly IHubContext<NotifyHub> _notifyHubContext;
        public QueueInspectController(IHttpContextAccessor httpContextAccessor
            , IMasterService masterService
            , IUserAndPermissionService userAndPermissionService
            , IHubContext<NotifyHub> notifyHubContext) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _notifyHubContext = notifyHubContext;
        }
        public IActionResult Index()
        {
            ViewBag.listBu = _masterService.GetlistBU(new BUModel());

            var allProject = _masterService.GetlisDDl(new GetDDLModel { Act = "listDDlAllProject" });
            ViewBag.listDDlAllProject = allProject;
            ViewBag.listProject = allProject;

            var listExpecttransfer = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 13 });
            ViewBag.listExpecttransfer = listExpecttransfer;

            var listUnitstatuscs = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 16 });
            ViewBag.listUnitstatuscs = listUnitstatuscs;

            var listgCSRespons = _masterService.GetlisDDl(new GetDDLModel { Act = "listAllCSUser" });
            ViewBag.listgCSRespons = listgCSRespons;

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetUnitListByProject([FromForm] UnitModel model)
        {
            var result = _masterService.GetlistUnitByProject(model);
            return Json(new { success = true, data = result });
        }
    }
}
