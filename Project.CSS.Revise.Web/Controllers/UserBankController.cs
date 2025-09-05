using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserBankController : BaseController
    {
        private readonly IMasterService _masterService;

        public UserBankController(IHttpContextAccessor httpContextAccessor, IMasterService masterService) : base(httpContextAccessor)
        {
            _masterService = masterService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
