using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Hubs;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class QueueBankCheckerViewController : BaseController
    {

        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly IQueueBankCounterViewService _queueBankCounterViewService;
        private readonly IQueueBankService _queueBankService;
        private readonly IHubContext<NotifyHub> _notifyHubContext;

        public QueueBankCheckerViewController(IHttpContextAccessor httpContextAccessor
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

            var listCareer = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 12 });
            ViewBag.listCareer = listCareer;

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
        public IActionResult UpdateUnitRegister([FromBody] UpdateUnitRegisterModel.Entity model)
        {
            var message = _queueBankCounterViewService.UpdateUnitRegister(model);
            return Json(message);
        }

        [HttpPost]
        public IActionResult RemoveUnitRegister([FromBody] UpdateUnitRegisterModel.Entity model)
        {
            var message = _queueBankCounterViewService.RemoveUnitFromCounter(model);
            return Json(message);
        }

        [HttpPost]
        public IActionResult CheckoutBankCounter([FromBody] BankCheckoutRequest model)
        {
            var message = _queueBankCounterViewService.CheckoutBankCounter(model);
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

        //[HttpPost]
        //public async Task<IActionResult> StopCallStaff([FromBody] RegisterCallStaffCounter model)
        //{
        //    if (model == null)
        //        return BadRequest(new { success = false, message = "Model is null." });

        //    // ✅ บังคับให้เป็น stop
        //    model.CallStaffStatus = "stop";

        //    // (optional) ใส่ ActionDate/อื่นๆ ถ้าต้องการ
        //    // model.ActionDate = DateTime.Now;

        //    // ✅ Broadcast ไปทุก client
        //    await _notifyHubContext.Clients.All.SendAsync("sendCallStaff", model);

        //    return Json(new { success = true, message = "Stop call staff broadcasted.", data = model });
        //}

        [HttpPost]
        public async Task<IActionResult> SaveRegisterCallStaffCounter([FromBody] RegisterCallStaffCounter model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new { Success = false, Message = "Model is null." });
                }

                if (string.IsNullOrWhiteSpace(model.ProjectID))
                {
                    return Json(new { Success = false, Message = "ProjectID is required." });
                }

                if (model.Counter <= 0)
                {
                    return Json(new { Success = false, Message = "Counter is required." });
                }

                if (model.RegisterLogID <= 0)
                {
                    return Json(new { Success = false, Message = "RegisterLogID is required." });
                }

                var status = (model.CallStaffStatus ?? "").Trim().ToLower();

                if (status != "start" && status != "stop")
                {
                    return Json(new { Success = false, Message = "CallStaffStatus must be 'start' or 'stop'." });
                }

                // ✅ normalize กลับเข้า model กันเคส START/Stop
                model.CallStaffStatus = status;

                // ✅ userId (ปรับ claim name ตามระบบพ่อใหญ่)
                int userId = 0;


                string loginIdClaim = User.FindFirst("LoginID")?.Value;
                string passClaim = User.FindFirst("Password")?.Value;

                // ถอดแบบ "ปลอดภัย" – ถ้าไม่ใช่ base64 จะได้ไม่ระเบิด
                string _userID = SecurityManager.TryDecodeFrom64(loginIdClaim ?? string.Empty);

                if (!int.TryParse(_userID, out userId))
                {
                    userId = 0;
                }

                // ✅ save (คืน bool)
                bool saved = _queueBankCounterViewService.SaveRegisterCallStaffCounter(model, userId);

                if (!saved)
                {
                    return Json(new { Success = false, Message = "Cannot save Call Staff data." });
                }

                // ✅ broadcast ไป client
                await _notifyHubContext.Clients.All.SendAsync("sendCallStaff", model);

                return Json(new
                {
                    Success = true,
                    Message = status == "start" ? "Call Staff Started." : "Call Staff Stopped."
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
