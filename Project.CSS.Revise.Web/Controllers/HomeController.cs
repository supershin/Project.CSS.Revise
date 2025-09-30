using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Models;

namespace Project.CSS.Revise.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult NoPermission(int menuId, int departmentId, int roleId, string? reason = null)
        {
            ViewBag.MenuId = menuId;
            ViewBag.DepartmentId = departmentId;
            ViewBag.RoleId = roleId;
            ViewBag.Reason = reason;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
