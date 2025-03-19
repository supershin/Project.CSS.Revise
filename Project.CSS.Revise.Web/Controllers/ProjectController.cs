using Microsoft.AspNetCore.Mvc;

namespace Project.CSS.Revise.Web.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
