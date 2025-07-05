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
                return Json(new { success = false, id = 0, message = "Event name, location, and at least one tag are required." });
            }
            if (string.IsNullOrEmpty(model.EventType) || string.IsNullOrEmpty(model.EventColor))
            {
                return Json(new { success = false, id = 0, message = "Event type and color are required." });
            }
            if (model.ProjectIds == null || model.ProjectIds.Count == 0)
            {
                return Json(new { success = false, id = 0, message = "At least one project must be selected." });
            }
            if (string.IsNullOrEmpty(model.StartDateTime) || string.IsNullOrEmpty(model.EndDateTime))
            {
                return Json(new { success = false, id = 0, message = "Start and end date/time are required." });
            }
            if (DateTime.TryParse(model.StartDateTime, out DateTime startDate) && DateTime.TryParse(model.EndDateTime, out DateTime endDate))
            {
                if (startDate >= endDate)
                {
                    return Json(new { success = false, id = 0, message = "Start date/time must be before end date/time." });
                }
            }
            else
            {
                return Json(new { success = false, id = 0, message = "Invalid date format." });
            }
            if (model.TagItems.Any(tag => string.IsNullOrEmpty(tag.Value) || string.IsNullOrEmpty(tag.Label)))
            {
                return Json(new { success = false, id = 0 , message = "All tags must have both value and label." });
            }

            string LoginID = User.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);

            model.UserID = Commond.FormatExtension.Nulltoint(UserID);

            var result = _shopAndEventService.CreateEventsAndTags(model);

            return Json(new { success = result.ID > 0 , id = result.ID, message = result.Message });
        }

        [HttpGet]
        public JsonResult GetDataTabShopFromInsert(int EventID)
        {

            var filter = new GetDDLModel
            {
                Act = "listEventdateByID",
                ID = EventID,
            };
            var listEventdate = _masterService.GetlisDDl(filter);

            var filter2 = new GetDDLModel
            {
                Act = "listEventProjectByID",
                ID = EventID,
            };
            var listEventproject = _masterService.GetlisDDl(filter2);

            var filter3 = new GetDDLModel
            {
                Act = "listShop"
            };
            var listShop = _masterService.GetlisDDl(filter3);

            var result = new
            {
                EventDates = listEventdate,
                EventProjects = listEventproject,
                Shops = listShop
            };

            return Json(result);
        }

        [HttpPost]
        public IActionResult InsertNewEventsAndShops([FromBody] CreateEvent_Shops model)
        {
            string LoginID = User.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);
            model.UserID = Commond.FormatExtension.Nulltoint(UserID);
            var result = _shopAndEventService.CreateEventsAndShops(model);
            return Json(new { success = result.ID, message = result.Message });
        }
    }
}
