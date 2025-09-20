using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
