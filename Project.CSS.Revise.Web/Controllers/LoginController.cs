using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Service;
using System;
using System.Security.Claims;
using Microsoft.Extensions.Options;


namespace Project.CSS.Revise.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly AuthenticationSettings _authSettings;
        public LoginController(ILoginService loginService, IOptions<AuthenticationSettings> authOptions)
        {
            _loginService = loginService;
            _authSettings = authOptions.Value; // 👈 เอาค่าออกมาใช้
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    return Json(new { status = 0, message = Constants.Message.ERROR.USERNAME_AND_PASSWORD_CANNOT_BE_EMPTY });
                }

                UserProfile model = new UserProfile
                {
                    Username = Username,
                    Password = Password
                };
                UserProfile userProfile = _loginService.VerifyLogin(model);

                if (userProfile == null || userProfile.Status == 0)
                {
                    var message = userProfile?.Message ?? Constants.Message.ERROR.INVALID_USER_OR_PASSWORD;
                    return Json(new { status = 0, message = message });
                }

                //var x = SecurityManager.EnCryptPassword(userProfile.TitleTH + "." + " " + userProfile.FirstNameTH + " " + userProfile.LastNameTH ?? "");
                //var y = SecurityManager.DecodeFrom64(x);

                // ✅ Add Claims for this user
                var claims = new List<Claim>
                {
                    new Claim("LoginID", SecurityManager.EnCryptPassword(userProfile.ID.ToString())),
                    new Claim("Password", SecurityManager.EnCryptPassword(Password.ToString())),
                    new Claim("UserID", SecurityManager.EnCryptPassword(userProfile.UserID ?? string.Empty)),
                    new Claim("LoginNameTH", SecurityManager.EnCryptPassword(userProfile.TitleTH + " " + userProfile.FirstNameTH + " " + userProfile.LastNameTH ?? "")),
                    new Claim("LoginNameEN", SecurityManager.EnCryptPassword(userProfile.TitleEN + " " + userProfile.FirstNameEN + " " + userProfile.LastNameEN ?? "")),
                    new Claim(ClaimTypes.Email, SecurityManager.EnCryptPassword(userProfile.Email ?? "")),
                    new Claim("RoleID", SecurityManager.EnCryptPassword(userProfile.RoleID?.ToString() ?? "")),
                    new Claim("DepartmentID", SecurityManager.EnCryptPassword(userProfile.DepartmentID?.ToString() ?? "")),
                    new Claim("DepartmentName", SecurityManager.EnCryptPassword(userProfile.DepartmentName?.ToString() ?? ""))
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(_authSettings.SessionTimeoutHours)
                });

                return Json(new { status = 1, redirectUrl = Url.Action("Index", "Main") });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }



    }
}
