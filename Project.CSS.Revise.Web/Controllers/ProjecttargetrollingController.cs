using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            // ✅ Insert data
            //foreach (var item in list)
            //{
            //    await _yourService.InsertFromExcelAsync(item); // ปรับชื่อฟังก์ชันให้ตรงกับของพ่อใหญ่
            //}

            return Ok(new { success = true, count = list.Count });
        }

        // 🔧 Helper สำหรับแปลง string เป็น decimal แบบปลอดภัย
        private decimal? TryParseDecimal(string? value)
        {
            if (decimal.TryParse(value, out var result))
                return result;
            return null;
        }


    }
}
