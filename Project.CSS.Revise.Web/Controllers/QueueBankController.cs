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

        [HttpPost]
        public async Task<JsonResult> GetlistSummeryRegister([FromForm] GetQueueBankModel model)
        {
            // ถ้าต้องใช้ L_Act ต่างกัน ให้ clone model แต่ละตัว
            var typeModel = new GetQueueBankModel
            {
                L_Act = "SummeryRegisterType",
                L_ProjectID = model.L_ProjectID,
                L_RegisterDateStart = model.L_RegisterDateStart,
                L_RegisterDateEnd = model.L_RegisterDateEnd,
                L_UnitID = model.L_UnitID,
                L_CSResponse = model.L_CSResponse,
                L_UnitCS = model.L_UnitCS,
                L_ExpectTransfer = model.L_ExpectTransfer,
                L_QueueTypeID = model.L_QueueTypeID,
                start = model.start,
                length = model.length,
                SearchTerm = model.SearchTerm
            };

            var loanModel = new GetQueueBankModel
            {
                L_Act = "SummeryRegisterLoanType",
                L_ProjectID = model.L_ProjectID,
                L_RegisterDateStart = model.L_RegisterDateStart,
                L_RegisterDateEnd = model.L_RegisterDateEnd,
                L_UnitID = model.L_UnitID,
                L_CSResponse = model.L_CSResponse,
                L_UnitCS = model.L_UnitCS,
                L_ExpectTransfer = model.L_ExpectTransfer,
                L_QueueTypeID = model.L_QueueTypeID,
                start = model.start,
                length = model.length,
                SearchTerm = model.SearchTerm
            };

            var careerModel = new GetQueueBankModel
            {
                L_Act = "SummeryRegisterCareerType",
                L_ProjectID = model.L_ProjectID,
                L_RegisterDateStart = model.L_RegisterDateStart,
                L_RegisterDateEnd = model.L_RegisterDateEnd,
                L_UnitID = model.L_UnitID,
                L_CSResponse = model.L_CSResponse,
                L_UnitCS = model.L_UnitCS,
                L_ExpectTransfer = model.L_ExpectTransfer,
                L_QueueTypeID = model.L_QueueTypeID,
                start = model.start,
                length = model.length,
                SearchTerm = model.SearchTerm
            };

            // รัน 3 ตัวคู่ขนาน (ยังใช้ DAL sync เดิม แต่ขนานกัน)
            var typeTask = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterType(typeModel));
            var loanTask = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterLoanType(loanModel));
            var careerTask = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterCareerType(careerModel));

            await Task.WhenAll(typeTask, loanTask, careerTask);

            var listDataSummeryRegisterType = typeTask.Result;
            var listDataSummeryRegisterLoanTyp = loanTask.Result;
            var listDataSummeryRegisterCareerTyp = careerTask.Result;

            return Json(new
            {
                listDataSummeryRegisterType = listDataSummeryRegisterType,
                listDataSummeryRegisterLoanTyp = listDataSummeryRegisterLoanTyp,
                listDataSummeryRegisterCareerTyp = listDataSummeryRegisterCareerTyp
            });
        }

        [HttpPost]
        public JsonResult GetlistSummeryRegisterBank([FromForm] GetQueueBankModel model)
        {

            var BankModel = new GetQueueBankModel
            {
                L_Act = "SummeryRegisterBank",
                L_ProjectID = model.L_ProjectID,
                L_RegisterDateStart = model.L_RegisterDateStart,
                L_RegisterDateEnd = model.L_RegisterDateEnd,
                L_UnitID = model.L_UnitID,
                L_CSResponse = model.L_CSResponse,
                L_UnitCS = model.L_UnitCS,
                L_ExpectTransfer = model.L_ExpectTransfer,
                L_QueueTypeID = model.L_QueueTypeID,
                start = model.start,
                length = model.length,
                SearchTerm = model.SearchTerm
            };

            var listDataSummeryRegisterType = _configProject.sp_GetQueueBank_SummeryRegisterBank(BankModel);

            return Json(new{listDataSummeryRegisterType = listDataSummeryRegisterType});
        }
    }
}
