using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Data;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class MainController : BaseController
    {

        public MainController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
