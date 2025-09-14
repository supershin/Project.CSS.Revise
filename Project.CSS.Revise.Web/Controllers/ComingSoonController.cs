using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ComingSoonController : BaseController
    {
        public ComingSoonController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
