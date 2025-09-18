using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.AppointmentLimit;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    public class AppointmentLimitController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IAppointmentLimitService _appointmentLimitService;

        public AppointmentLimitController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IAppointmentLimitService appointmentLimitService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _appointmentLimitService = appointmentLimitService;
        }

        public IActionResult Index()
        {


            var listDDlAllProject = _masterService.GetlisDDl(new GetDDLModel { Act = "listDDlAllProject" });
            ViewBag.listDDlAllProject = listDDlAllProject;

            var listTimes = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext" , ID = 42 });
            ViewBag.listTimes = listTimes;

            return View();
        }


        [HttpPost]
        public IActionResult GetlistAppointmentLimit([FromBody] AppointmentLimitModel.ProjectAppointLimitPivotRow filter)
        {
            var resp = _appointmentLimitService.GetlistAppointmentLimit(filter);
            return Json(resp);
        }

        [HttpPost]
        public IActionResult InsertOrUpdateProjectAppointLimit([FromBody] IEnumerable<AppointmentLimitModel.ProjectAppointLimitIUD> filter)
        {
            string? LoginID = User.FindFirst("LoginID")?.Value;
            int UserID = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(LoginID));
            var resp = _appointmentLimitService.InsertOrUpdateProjectAppointLimit(filter , UserID);
            return Json(resp);
        }
    }
}
