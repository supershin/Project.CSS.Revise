using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.UserAndPermission;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    public class UserAndPermissionController : BaseController
    {

        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;

        public UserAndPermissionController(IHttpContextAccessor httpContextAccessor, IMasterService masterService , IUserAndPermissionService userAndPermissionService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
        }

        public IActionResult Index()
        {
            var listDepartments = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 9 });
            ViewBag.listDepartments = listDepartments;

            var listRoles = _masterService.GetlisDDl(new GetDDLModel { Act = "listRole"});
            ViewBag.listRoles = listRoles;

            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;

            var ListTitleTH = _masterService.GetlisDDl(new GetDDLModel { Act = "ListTitleTH" });
            ViewBag.ListTitleTHs = ListTitleTH;

            var ListTitleEN = _masterService.GetlisDDl(new GetDDLModel { Act = "ListTitleTH" });
            ViewBag.ListTitleENs = ListTitleEN;

            return View();
        }

        // ✅ ดึงรายการผู้ใช้ (ยังไม่ทำฟิลเตอร์ เดี๋ยวค่อยต่อ)
        [HttpPost]
        [Route("UserAndPermission/GetlistUser")]
        public JsonResult GetlistUser([FromForm] UserAndPermissionModel.FiltersGetlistUser filters)
        {
            filters ??= new UserAndPermissionModel.FiltersGetlistUser();
            var data = _userAndPermissionService.GetlistUser(filters);
            // ถ้าจะใช้กับ DataTables: return Json(new { data, recordsTotal = data.Count, recordsFiltered = data.Count });
            return Json(new { data });
        }
    }
}
