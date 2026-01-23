using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Hubs;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class QueueBankCounterViewController : BaseController
    {

        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly IQueueBankCounterViewService _queueBankCounterViewService;
        private readonly IQueueBankService _queueBankService;
        private readonly IHubContext<NotifyHub> _notifyHubContext;
        public QueueBankCounterViewController(IHttpContextAccessor httpContextAccessor
            , IMasterService masterService
            , IUserAndPermissionService userAndPermissionService
            , IQueueBankCounterViewService queueBankCounterViewService
            , IQueueBankService queueBankService
            , IHubContext<NotifyHub> notifyHubContext) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _queueBankCounterViewService = queueBankCounterViewService;
            _queueBankService = queueBankService;
            _notifyHubContext = notifyHubContext;
        }

        public IActionResult Index(string projectId, string projectName)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = projectName;
            var list = _queueBankCounterViewService.GetUnitDropdown(projectId);
            ViewBag.listunitforregister = list;

            return View();
        }

        [HttpGet]
        public IActionResult GetCounterList(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                return Json(new
                {
                    success = false,
                    message = "ProjectId is required.",
                    data = Array.Empty<object>()
                });
            }

            var filter = new ListCounterModel.Filter
            {
                ProjectID = projectId
            };

            var list = _queueBankCounterViewService.GetListsCounterQueueBank(filter);

            return Json(new
            {
                success = true,
                data = list
            });
        }

        [HttpGet]
        public IActionResult GetCounterDetailsList(string projectId, int counter)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                return Json(new
                {
                    success = false,
                    message = "ProjectId is required.",
                    data = Array.Empty<object>()
                });
            }

            var filter = new ListCounterDetailsModel.Filter
            {
                ProjectID = projectId
                ,
                Counter = counter
            };

            var list = _queueBankCounterViewService.GetListsCounterDetailsQueueBank(filter);

            return Json(new
            {
                success = true,
                data = list
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUnitRegister([FromBody] UpdateUnitRegisterModel.Entity model)
        {
            var message = _queueBankCounterViewService.UpdateUnitRegister(model);

            if (message?.Issucces == true)
                await _notifyHubContext.Clients.All.SendAsync("notifyCounter");

            return Json(message);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUnitRegister([FromBody] UpdateUnitRegisterModel.Entity model)
        {
            var message = _queueBankCounterViewService.RemoveUnitFromCounter(model);

            if (message?.Issucces == true)
                await _notifyHubContext.Clients.All.SendAsync("notifyCounter");

            return Json(message);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutBankCounter([FromBody] BankCheckoutRequest model)
        {
            var message = _queueBankCounterViewService.CheckoutBankCounter(model);

            if (message?.Issucces == true)
                await _notifyHubContext.Clients.All.SendAsync("notifyCounter");

            return Json(message);
        }


        // ============================
        //  NEW: QR image ต่อ Counter
        //  GET /QueueBankCounterView/CounterQr?projectId=...&projectName=...&queueType=bank&counterNo=1
        // ============================

        [HttpGet]
        [AllowAnonymous] // ถ้าต้องให้เครื่อง tablet ดูได้โดยไม่ login; ถ้าไม่ต้องก็ลบออก
        public async Task<IActionResult> CounterQr(
            string projectId,
            string projectName,
            string queueType,
            int counterNo,
            [FromServices] IWebHostEnvironment env)
        {
            if (string.IsNullOrWhiteSpace(projectId) ||
                string.IsNullOrWhiteSpace(projectName) ||
                counterNo <= 0)
            {
                return BadRequest("Invalid QR parameters.");
            }

            int queueTypeId = queueType?.Equals("bank", StringComparison.OrdinalIgnoreCase) == true
                ? 48
                : 49; // default inspect

            // โลโก้ ASW ตรงกลาง QR
            string iconUrl = BaseUrl + "assets/images/logo/ASW_Logo_Rac_dark-bg.png";

            byte[] pngBytes = await QrCounterImageHelper.GenerateCounterQrPngAsync(
                env,
                projectId,
                projectName,
                counterNo,
                queueTypeId,
                iconUrl
            );

            return File(pngBytes, "image/png");
        }


        [HttpPost]
        public ActionResult CheckingDingDongModel(string ProjectID, string LastRegisterSignature, string LastBankUpdateDate, string Day)
        {
            try
            {
                // =========================
                // Parse inputs (safe)
                // =========================
                ProjectID = (ProjectID ?? "").Trim();
                LastRegisterSignature = (LastRegisterSignature ?? "").Trim();

                DateTime? lastBankUpdateDt = null;
                if (!string.IsNullOrWhiteSpace(LastBankUpdateDate))
                {
                    // รองรับทั้ง "2026-01-23 10:20:30" / ISO / ฯลฯ
                    if (DateTime.TryParse(LastBankUpdateDate, out var tmp))
                        lastBankUpdateDt = tmp;
                }

                DateTime? dayDt = null;
                if (!string.IsNullOrWhiteSpace(Day))
                {
                    // รองรับ "2026-01-23" หรือ "23/01/2026" (แล้วแต่ culture)
                    if (DateTime.TryParse(Day, out var tmpDay))
                        dayDt = tmpDay.Date;
                }

                // =========================
                // Build filter -> call service
                // =========================
                var filter = new DingDongModel.Filter
                {
                    ProjectID = ProjectID,
                    LastRegisterSignature = LastRegisterSignature,
                    LastBankUpdateDate = lastBankUpdateDt,
                    Day = dayDt
                };

                var res = _queueBankCounterViewService.CheckCanDingDong(filter);

                // ✅ สำคัญ: คืนค่า "ล่าสุด" กลับไปให้ client เก็บ baseline รอบหน้า
                return Json(new
                {
                    Success = true,
                    CanDingDong = res.CanDingDong,

                    ProjectID = res.ProjectID,
                    RegisterSignature = res.RegisterSignature,          // latest
                    BankLatestUpdateDate = res.BankLatestUpdateDate,    // latest

                    // optional debug
                    RegisterChanged = res.RegisterChanged,
                    BankChanged = res.BankChanged
                });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = InnerException(ex) });
            }
        }


    }
}
