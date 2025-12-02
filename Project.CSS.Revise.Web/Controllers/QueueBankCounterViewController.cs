using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
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
        public QueueBankCounterViewController(IHttpContextAccessor httpContextAccessor
            , IMasterService masterService
            , IUserAndPermissionService userAndPermissionService
            , IQueueBankCounterViewService queueBankCounterViewService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _queueBankCounterViewService = queueBankCounterViewService;
        }

        public IActionResult Index(string projectId, string projectName)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = projectName;

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

            // เรียก Service ที่หุ้ม Repo ของมึง
            var list = _queueBankCounterViewService.GetListsCounterQueueBank(filter);

            return Json(new
            {
                success = true,
                data = list
            });
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

    }
}
