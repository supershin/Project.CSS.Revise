using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class QueueBankController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly ICSResponseService _csResponseServic;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly MasterManagementConfigProject _configProject;

        public QueueBankController(IHttpContextAccessor httpContextAccessor
                          , IMasterService masterService
                          , ICSResponseService csResponseServic
                          , MasterManagementConfigProject configProject
                          , IUserAndPermissionService userAndPermissionService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _csResponseServic = csResponseServic;
            _configProject = configProject;
            _userAndPermissionService = userAndPermissionService;
        }
        public IActionResult Index()
        {
            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;

            var listExpecttransfer = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 13 });
            ViewBag.listExpecttransfer = listExpecttransfer;

            var listUnitstatuscs = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 16 });
            ViewBag.listUnitstatuscs = listUnitstatuscs;

            var listgCSRespons = _masterService.GetlisDDl(new GetDDLModel { Act = "listAllCSUser"});
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
        public JsonResult GetlistUnitByProject([FromForm] UnitModel model)
        {
            var result = _masterService.GetlistUnitByProject(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetlistRegisterTable([FromForm] GetQueueBankModel model)
        {
            var result = _configProject.sp_GetQueueBank_RegisterTable(model);

            int total = 0;
            int filtered = 0;

            if (result != null && result.Count > 0)
            {
                total = result[0].TotalRecords;
                filtered = result[0].FilteredRecords;
            }

            return Json(new
            {
                draw = model.draw,
                recordsTotal = total,
                recordsFiltered = filtered,
                data = result
            });
        }



    }
}
