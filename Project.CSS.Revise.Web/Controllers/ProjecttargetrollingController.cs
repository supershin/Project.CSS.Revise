using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Service;
using Project.CSS.Revise.Web.Commond;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjecttargetrollingController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IProjectAndTargetRollingService _projectAndTargetRollingService;
        public ProjecttargetrollingController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IProjectAndTargetRollingService projectAndTargetRollingService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _projectAndTargetRollingService = projectAndTargetRollingService;
        }

        public IActionResult Index()
        {
            var listBu = _masterService.GetlistBU(new BUModel());
            ViewBag.listBu = listBu;

            var filter = new GetDDLModel
            {
                Act = "Ext",
                ID = 32,

            };
            var ListPlanType = _masterService.GetlisDDl(filter);
            ViewBag.ListPlanType = ListPlanType;

            return View();
        }

        [HttpPost]
        public JsonResult GetProjectListByBU([FromForm] ProjectModel model)
        {
            var result = _masterService.GetlistPrject(model);
            return Json(new { success = true, data = result });
        }

        [HttpPost]
        public JsonResult GetDataTableProjectAndTargetRolling([FromForm] RollingPlanSummaryModel model)
        {
            var result = _projectAndTargetRollingService.GetListTargetRollingPlan(model);

            var filter = new RollingPlanTotalModel
            {
                L_Year = model.L_Year,
                L_Quarter = model.L_Quarter,
                L_Month = model.L_Month,
                L_ProjectID = model.L_ProjectID,
                L_Bu = model.L_Bu,
                L_PlanTypeID = model.L_PlanTypeID
            };

            var resultsum = _projectAndTargetRollingService.GetDataTotalTargetRollingPlan(filter);

            // ✅ Group & reshape datasum for summary cards
            var groupedSum = resultsum
                .GroupBy(x => new { x.PlanTypeName, x.COLORS })
                .Select(g => new
                {
                    PlanTypeName = g.Key.PlanTypeName,
                    ColorClass = g.Key.COLORS,
                    Unit = g.FirstOrDefault(x => x.PlanAmountName == "Unit")?.TOTAL ?? "",
                    Value = g.FirstOrDefault(x => x.PlanAmountName == "Value")?.TOTAL ?? ""
                })
                .ToList();

            return Json(new
            {
                success = true,
                data = result,
                datasum = groupedSum
            });
        }


        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
                
            var list = new List<ImportDataProjectTargetRolling>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++) // start from data row
                    {
                        var item = new ImportDataProjectTargetRolling
                        {
                            ProjectID = worksheet.Cells[row, 1].Text?.Trim(),
                            ProjectName = worksheet.Cells[row, 2].Text?.Trim(),
                            ProjectPlanType = worksheet.Cells[row, 3].Text?.Trim(),
                            Year = int.TryParse(worksheet.Cells[row, 4].Text, out var year) ? year : 0,

                            Jan_Unit = TryParseDecimal(worksheet.Cells[row, 5].Text),
                            Jan_Value = TryParseDecimal(worksheet.Cells[row, 6].Text),

                            Feb_Unit = TryParseDecimal(worksheet.Cells[row, 7].Text),
                            Feb_Value = TryParseDecimal(worksheet.Cells[row, 8].Text),

                            Mar_Unit = TryParseDecimal(worksheet.Cells[row, 9].Text),
                            Mar_Value = TryParseDecimal(worksheet.Cells[row, 10].Text),

                            Apr_Unit = TryParseDecimal(worksheet.Cells[row, 11].Text),
                            Apr_Value = TryParseDecimal(worksheet.Cells[row, 12].Text),

                            May_Unit = TryParseDecimal(worksheet.Cells[row, 13].Text),
                            May_Value = TryParseDecimal(worksheet.Cells[row, 14].Text),

                            Jun_Unit = TryParseDecimal(worksheet.Cells[row, 15].Text),
                            Jun_Value = TryParseDecimal(worksheet.Cells[row, 16].Text),

                            Jul_Unit = TryParseDecimal(worksheet.Cells[row, 17].Text),
                            Jul_Value = TryParseDecimal(worksheet.Cells[row, 18].Text),

                            Aug_Unit = TryParseDecimal(worksheet.Cells[row, 19].Text),
                            Aug_Value = TryParseDecimal(worksheet.Cells[row, 20].Text),

                            Sep_Unit = TryParseDecimal(worksheet.Cells[row, 21].Text),
                            Sep_Value = TryParseDecimal(worksheet.Cells[row, 22].Text),

                            Oct_Unit = TryParseDecimal(worksheet.Cells[row, 23].Text),
                            Oct_Value = TryParseDecimal(worksheet.Cells[row, 24].Text),

                            Nov_Unit = TryParseDecimal(worksheet.Cells[row, 25].Text),
                            Nov_Value = TryParseDecimal(worksheet.Cells[row, 26].Text),

                            Dec_Unit = TryParseDecimal(worksheet.Cells[row, 27].Text),
                            Dec_Value = TryParseDecimal(worksheet.Cells[row, 28].Text)
                        };

                        list.Add(item);
                    }
                }
            }

            string LoginID = User.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);
            int currentUserId = Commond.FormatExtension.Nulltoint(UserID);

            var Listdatainsert = BuildTargetRollingPlanList(list , currentUserId);

            var result = _projectAndTargetRollingService.UpsertTargetRollingPlans(Listdatainsert);

            if (result == null)
            {
                return BadRequest("Failed to insert data.");
            }
            // ✅ Return success response with count of inserted records

            return Ok(new { success = true, message = result.Message, count = list.Count });
        }

        // 🔧 Helper สำหรับแปลง string เป็น decimal แบบปลอดภัย
        private decimal? TryParseDecimal(string? value)
        {
            if (decimal.TryParse(value, out var result))
                return result;
            return null;
        }

        public List<TargetRollingPlanInsertModel> BuildTargetRollingPlanList(List<ImportDataProjectTargetRolling> importList, int currentUserId)
        {
            var result = new List<TargetRollingPlanInsertModel>();

            foreach (var row in importList)
            {
                // Map PlanType text to ID
                int PlanTypeID = GetPlanTypeId(row.ProjectPlanType);

                for (int month = 1; month <= 12; month++)
                {
                    // Unit
                    decimal? unit = GetMonthUnit(row, month);
                    if (unit.HasValue)
                    {
                        result.Add(new TargetRollingPlanInsertModel
                        {
                            ProjectID = row.ProjectID,
                            PlanTypeID = PlanTypeID,
                            PlanAmountID = Constants.Ext.Unit,
                            MonthlyDate = new DateTime(row.Year, month, 1),
                            Amount = unit.Value,
                            FlagActive = true,
                            CreateBy = currentUserId,
                            UpdateBy = currentUserId
                        });
                    }

                    // Value
                    decimal? value = GetMonthValue(row, month);
                    if (value.HasValue)
                    {
                        result.Add(new TargetRollingPlanInsertModel
                        {
                            ProjectID = row.ProjectID,
                            PlanTypeID = PlanTypeID,
                            PlanAmountID = Constants.Ext.Value,
                            MonthlyDate = new DateTime(row.Year, month, 1),
                            Amount = value.Value,
                            FlagActive = true,
                            CreateBy = currentUserId,
                            UpdateBy = currentUserId
                        });
                    }
                }
            }

            return result;
        }

        private int GetPlanTypeId(string? planTypeName)
        {
            if (string.IsNullOrWhiteSpace(planTypeName))
                return 0;

            switch (planTypeName.Trim())
            {
                case "Target":
                    return Constants.Ext.Target;
                case "Rolling":
                    return Constants.Ext.Rolling;
                case "Actual":
                    return Constants.Ext.Actual;
                case "Working Target":
                    return Constants.Ext.WorkingTarget;
                case "Working Rolling":
                    return Constants.Ext.WorkingRolling;
                case "MLL":
                    return Constants.Ext.MLL;
                default:
                    return 0; // unknown
            }
        }

        private decimal? GetMonthUnit(ImportDataProjectTargetRolling r, int month) =>
            month switch
            {
                1 => r.Jan_Unit,
                2 => r.Feb_Unit,
                3 => r.Mar_Unit,
                4 => r.Apr_Unit,
                5 => r.May_Unit,
                6 => r.Jun_Unit,
                7 => r.Jul_Unit,
                8 => r.Aug_Unit,
                9 => r.Sep_Unit,
                10 => r.Oct_Unit,
                11 => r.Nov_Unit,
                12 => r.Dec_Unit,
                _ => null
            };

        private decimal? GetMonthValue(ImportDataProjectTargetRolling r, int month) =>
            month switch
            {
                1 => r.Jan_Value,
                2 => r.Feb_Value,
                3 => r.Mar_Value,
                4 => r.Apr_Value,
                5 => r.May_Value,
                6 => r.Jun_Value,
                7 => r.Jul_Value,
                8 => r.Aug_Value,
                9 => r.Sep_Value,
                10 => r.Oct_Value,
                11 => r.Nov_Value,
                12 => r.Dec_Value,
                _ => null
            };


    }
}
