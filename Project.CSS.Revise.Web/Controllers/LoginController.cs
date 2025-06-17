using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Common;
using Project.CSS.Revise.Web.Models;

namespace Project.CSS.Revise.Web.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserProfile userProfile, string returnUrl)
        {
            try
            {
                //validateLogin(userProfile.Email, userProfile.Password);

                //var user = _login.VerifyLogin(userProfile.Email, userProfile.Password);
                //_securityManager.SignIn(this.HttpContext, user);

                //if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                //{
                //    var url = returnUrl.Substring(1, returnUrl.Length - 1);
                //    return LocalRedirect(returnUrl);
                //}
                //else return RedirectToAction("Index", "Main");
                return View();
            }
            catch (Exception ex)
            {
                //ViewBag.returnUrl = returnUrl;
                //ViewBag.ErrorMsg = InnerException(ex);
                //return View("Index", userProfile);
                return View();
            }
        }
    }
}
