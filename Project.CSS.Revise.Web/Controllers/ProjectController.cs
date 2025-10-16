using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjectController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly IHostEnvironment _hosting;
        public ProjectController(IHttpContextAccessor httpContextAccessor
                                 , IMasterService masterService
                                 , IUserAndPermissionService userAndPermissionService
                                 , IHostEnvironment hosting) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _hosting = hosting;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
