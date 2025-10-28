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
        private readonly IUserAndPermissionService _userAndPermissionService;
        public AppointmentLimitController(IHttpContextAccessor httpContextAccessor
                                        , IMasterService masterService
                                        , IAppointmentLimitService appointmentLimitService
                                        , IUserAndPermissionService userAndPermissionService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _appointmentLimitService = appointmentLimitService;
            _userAndPermissionService = userAndPermissionService;
        }

        public IActionResult Index()
        {
            int menuId = Constants.Menu.Appointmentlimit;
            string? dep64 = User.FindFirst("DepartmentID")?.Value;
            string? rol64 = User.FindFirst("RoleID")?.Value;

            int departmentId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(dep64));
            int roleId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(rol64));

            var perms = _userAndPermissionService.GetPermissions(10, menuId, departmentId, roleId);
            if (!perms.View) return RedirectToAction("NoPermission", "Home");
            ViewBag.Permission = perms;

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

            int menuId = Constants.Menu.Appointmentlimit;
            int qcTypeId = 10;

            string? dep64 = User.FindFirst("DepartmentID")?.Value;
            string? rol64 = User.FindFirst("RoleID")?.Value;

            int departmentId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(dep64));
            int roleId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(rol64));

            var perms = _userAndPermissionService.GetPermissions(qcTypeId, menuId, departmentId, roleId);
            if (perms is null || !perms.Update)
            {
                return Json(new { Issuccess = false, Message = "No Permission" });
            }

            string? LoginID = User.FindFirst("LoginID")?.Value;
            int UserID = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(LoginID));
            var resp = _appointmentLimitService.InsertOrUpdateProjectAppointLimit(filter , UserID);
            return Json(resp);
        }
    }
}
