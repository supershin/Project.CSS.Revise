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

        [HttpPost]
        public async Task<IActionResult> SaveMapping([FromForm] SaveMappingFloorplanModel model, CancellationToken ct)
        {
            // Collect human-readable validation errors
            var errors = new List<string>();

            if (model is null)
                return Json(new { success = false, message = "ไม่พบข้อมูลที่ส่งมา", errors = new[] { "payload is null" } });

            // Normalize & de-dup inputs
            var projectId = (model.ProjectID ?? string.Empty).Trim();
            var floorIds = (model.FloorPlanIDs ?? new List<Guid>()).Where(g => g != Guid.Empty).Distinct().ToList();
            var unitIds = (model.UnitIDs ?? new List<Guid>()).Where(g => g != Guid.Empty).Distinct().ToList();

            if (string.IsNullOrWhiteSpace(projectId)) errors.Add("กรุณาเลือกโครงการ (ProjectID)");
            if (floorIds.Count == 0) errors.Add("กรุณาเลือก Floor plan อย่างน้อย 1 รายการ");
            if (unitIds.Count == 0) errors.Add("กรุณาเลือก Unit อย่างน้อย 1 รายการ");

            if (errors.Count > 0)
            {
                return Json(new
                {
                    success = false,
                    message = string.Join(" | ", errors),
                    errors
                });
            }

            // Put normalized values back to the model
            model.ProjectID = projectId;
            model.FloorPlanIDs = floorIds;
            model.UnitIDs = unitIds;

            // UserId from claims
            string? loginId64 = User.FindFirst("LoginID")?.Value;
            var userId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(loginId64));

            var ok = await _projectandunitfloorplanService.SaveMappingAsync(model, userId, ct);

            return Json(new
            {
                success = ok,
                message = ok
                    ? "บันทึก Mapping สำเร็จ"
                    : "บันทึกไม่สำเร็จ กรุณาลองใหม่",
                // Optional: include counts for UI feedback
                selectedFloorPlans = floorIds.Count,
                selectedUnits = unitIds.Count
            });
        }

        [HttpPost]
        public JsonResult GetListFloorPlansByUnit(string Unit)
        {
            Guid unitId = Commond.FormatExtension.NulltoGuid(Unit);
            var result = _projectandunitfloorplanService.GetFloorPlansByUnit(unitId);
            return Json(new { success = true, data = result});
        }

        [HttpPost]
        public JsonResult RemoveUnitFloorPlan(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Invalid mapping ID." });

            string? loginId64 = User.FindFirst("LoginID")?.Value;
            var userId = Commond.FormatExtension.Nulltoint(SecurityManager.DecodeFrom64(loginId64));

            var ok = _projectandunitfloorplanService.DeactivateUnitFloorPlan(id, userId);
            return Json(new { success = ok, message = ok ? "Removed." : "Remove failed." });
        }

    }
}
