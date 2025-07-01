using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public OtherSettingsController(IHttpContextAccessor httpContextAccessor, IMasterService masterService): base(httpContextAccessor)
        {
            _masterService = masterService;
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
            //if (string.IsNullOrWhiteSpace(model.EventName))
            //    return BadRequest("Event Name is required.");

            //// 💡 Insert Event to tr_Event table
            //var newEvent = new tr_Event
            //{
            //    ID = Guid.NewGuid(),
            //    Name = model.EventName,
            //    Location = model.EventLocation,
            //    StartDate = Convert.ToDateTime(model.StartDateTime),
            //    EndDate = Convert.ToDateTime(model.EndDateTime),
            //    FlagActive = model.IsActive,
            //    CreateDate = DateTime.Now,
            //    CreateBy = "System"
            //};
            //_context.tr_Events.Add(newEvent);
            //_context.SaveChanges();

            //// 💡 Save project mappings (e.g., tr_EventProject)
            //foreach (var projectId in model.ProjectIds)
            //{
            //    _context.tr_EventProjects.Add(new tr_EventProject
            //    {
            //        ID = Guid.NewGuid(),
            //        EventID = newEvent.ID,
            //        ProjectID = Guid.Parse(projectId),
            //        CreateDate = DateTime.Now,
            //        CreateBy = "System"
            //    });
            //}

            //// 💡 Insert new tags (check existence first)
            //foreach (var tag in model.TagItems)
            //{
            //    var exists = _context.tm_Tags.Any(t => t.Name == tag.Value && t.FlagActive);
            //    if (!exists)
            //    {
            //        var newTag = new tm_Tag
            //        {
            //            Name = tag.Value,
            //            FlagActive = true,
            //            CreateDate = DateTime.Now,
            //            CreateBy = "System"
            //        };
            //        _context.tm_Tags.Add(newTag);
            //    }
            //}

            //_context.SaveChanges();

            return Ok(new { success = true, message = "Event saved successfully." });
        }


    }
}
