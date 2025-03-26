using Microsoft.AspNetCore.Mvc;

namespace Project.CSS.Revise.Web.Controllers
{
    public class CSResponseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
