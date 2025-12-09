using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
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
        private readonly IQueueBankService _queueBankService;

        public QueueBankController(IHttpContextAccessor httpContextAccessor
                          , IMasterService masterService
                          , ICSResponseService csResponseServic
                          , MasterManagementConfigProject configProject
                          , IUserAndPermissionService userAndPermissionService
                          , IQueueBankService queueBankService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _csResponseServic = csResponseServic;
            _configProject = configProject;
            _userAndPermissionService = userAndPermissionService;
            _queueBankService = queueBankService;
        }
        public IActionResult Index()
        {
            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;

            var listExpecttransfer = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 13 });
            ViewBag.listExpecttransfer = listExpecttransfer;

            var listUnitstatuscs = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 16 });
            ViewBag.listUnitstatuscs = listUnitstatuscs;

            var listgCSRespons = _masterService.GetlisDDl(new GetDDLModel { Act = "listAllCSUser" });
            ViewBag.listgCSRespons = listgCSRespons;

            var listgCSResponsEdit = _masterService.GetlisDDl(new GetDDLModel { Act = "listAllUser" });
            ViewBag.listgCSResponsEdit = listgCSResponsEdit;

            var listCareer = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 12 });
            ViewBag.listCareer = listCareer;

            var listReason = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 15 });
            ViewBag.listReason = listReason;

            var listBank = _masterService.GetlisDDl(new GetDDLModel { Act = "listAllBank"});
            ViewBag.listBank = listBank;

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

            return Json(new { listDataSummeryRegisterType = listDataSummeryRegisterType });
        }

        [HttpPost]
        public JsonResult GetlistCreateRegisterTable([FromForm] GetQueueBankModel model)
        {
            var bankModel = new GetQueueBankModel
            {
                L_Act = "CreateRegisterTable",
                L_ProjectID = model.L_ProjectID,
                L_RegisterDateStart = "",
                L_RegisterDateEnd = "",
                L_UnitID = "",
                L_CSResponse = "",
                L_UnitCS = "",
                L_ExpectTransfer = "",
                start = model.start,
                length = model.length,
                SearchTerm = model.SearchTerm
            };

            var result = _configProject.sp_GetQueueBank_CreateRegisterTable(bankModel)
                         ?? new List<ListCreateRegisterTableModel>();

            int total = 0;
            int filtered = 0;

            if (result.Count > 0)
            {
                // ใช้ค่าจาก SP ถ้ามี
                if (result[0].TotalRecords > 0)
                {
                    total = result[0].TotalRecords;
                    filtered = result[0].FilteredRecords;
                }
                else
                {
                    total = filtered = result.Count;
                }
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
        public JsonResult GetListUnitForRegisterBankTable(string ProjectID)
        {
            var ListUnitForRegisterBankTable = _queueBankService.GetListUnitForRegisterBank(ProjectID);
            return Json(new { ListUnitForRegisterBankTable = ListUnitForRegisterBankTable });
        }

        [HttpPost]
        public ActionResult GetMessageAppointmentInspect(RegisterLog model)
        {
            try
            {
                var Msg = _queueBankService.GetMessageAppointmentInspect(model);
                return Json(new
                {
                    Message = Msg,
                    Success = true
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Message = InnerException(ex)
                });
            }

        }

        private void ValidateSaveRegisterLog(RegisterLog model)
        {
            if (model.QueueTypeID.AsInt() == Constants.Ext.QUEUE_TYPE_BANK)
            {
                if ((model.CareerTypeID.AsInt() == Constants.Ext.NO_INPUT
                    || model.ResponsibleID.AsInt() == Constants.Ext.NO_INPUT
                    || (model.FlagInprocess == null && model.FlagFinish == null))
                    && model.ID > 0)
                {
                    throw new Exception(Constants.Message.ERROR.PLEASE_INPUT_DATA);
                }
            }
        }

        [HttpPost]
        public JsonResult SaveRegisterLog([FromForm] RegisterLog model)
        {
            try
            {
                // เช็คเฉพาะเงื่อนไข UI ฝั่ง BANK (ต่อจากของเดิม)
                ValidateSaveRegisterLog(model);

                // เรียก Service → Repo → SaveRegisterLog ที่เราเพิ่งย้ายมาใช้ _context
                _queueBankService.SaveRegisterLog(model);

                // ถ้าทีหลังอยากส่ง SignalR เหมือนระบบเก่า
                // NotifyCounterSignalR();

                return Json(new
                {
                    Message = Constants.Message.SUCCESS.SAVE_SUCCESS,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = InnerException(ex)
                });
            }
        }

        [HttpPost]
        public ActionResult RegisterLogInfo(RegisterLog criteria)
        {
            try
            {
                string loginIdClaim = User.FindFirst("LoginID")?.Value;
                string passClaim = User.FindFirst("Password")?.Value;

                // ถอดแบบ "ปลอดภัย" – ถ้าไม่ใช่ base64 จะได้ไม่ระเบิด
                string userID = SecurityManager.TryDecodeFrom64(loginIdClaim ?? string.Empty);
                string password = SecurityManager.TryDecodeFrom64(passClaim ?? string.Empty);

                var model = _queueBankService.GetRegisterLogInfo(criteria, userID, password);

                return Json(new
                {
                    Success = true,
                    Data = model
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = InnerException(ex)
                });
            }
        }


    }
}
