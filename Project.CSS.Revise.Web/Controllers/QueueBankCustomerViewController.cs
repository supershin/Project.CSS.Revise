using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class QueueBankCustomerViewController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly ICSResponseService _csResponseServic;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly MasterManagementConfigProject _configProject;
        public QueueBankCustomerViewController(IHttpContextAccessor httpContextAccessor
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
        public IActionResult Index(string projectId, string projectName)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = projectName;
            return View();
        }
    }
}
