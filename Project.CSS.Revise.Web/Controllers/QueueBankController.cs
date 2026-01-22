using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Hubs;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using Project.CSS.Revise.Web.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        private readonly IHubContext<QueueBankHub> _hub;

        public QueueBankController(
            IHttpContextAccessor httpContextAccessor,
            IMasterService masterService,
            ICSResponseService csResponseServic,
            MasterManagementConfigProject configProject,
            IUserAndPermissionService userAndPermissionService,
            IQueueBankService queueBankService,
            IHubContext<QueueBankHub> hub
        ) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _csResponseServic = csResponseServic;
            _configProject = configProject;
            _userAndPermissionService = userAndPermissionService;
            _queueBankService = queueBankService;
            _hub = hub;
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

            var listBank = _masterService.GetlisDDl(new GetDDLModel { Act = "listAllBank" });
            ViewBag.listBank = listBank;

            var listBankNonSubmissionReason = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 69 });
            ViewBag.listBankNonSubmissionReason = listBankNonSubmissionReason;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RemoveRegisterLog(int ID)
        {
            // ✅ หาค่า projectId ก่อนลบ
            var projectId = _queueBankService.GetProjectIDRegisterLog(ID);

            var Issuccess = _queueBankService.RemoveRegisterLog(ID);

            if (!string.IsNullOrWhiteSpace(projectId))
            {
                await _hub.Clients.Group($"queuebank:project:{projectId}")
                    .SendAsync("QueueBankChanged", new
                    {
                        action = "delete",
                        projectId = projectId,
                        id = ID
                    });
            }

            return Json(new { result = Issuccess });
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

            //var careerModel = new GetQueueBankModel
            //{
            //    L_Act = "SummeryRegisterCareerType",
            //    L_ProjectID = model.L_ProjectID,
            //    L_RegisterDateStart = model.L_RegisterDateStart,
            //    L_RegisterDateEnd = model.L_RegisterDateEnd,
            //    L_UnitID = model.L_UnitID,
            //    L_CSResponse = model.L_CSResponse,
            //    L_UnitCS = model.L_UnitCS,
            //    L_ExpectTransfer = model.L_ExpectTransfer,
            //    L_QueueTypeID = model.L_QueueTypeID,
            //    start = model.start,
            //    length = model.length,
            //    SearchTerm = model.SearchTerm
            //};

            // รัน 3 ตัวคู่ขนาน (ยังใช้ DAL sync เดิม แต่ขนานกัน)
            var typeTask = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterType(typeModel));
            var loanTask = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterLoanType(loanModel));
            /*var careerTask = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterCareerType(careerModel))*/;

            await Task.WhenAll(typeTask, loanTask);

            var listDataSummeryRegisterType = typeTask.Result;
            var listDataSummeryRegisterLoanTyp = loanTask.Result;
            //var listDataSummeryRegisterCareerTyp = careerTask.Result;

            return Json(new
            {
                listDataSummeryRegisterType = listDataSummeryRegisterType,
                listDataSummeryRegisterLoanTyp = listDataSummeryRegisterLoanTyp,
                //listDataSummeryRegisterCareerTyp = listDataSummeryRegisterCareerTyp
            });
        }

        [HttpPost]
        public async Task<JsonResult> GetlistSummeryRegisterBank([FromForm] GetQueueBankModel model)
        {
            var bankModel = new GetQueueBankModel
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

            var nonSubmissionModel = new GetQueueBankModel
            {
                L_Act = "SummeryRegisterBankNonSubmissionReason",
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

            // ❗ ถ้า service/EF ยังเป็น sync: ห่อ Task.Run “เฉพาะจำเป็น”
            var taskBank = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterBank(bankModel));
            var taskNon = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterBankNonSubmissionReason(nonSubmissionModel));
            var careerTask = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterCareerType(careerModel));

            await Task.WhenAll(taskBank, taskNon, careerTask);

            return Json(new
            {
                listDataSummeryRegisterBank = taskBank.Result,
                listDataSummeryRegisterBankNonSubmissionReason = taskNon.Result,
                listDataSummerycareerTask = careerTask.Result
            });
        }

        [HttpPost]
        public async Task<JsonResult> GetlistSummeryRegisterBankCustomerView([FromForm] GetQueueBankModel model)
        {
            var bankModel = new GetQueueBankModel
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

            var nonSubmissionModel = new GetQueueBankModel
            {
                L_Act = "SummeryRegisterBankNonSubmissionReason",
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

            // ❗ ถ้า service/EF ยังเป็น sync: ห่อ Task.Run “เฉพาะจำเป็น”
            var taskBank = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterBank(bankModel));
            var taskNon = Task.Run(() => _configProject.sp_GetQueueBank_SummeryRegisterBankNonSubmissionReason(nonSubmissionModel));

            await Task.WhenAll(taskBank, taskNon);

            return Json(new
            {
                listDataSummeryRegisterBank = taskBank.Result,
                listDataSummeryRegisterBankNonSubmissionReason = taskNon.Result
            });
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
        public async Task<JsonResult> SaveRegisterLog([FromForm] RegisterLog model)
        {
            try
            {
                string loginIdClaim = User.FindFirst("LoginID")?.Value;
                string userID = SecurityManager.TryDecodeFrom64(loginIdClaim ?? string.Empty);

                model.SaveByID = FormatExtension.Nulltoint(userID);
                ValidateSaveRegisterLog(model);

                _queueBankService.SaveRegisterLog(model);

                // ✅ ยิงแจ้ง refresh เฉพาะ Project
                var projectId = model.ProjectID?.ToString() ?? ""; // ให้ตรง type ที่ model ใช้จริง
                if (!string.IsNullOrWhiteSpace(projectId))
                {
                    await _hub.Clients.Group($"queuebank:project:{projectId}")
                        .SendAsync("QueueBankChanged", new
                        {
                            action = "save",
                            projectId = projectId,
                            id = model.ID
                        });
                }
                else
                {
                    projectId = _queueBankService.GetProjectIDRegisterLog(model.ID);
                    await _hub.Clients.Group($"queuebank:project:{projectId}")
                    .SendAsync("QueueBankChanged", new
                    {
                        action = "save",
                        projectId = projectId,
                        id = model.ID
                    });
                }

                return Json(new { Message = Constants.Message.SUCCESS.SAVE_SUCCESS, Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = InnerException(ex) });
            }
        }


        [HttpPost]
        public ActionResult RegisterLogInfo(RegisterLog criteria)
        {
            try
            {
                string loginIdClaim = User.FindFirst("UserID")?.Value;
                string passClaim = User.FindFirst("Password")?.Value;

                // ถอดแบบ "ปลอดภัย" – ถ้าไม่ใช่ base64 จะได้ไม่ระเบิด
                string userID = SecurityManager.TryDecodeFrom64(loginIdClaim ?? string.Empty);
                //string password = SecurityManager.TryDecodeFrom64(passClaim ?? string.Empty);
                string password = passClaim ?? string.Empty;

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


        [HttpPost]
        public ActionResult CustomerSubmitFinPlus(LoanModel model)
        {
            try
            {
                _queueBankService.SaveCustomerSubmit_FINPlus(model);
                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = InnerException(ex) });
            }
        }

        [HttpPost]
        public ActionResult RemoveFinPlusBank(LoanBankModel model)
        {
            try
            {
                // ✅ Read LoginNameEN from cookie claims (encrypted)
                string loginNameEnClaim = User.FindFirst("LoginNameEN")?.Value;

                // ✅ decrypt safely
                string loginNameEn = SecurityManager.TryDecodeFrom64(loginNameEnClaim ?? string.Empty);

                // ✅ set UpdateBy on server (do NOT trust client)
                model.UpdateBy = loginNameEn;

                _queueBankService.SaveDeleteLoanBank_FINPlus(model);

                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = InnerException(ex) });
            }
        }


    }
}
