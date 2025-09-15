using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CSResponseController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly ICSResponseService _csResponseServic;
        public CSResponseController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, ICSResponseService csResponseServic) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _csResponseServic = csResponseServic;
        }

        public async Task<IActionResult> Index()
        {
            var filter1 = new GetDDLModel
            {
                Act = "listAllCSUser"
            };
            var listAllCSUser = _masterService.GetlisDDl(filter1);
            ViewBag.listAllCSUser = listAllCSUser;

            var filter2 = new GetDDLModel
            {
                Act = "listDDlAllProject"
            };
            var listDDlAllProject = _masterService.GetlisDDl(filter2);
            ViewBag.listDDlAllProject = listDDlAllProject;

            var model = await _csResponseServic.GetListCountByCSAsync();

            return View(model);
        }


        [HttpPost]
        public JsonResult GetlistBuildInProject(string ProjectID)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "listBuildInProject", IDString = ProjectID });
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetListFloorInBuildInProject(string ProjectID, string Builds)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "ListFloorInBuildInProject", IDString = ProjectID , IDString2 = Builds});
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetListUnitInFloorInBuildInProject(string ProjectID, string Builds , string Floors)
        {
            var result = _masterService.GetlisDDl(new GetDDLModel { Act = "ListUnitInFloorInBuildInProject", IDString = ProjectID, IDString2 = Builds , IDString3 = Floors});
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetlistDataTableUnitCSResponse(string USerID, string Showtype, string ProjectID, string Builds, string Floors , string Units)
        {
            var result = _csResponseServic.GetlistUnitCSResponse(new GetlistUnitCSResponseModel.FilterData { L_UserID = USerID, L_TypeUserShow = Showtype, L_ProjectID = ProjectID, L_Build = Builds , L_Floor = Floors , L_Room = Units });
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult UpdateorInsertCsmapping([FromForm] UpdateInsertCsmapping model)
        {
            string LoginID = User.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);

            model.UpdateBy = Commond.FormatExtension.Nulltoint(UserID);
            var result = _csResponseServic.UpdateorInsertCsmapping(model);
            return Json(new { success = result.Issuccess, message = result.Message });
        }

    }
}
