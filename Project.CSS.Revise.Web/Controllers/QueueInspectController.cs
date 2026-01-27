using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.CSS.Revise.Web.Hubs;
using Project.CSS.Revise.Web.Library.BLL;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.QueueInspect;
using Project.CSS.Revise.Web.Service;
using System.Globalization;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class QueueInspectController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserAndPermissionService _userAndPermissionService;
        private readonly MasterManagementConfigQueueInspect _configQueueInspec;
        private readonly IHubContext<NotifyHub> _notifyHubContext;
        public QueueInspectController(IHttpContextAccessor httpContextAccessor
            , IMasterService masterService
            , IUserAndPermissionService userAndPermissionService
            , MasterManagementConfigQueueInspect configQueueInspec
            , IHubContext<NotifyHub> notifyHubContext) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userAndPermissionService = userAndPermissionService;
            _configQueueInspec = configQueueInspec;
            _notifyHubContext = notifyHubContext;
        }
        public IActionResult Index()
        {
            ViewBag.listBu = _masterService.GetlistBU(new BUModel());

            var allProject = _masterService.GetlisDDl(new GetDDLModel { Act = "listDDlAllProject" });
            ViewBag.listDDlAllProject = allProject;
            ViewBag.listProject = allProject;

            var listExpecttransfer = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 13 });
            ViewBag.listExpecttransfer = listExpecttransfer;

            var listUnitstatuscs = _masterService.GetlisDDl(new GetDDLModel { Act = "Ext", ID = 16 });
            ViewBag.listUnitstatuscs = listUnitstatuscs;

            var listgCSRespons = _masterService.GetlisDDl(new GetDDLModel { Act = "listAllCSUser" });
            ViewBag.listgCSRespons = listgCSRespons;

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetUnitListByProject([FromForm] UnitModel model)
        {
            var result = _masterService.GetlistUnitByProject(model);
            return Json(new { success = true, data = result });
        }



        [HttpPost]
        public async Task<JsonResult> GetlistDataQueueInspect([FromForm] QueueInspectModel.FiltersModel model)
        {
            var tableFilter = new QueueInspectModel.FiltersModel
            {
                Act = "RegisterQueueInspectTable",
                Bu = model.Bu,
                ProjectID = model.ProjectID,
                RegisterDateStart = model.RegisterDateStart,
                RegisterDateEnd = model.RegisterDateEnd,
                UnitID = model.UnitID,
                Inspect_Round = model.Inspect_Round,
                CSResponse = model.CSResponse,
                UnitCS = model.UnitCS,
                ExpectTransfer = model.ExpectTransfer,
                Start = model.Start,
                Length = model.Length,
                QueueTypeID = 49,
                SearchText = model.SearchText
            };

            var summaryFilter = new QueueInspectModel.FiltersModel
            {
                Act = "RegisterQueueInspectSummary",
                Bu = model.Bu,
                ProjectID = model.ProjectID,
                RegisterDateStart = model.RegisterDateStart,
                RegisterDateEnd = model.RegisterDateEnd,
                UnitID = model.UnitID,
                Inspect_Round = model.Inspect_Round,
                CSResponse = model.CSResponse,
                UnitCS = model.UnitCS,
                ExpectTransfer = model.ExpectTransfer,
                Start = model.Start,
                Length = model.Length,
                QueueTypeID = 49,
                SearchText = model.SearchText
            };

            var CheckingFilter = new QueueInspectModel.FiltersModel
            {
                Act = "RegisterQueueCheckingSummary",
                Bu = model.Bu,
                ProjectID = model.ProjectID,
                RegisterDateStart = model.RegisterDateStart,
                RegisterDateEnd = model.RegisterDateEnd,
                UnitID = model.UnitID,
                Inspect_Round = model.Inspect_Round,
                CSResponse = model.CSResponse,
                UnitCS = model.UnitCS,
                ExpectTransfer = model.ExpectTransfer,
                Start = model.Start,
                Length = model.Length,
                QueueTypeID = 49,
                SearchText = model.SearchText
            };

            var TraferFilter = new QueueInspectModel.FiltersModel
            {
                Act = "RegisterQueueTransferTypeSummary",
                Bu = model.Bu,
                ProjectID = model.ProjectID,
                RegisterDateStart = model.RegisterDateStart,
                RegisterDateEnd = model.RegisterDateEnd,
                UnitID = model.UnitID,
                Inspect_Round = model.Inspect_Round,
                CSResponse = model.CSResponse,
                UnitCS = model.UnitCS,
                ExpectTransfer = model.ExpectTransfer,
                Start = model.Start,
                Length = model.Length,
                QueueTypeID = 49,
                SearchText = model.SearchText
            };

            try
            {
                // Run both tasks in parallel and await them
                var tableTask = Task.Run(() => _configQueueInspec.sp_GetQueueInspect(tableFilter));
                var summaryTask = Task.Run(() => _configQueueInspec.sp_GetQueueInspect(summaryFilter));
                var CheckingTask = Task.Run(() => _configQueueInspec.sp_GetQueueInspect(CheckingFilter));
                var TraferTask = Task.Run(() => _configQueueInspec.sp_GetQueueInspect(TraferFilter));

                await Task.WhenAll(tableTask, summaryTask, CheckingTask, TraferTask);

                var tableResult = tableTask.Result;
                var summaryResult = summaryTask.Result;
                var checkingResult = CheckingTask.Result;
                var traferResult = TraferTask.Result;

                var rows = tableResult?.ListRegisterQueueInspectTable ?? new List<QueueInspectModel.RegisterQueueInspectTableModel>();
                var summary = summaryResult?.ListRegisterQueueInspectSummary ?? new List<QueueInspectModel.RegisterQueueInspectSummaryModel>();
                var checking = checkingResult?.ListRegisterQueueCheckingSummary ?? new List<QueueInspectModel.RegisterQueueCheckingSummaryModel>();
                var trafer = traferResult?.ListRegisterQueueTransferTypeSummary ?? new List<QueueInspectModel.RegisterQueueTransferTypeSummaryModel>();

                var recordsTotal = rows.Count > 0 ? ToIntSafe(rows[0].TotalRecords) : 0;
                var recordsFiltered = rows.Count > 0 ? ToIntSafe(rows[0].FilteredRecords) : recordsTotal;

                return Json(new
                {
                    draw = model.Draw,   
                    recordsTotal,
                    recordsFiltered,
                    data = rows,
                    data2 = summary,
                    data3 = checking,
                    data4 = trafer
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    draw = model.Draw,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<object>(),
                    error = ex.Message
                });
            }
        }

        private static int ToIntSafe(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;

            s = s.Trim();

            // 34,470.00 -> 34470
            if (decimal.TryParse(
                    s,
                    NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out var dec))
            {
                return (int)dec;
            }

            // fallback
            s = s.Replace(",", "");
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
                return i;

            return 0;
        }

    }
}
