using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class OtherSettingsController : BaseController
    {
        private readonly IMasterService _masterService;

        private readonly IShopAndEventService _shopAndEventService;
        public OtherSettingsController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IShopAndEventService shopAndEventService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _shopAndEventService = shopAndEventService;
        }

        public IActionResult Index()
        {
            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;


            var filterDDlAllProject = new GetDDLModel
            {
                Act = "listDDlAllProject"
            };
            var listDDlAllProject = _masterService.GetlisDDl(filterDDlAllProject);
            ViewBag.listDDlAllProject = listDDlAllProject;

            var filter = new GetDDLModel
            {
                Act = "listAlltag"
            };
            var Listtag = _masterService.GetlisDDl(filter);
            ViewBag.Listtag = Listtag;

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

        [HttpGet]
        public IActionResult LoadPartial(string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                return BadRequest("View name cannot be null or empty.");
            }
            return PartialView(viewName);
        }


        [HttpGet]
        public IActionResult LoadPartialshopevent(string projectID, string daterang)
        {
            string? startDate = null;
            string? endDate = null;

            //if (!string.IsNullOrWhiteSpace(daterang) && daterang.Contains(" - "))
            //{
            //    var parts = daterang.Split(" - ");
            //    if (parts.Length == 2)
            //    {
            //        // 👇 แปลงเป็น yyyy-MM-dd เพื่อให้ SQL อ่านง่าย
            //        startDate = DateTime.ParseExact(parts[0], "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
            //        endDate = DateTime.ParseExact(parts[1], "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
            //    }
            //}

            //var filter = new EventsModel
            //{
            //    L_ProjectID = projectID,
            //    L_Startdate = startDate,
            //    L_Enddate = endDate
            //};

            //var listEvents = _masterService.GetlistEvents(filter);

            return PartialView("Partial_shop_event");

        }

        [HttpGet]
        public JsonResult GetlistCountEventByMonth(string projectID, string year)
        {
            var filter = new Monthevents
            {
                L_ProjectID = projectID,
                L_year = year
            };

            var listCountEvents = _masterService.GetlistCountEventByMonth(filter);
            return Json(listCountEvents);
        }

        [HttpGet]
        public JsonResult GetEventsForCalendar(string projectID, string month, string year)
        {
            var filter = new EventsModel
            {
                L_ProjectID = projectID,
                L_month = month,
                L_year = year                
            };

            var listEvents = _masterService.GetlistEvents(filter);
            return Json(listEvents);
        }

        [HttpPost]
        public IActionResult InsertNewEventsAndtags([FromBody] CreateEvents_Tags model)
        {

            if (string.IsNullOrEmpty(model.EventName) || string.IsNullOrEmpty(model.EventLocation) || model.TagItems == null || model.TagItems.Count == 0)
            {
                return Json(new { success = false, message = "Event name, location, and at least one tag are required." });
            }
            if (model.ProjectIds == null || model.ProjectIds.Count == 0)
            {
                return Json(new { success = false, message = "At least one project must be selected." });
            }
            if (string.IsNullOrEmpty(model.StartDateTime) || string.IsNullOrEmpty(model.EndDateTime))
            {
                return Json(new { success = false, message = "Start and end date/time are required." });
            }
            if (DateTime.TryParse(model.StartDateTime, out DateTime startDate) && DateTime.TryParse(model.EndDateTime, out DateTime endDate))
            {
                if (startDate >= endDate)
                {
                    return Json(new { success = false, message = "Start date/time must be before end date/time." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid date format." });
            }
            if (model.TagItems.Any(tag => string.IsNullOrEmpty(tag.Value) || string.IsNullOrEmpty(tag.Label)))
            {
                return Json(new { success = false, message = "All tags must have both value and label." });
            }

            string LoginID = User.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);

            model.UserID = Commond.FormatExtension.Nulltoint(UserID);

            var result = _shopAndEventService.CreateEventsAndTags(model);

            return Json(new { success = result.ID > 0, message = result.Message });
        }


    }
}
