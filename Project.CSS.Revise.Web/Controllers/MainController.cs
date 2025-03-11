using Microsoft.AspNetCore.Mvc;

namespace Project.CSS.Revise.Web.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
