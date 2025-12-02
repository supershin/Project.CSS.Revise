using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    }
}
