using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Library.DAL;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CSResponseController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly ICSResponseService _csResponseServic;
        private readonly MasterManagementConfigProject _configProject;
        public CSResponseController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, ICSResponseService csResponseServic, MasterManagementConfigProject configProject) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _csResponseServic = csResponseServic;
            _configProject = configProject;
        }

        public IActionResult Index()
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

            var filter = new SPGetDataCSResponse.FilterData
            {
                Act = "GetListCSSummary",
                ProjectID = "", // or ""
                BUID = "",                  // or ""
                CsName = "",
                UserID = 506
            };

            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu2 = listBu;

            //var result = _configProject.sp_GetDataCSResponse(filter);
            //var model = await _csResponseServic.GetListCountByCSAsync();

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }


        [HttpPost]
        public JsonResult GetListCSSummary([FromForm] SPGetDataCSResponse.FilterData filter)
        {
            filter.Act = "GetListCSSummary";
            var result = _configProject.sp_GetDataCSResponse(filter);
            return Json(new { success = true, data = result.CSSummary });
        }


        [HttpPost]
        public JsonResult GetListCountUnitStatus([FromForm] int UserID)
        {
            var filter = new SPGetDataCSResponse.FilterData
            {
                Act = "GetListCountUnitStatus",
                UserID = UserID
            };

            var result = _configProject.sp_GetDataCSResponse(filter);
            return Json(new { success = true, data = result.CountUnitStatus });
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
