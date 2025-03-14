using Microsoft.AspNetCore.Mvc;

namespace Project.CSS.Revise.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
