using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class OtherSettingsController : BaseController
    {
        public OtherSettingsController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
