using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class OtherSettingsController : BaseController
    {
        private readonly IMasterService _masterService;
        public OtherSettingsController(IHttpContextAccessor httpContextAccessor, IMasterService masterService): base(httpContextAccessor)
        {
            _masterService = masterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Page_Load()
        {
            var data = _masterService.GetlistBU(new BUModel());
            return Json(new { success = true, buList = data });
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }


    }
}
