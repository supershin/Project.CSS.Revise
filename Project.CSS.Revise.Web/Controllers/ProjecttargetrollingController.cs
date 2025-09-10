using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Service;
using System.Data;
using System.Drawing;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjecttargetrollingController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IProjectAndTargetRollingService _projectAndTargetRollingService;
        private readonly MasterManagementConfigProject _configProject;
        public ProjecttargetrollingController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IProjectAndTargetRollingService projectAndTargetRollingService, MasterManagementConfigProject configProject) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _projectAndTargetRollingService = projectAndTargetRollingService;
            _configProject = configProject;
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

            var filter2 = new GetDDLModel
            {
                Act = "Ext",
                ID = 54,

            };
            var ListProjectStatus = _masterService.GetlisDDl(filter2);
            ViewBag.ListProjectStatus = ListProjectStatus;

            var filter3 = new GetDDLModel
            {
                Act = "Ext",
                ID = 59,

            };
            var ListProjectPartner = _masterService.GetlisDDl(filter3);
            ViewBag.ListProjectPartner = ListProjectPartner;

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
            // Fix: The method returns a List<RollingPlanSummaryModel>, not a single RollingPlanSummaryModel
            model.L_Act = "GetListTargetRollingPlan";
            List<RollingPlanSummaryModel> result = _configProject.sp_GetProjecTargetRollingPlanList_GetListTargetRollingPlan(model);

            var filter = new RollingPlanTotalModel
            {
                L_Act = "GetDataTotalTargetRollingPlan",
                L_Year = model.L_Year,
                L_Quarter = model.L_Quarter,
                L_Month = model.L_Month,
                L_ProjectID = model.L_ProjectID,
                L_Bu = model.L_Bu,
                L_PlanTypeID = model.L_PlanTypeID,
                L_ProjectStatus = model.L_ProjectStatus,
                L_ProjectPartner = model.L_ProjectPartner
            };

            //var resultsum = _projectAndTargetRollingService.GetDataTotalTargetRollingPlan(filter);
            List<RollingPlanTotalModel> resultsum = _configProject.sp_GetProjecTargetRollingPlanList_GetDataTotalTargetRollingPlan(filter);

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
                            ProjectPlanType = worksheet.Cells[row, 4].Text?.Trim(),
                            Year = int.TryParse(worksheet.Cells[row, 5].Text, out var year) ? year : 0,

                            Jan_Unit = TryParseDecimal(worksheet.Cells[row, 6].Text),
                            Jan_Value = TryParseDecimal(worksheet.Cells[row, 7].Text),

                            Feb_Unit = TryParseDecimal(worksheet.Cells[row, 8].Text),
                            Feb_Value = TryParseDecimal(worksheet.Cells[row, 9].Text),

                            Mar_Unit = TryParseDecimal(worksheet.Cells[row, 10].Text),
                            Mar_Value = TryParseDecimal(worksheet.Cells[row, 11].Text),

                            Apr_Unit = TryParseDecimal(worksheet.Cells[row, 12].Text),
                            Apr_Value = TryParseDecimal(worksheet.Cells[row, 13].Text),

                            May_Unit = TryParseDecimal(worksheet.Cells[row, 14].Text),
                            May_Value = TryParseDecimal(worksheet.Cells[row, 15].Text),

                            Jun_Unit = TryParseDecimal(worksheet.Cells[row, 16].Text),
                            Jun_Value = TryParseDecimal(worksheet.Cells[row, 17].Text),

                            Jul_Unit = TryParseDecimal(worksheet.Cells[row, 18].Text),
                            Jul_Value = TryParseDecimal(worksheet.Cells[row, 19].Text),

                            Aug_Unit = TryParseDecimal(worksheet.Cells[row, 20].Text),
                            Aug_Value = TryParseDecimal(worksheet.Cells[row, 21].Text),

                            Sep_Unit = TryParseDecimal(worksheet.Cells[row, 22].Text),
                            Sep_Value = TryParseDecimal(worksheet.Cells[row, 23].Text),

                            Oct_Unit = TryParseDecimal(worksheet.Cells[row, 24].Text),
                            Oct_Value = TryParseDecimal(worksheet.Cells[row, 25].Text),

                            Nov_Unit = TryParseDecimal(worksheet.Cells[row, 26].Text),
                            Nov_Value = TryParseDecimal(worksheet.Cells[row, 27].Text),

                            Dec_Unit = TryParseDecimal(worksheet.Cells[row, 28].Text),
                            Dec_Value = TryParseDecimal(worksheet.Cells[row, 29].Text)
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

        [HttpPost]
        public IActionResult ExportProjectAndTargetRolling([FromForm] RollingPlanSummaryModel model)
        {
            try
            {
                var dataList = _projectAndTargetRollingService.GetListDataExportTargetRollingPlan(model);
                var dataTable = ConvertToDataTable(dataList);

                var excelBytes = WriteExcelProjectAndTargetRolling(dataTable);

                // 👇 ใช้ L_PlanTypeName สร้างชื่อไฟล์
                var fileName = BuildExportFileName(model);

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error fetching data: " + ex.Message });
            }
        }

        private static string BuildExportFileName(RollingPlanSummaryModel model)
        {
            // เวลาในรูปแบบที่พ่อใหญ่ยกตัวอย่าง
            var ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            // กำหนดลำดับและ mapping ตัวย่อ
            var order = new[] { "Target", "Rolling", "Actual", "WorkingTarget", "WorkingRolling", "MLL" };

            // รองรับชื่อที่อาจมีเว้นวรรค เช่น "Working Target", "Working Rolling"
            // key = ชื่อแบบ normalize (ตัดช่องว่าง, lower)
            string Norm(string s) => (s ?? "").Replace(" ", "").ToLowerInvariant();

            var displayName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "target", "Target" },
                { "rolling", "Rolling" },
                { "actual", "Actual" },
                { "workingtarget", "WorkingTarget" },
                { "workingrolling", "WorkingRolling" },
                { "mll", "MLL" }
            };

            var shortCode = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "target", "T" },
                { "rolling", "R" },
                { "actual", "A" },
                { "workingtarget", "WT" },
                { "workingrolling", "WR" },
                { "mll", "MLL" }
            };

            // แยกชื่อจาก L_PlanTypeName
            var raw = model?.L_PlanTypeName ?? string.Empty;
            var tokens = raw
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(Norm)
                .Distinct()
                .Where(displayName.ContainsKey)       // เก็บเฉพาะที่อยู่ในชุด 6 ชื่อ
                .ToList();

            // ว่าง หรือครบทั้ง 6 ชื่อ => ALL
            if (tokens.Count == 0 || tokens.Count == 6)
                return $"ALL_{ts}.xlsx";

            // 1 ชื่อ => ใช้ชื่อเต็ม
            if (tokens.Count == 1)
            {
                var norm = tokens[0];
                var full = displayName[norm]; // ชื่อเต็มตามมาตรฐาน
                return $"{full}_{ts}.xlsx";
            }

            // หลายชื่อ => ใช้ตัวย่อ เรียงตามลำดับมาตรฐาน
            var normOrder = order.Select(Norm).ToList();
            var codes = new List<string>();
            for (int i = 0; i < normOrder.Count; i++)
            {
                var key = normOrder[i];
                if (tokens.Contains(key))
                    codes.Add(shortCode[key]);
            }

            // ถ้าบังเอิญ tokens ครบ 6 จากทางอ้อม ให้เป็น ALL
            if (codes.Count == 6)
                return $"ALL_{ts}.xlsx";

            return $"{string.Join("_", codes)}_{ts}.xlsx";
        }

        private static object ToDbNullableDecimal(object? v)
        {
            if (v == null) return DBNull.Value;
            if (v is string s && string.IsNullOrWhiteSpace(s)) return DBNull.Value;
            if (v is decimal d) return d;
            if (v is double db) return Convert.ToDecimal(db);
            if (v is int i) return Convert.ToDecimal(i);
            if (decimal.TryParse(Convert.ToString(v), out var dec)) return dec;
            return DBNull.Value;
        }

        private DataTable ConvertToDataTable(List<RollingPlanSummaryModel> dataList)
        {
            var dt = new DataTable();

            // Fixed columns
            dt.Columns.Add("ProjectID", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("Bu", typeof(string));
            dt.Columns.Add("ProjectPlanType", typeof(string));
            dt.Columns.Add("Year", typeof(int));

            // Month columns
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            foreach (var m in months)
            {
                dt.Columns.Add($"{m}_Unit", typeof(decimal));
                dt.Columns.Add($"{m}_Value", typeof(decimal));
            }

            // ✅ เตรียมคอลัมน์ TOTAL (จะใส่สูตรใน Excel ตอนเขียนไฟล์)
            dt.Columns.Add("Total_Unit", typeof(decimal));
            dt.Columns.Add("Total_Value", typeof(decimal));

            foreach (var item in dataList)
            {
                var row = dt.NewRow();

                row["ProjectID"] = item.ProjectID ?? (object)DBNull.Value;
                row["ProjectName"] = item.ProjectName ?? (object)DBNull.Value;
                row["Bu"] = item.BuName ?? (object)DBNull.Value;
                row["ProjectPlanType"] = item.PlanTypeName ?? (object)DBNull.Value;
                row["Year"] = int.TryParse(item.PlanYear, out var y) ? y : (object)DBNull.Value;

                // Map month values (แปลง null/"" เป็น DBNull เพื่อไปโชว์ "-" ใน Excel)
                row["Jan_Unit"] = ToDbNullableDecimal(item.Jan_Unit);
                row["Jan_Value"] = ToDbNullableDecimal(item.Jan_Value);

                row["Feb_Unit"] = ToDbNullableDecimal(item.Feb_Unit);
                row["Feb_Value"] = ToDbNullableDecimal(item.Feb_Value);

                row["Mar_Unit"] = ToDbNullableDecimal(item.Mar_Unit);
                row["Mar_Value"] = ToDbNullableDecimal(item.Mar_Value);

                row["Apr_Unit"] = ToDbNullableDecimal(item.Apr_Unit);
                row["Apr_Value"] = ToDbNullableDecimal(item.Apr_Value);

                row["May_Unit"] = ToDbNullableDecimal(item.May_Unit);
                row["May_Value"] = ToDbNullableDecimal(item.May_Value);

                row["Jun_Unit"] = ToDbNullableDecimal(item.Jun_Unit);
                row["Jun_Value"] = ToDbNullableDecimal(item.Jun_Value);

                row["Jul_Unit"] = ToDbNullableDecimal(item.Jul_Unit);
                row["Jul_Value"] = ToDbNullableDecimal(item.Jul_Value);

                row["Aug_Unit"] = ToDbNullableDecimal(item.Aug_Unit);
                row["Aug_Value"] = ToDbNullableDecimal(item.Aug_Value);

                row["Sep_Unit"] = ToDbNullableDecimal(item.Sep_Unit);
                row["Sep_Value"] = ToDbNullableDecimal(item.Sep_Value);

                row["Oct_Unit"] = ToDbNullableDecimal(item.Oct_Unit);
                row["Oct_Value"] = ToDbNullableDecimal(item.Oct_Value);

                row["Nov_Unit"] = ToDbNullableDecimal(item.Nov_Unit);
                row["Nov_Value"] = ToDbNullableDecimal(item.Nov_Value);

                row["Dec_Unit"] = ToDbNullableDecimal(item.Dec_Unit);
                row["Dec_Value"] = ToDbNullableDecimal(item.Dec_Value);

                // TOTAL จะคำนวณใน Excel => ปล่อยว่างไว้
                row["Total_Unit"] = DBNull.Value;
                row["Total_Value"] = DBNull.Value;

                dt.Rows.Add(row);
            }

            return dt;
        }

        private byte[] WriteExcelProjectAndTargetRolling(DataTable dt)
        {
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Sheet 1");
                if (dt == null || dt.Columns.Count == 0)
                    throw new Exception("No columns found in DataTable.");

                string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

                int r1 = 1; // header row 1
                int r2 = 2; // header row 2
                int c = 1;

                // ===== Row 1: Fixed headers (5 columns now) =====
                ws.Cells[r1, c].Value = "ProjectID"; c++;
                ws.Cells[r1, c].Value = "Project Name"; c++;
                ws.Cells[r1, c].Value = "Bu"; c++;
                ws.Cells[r1, c].Value = "Project Plan Type"; c++;
                ws.Cells[r1, c].Value = "Year"; c++;

                // ===== Row 1: Month groups (merge Unit/Value) =====
                foreach (var m in months)
                {
                    ws.Cells[r1, c].Value = m;
                    ws.Cells[r1, c, r1, c + 1].Merge = true; // Unit + Value
                    c += 2;
                }

                // ===== Row 1: TOTAL (merge 2 cols) =====
                ws.Cells[r1, c].Value = "TOTAL";
                ws.Cells[r1, c, r1, c + 1].Merge = true;
                int totalUnitCol = c;       // first of TOTAL (Unit)
                int totalValueCol = c + 1;  // second of TOTAL (Value)
                c += 2;

                // ===== Row 2: Subheaders =====
                int c2 = 1;
                ws.Cells[r2, c2++].Value = ""; // ProjectID
                ws.Cells[r2, c2++].Value = ""; // Project Name
                ws.Cells[r2, c2++].Value = ""; // Bu
                ws.Cells[r2, c2++].Value = ""; // Project Plan Type
                ws.Cells[r2, c2++].Value = ""; // Year
                foreach (var _ in months)
                {
                    ws.Cells[r2, c2++].Value = "Unit";
                    ws.Cells[r2, c2++].Value = "Value";
                }
                ws.Cells[r2, c2++].Value = "Unit";   // TOTAL Unit
                ws.Cells[r2, c2++].Value = "Value";  // TOTAL Value

                // ===== Header styling =====
                using (var rngTop = ws.Cells[r1, 1, r1, c - 1])
                {
                    rngTop.Style.Font.Bold = true;
                    rngTop.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    rngTop.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    rngTop.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rngTop.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#bfbfbf"));
                    rngTop.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Gray);
                }
                using (var rngSub = ws.Cells[r2, 1, r2, c - 1])
                {
                    rngSub.Style.Font.Bold = true;
                    rngSub.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    rngSub.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    rngSub.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rngSub.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#e6e6e6"));
                    rngSub.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Gray);
                }

                ws.Row(r1).Height = 22;
                ws.Row(r2).Height = 20;

                // Freeze below headers & after Year (Year is col 5, so freeze at 6)
                ws.View.FreezePanes(3, 6);

                // helper
                string CellAddr(int row, int col) => ws.Cells[row, col].Address;

                // ===== Data rows =====
                int dataStartRow = 3;
                int totalCols = c - 1;        // last used column index
                int firstUnitCol = 6;         // after 5 fixed cols, Unit starts at col 6
                int lastValueCol = 5 + months.Length * 2; // last month Value col

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    int excelRow = dataStartRow + i;
                    int dc = 1;

                    // Fixed cols (match DataTable column names)
                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("ProjectID") ? dr["ProjectID"] : null;
                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("ProjectName") ? dr["ProjectName"] : null;

                    // ✅ BU: use "Bu" (not "BuName")
                    object bu = dr.Table.Columns.Contains("Bu") ? dr["Bu"] : null;
                    // ถ้า BU เป็นค่าว่าง "" ให้ใส่ช่องว่างหนึ่งตัว (ป้องกัน Excel ตีความอะไรแปลก ๆ)
                    if (bu == null || bu == DBNull.Value || (bu is string sBu && string.IsNullOrWhiteSpace(sBu)))
                        bu = " ";
                    ws.Cells[excelRow, dc++].Value = bu;

                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("ProjectPlanType") ? dr["ProjectPlanType"] : null;
                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("Year") ? dr["Year"] : null;

                    // Month Unit/Value (use "-" for nulls)
                    foreach (var m in months)
                    {
                        var unitColName = m + "_Unit";
                        var valueColName = m + "_Value";

                        if (dr.Table.Columns.Contains(unitColName) && dr[unitColName] != DBNull.Value)
                            ws.Cells[excelRow, dc].Value = dr[unitColName];
                        else
                            ws.Cells[excelRow, dc].Value = "-";
                        dc++;

                        if (dr.Table.Columns.Contains(valueColName) && dr[valueColName] != DBNull.Value)
                            ws.Cells[excelRow, dc].Value = dr[valueColName];
                        else
                            ws.Cells[excelRow, dc].Value = "-";
                        dc++;
                    }

                    // Per-row TOTAL formulas
                    var unitCells = new List<string>();
                    for (int col = firstUnitCol; col <= lastValueCol; col += 2)
                        unitCells.Add(CellAddr(excelRow, col));
                    ws.Cells[excelRow, totalUnitCol].Formula = $"=SUM({string.Join(",", unitCells)})";

                    var valueCells = new List<string>();
                    for (int col = firstUnitCol + 1; col <= lastValueCol; col += 2)
                        valueCells.Add(CellAddr(excelRow, col));
                    ws.Cells[excelRow, totalValueCol].Formula = $"=SUM({string.Join(",", valueCells)})";
                }

                // ===== Column widths (update for 5 fixed cols) =====
                ws.Column(1).Width = 15;  // ProjectID
                ws.Column(2).Width = 28;  // Project Name
                ws.Column(3).Width = 18;  // Bu  (เพิ่มความกว้างกัน "####")
                ws.Column(4).Width = 20;  // Project Plan Type
                ws.Column(5).Width = 10;  // Year

                // Unit columns
                for (int col = firstUnitCol; col <= lastValueCol; col += 2)
                    ws.Column(col).Width = 7;
                ws.Column(totalUnitCol).Width = 7;

                // Value columns
                for (int col = firstUnitCol + 1; col <= lastValueCol; col += 2)
                    ws.Column(col).Width = 30;
                ws.Column(totalValueCol).Width = 30;

                // ===== Number formats =====
                for (int col = firstUnitCol + 1; col <= lastValueCol; col += 2)
                    ws.Column(col).Style.Numberformat.Format = "#,##0.00";   // Value
                ws.Column(totalValueCol).Style.Numberformat.Format = "#,##0.00";

                for (int col = firstUnitCol; col <= lastValueCol; col += 2)
                    ws.Column(col).Style.Numberformat.Format = "#,##0";      // Unit (integer)
                ws.Column(totalUnitCol).Style.Numberformat.Format = "#,##0";

                // Center headers & wrap
                ws.Cells[r1, 1, r2, totalCols].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells[r1, 1, r2, totalCols].Style.WrapText = true;

                // ===== GRAND TOTAL row =====
                int lastDataRow = dataStartRow + dt.Rows.Count - 1;
                int grandTotalRow = Math.Max(dataStartRow, lastDataRow + 1);

                ws.Cells[grandTotalRow, 1].Value = "GRAND TOTAL";
                ws.Cells[grandTotalRow, 1, grandTotalRow, 5].Merge = true; // merge over 5 fixed cols
                ws.Cells[grandTotalRow, 1, grandTotalRow, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                if (dt.Rows.Count > 0)
                {
                    // Subtotal down each month column (Unit/Value)
                    for (int i = 0; i < months.Length; i++)
                    {
                        int unitCol = firstUnitCol + (i * 2);
                        int valueCol = unitCol + 1;

                        ws.Cells[grandTotalRow, unitCol].Formula =
                            $"=SUBTOTAL(9,{ws.Cells[dataStartRow, unitCol].Address}:{ws.Cells[lastDataRow, unitCol].Address})";

                        ws.Cells[grandTotalRow, valueCol].Formula =
                            $"=SUBTOTAL(9,{ws.Cells[dataStartRow, valueCol].Address}:{ws.Cells[lastDataRow, valueCol].Address})";
                    }

                    // Subtotal TOTAL Unit / TOTAL Value
                    ws.Cells[grandTotalRow, totalUnitCol].Formula =
                        $"=SUBTOTAL(9,{ws.Cells[dataStartRow, totalUnitCol].Address}:{ws.Cells[lastDataRow, totalUnitCol].Address})";

                    ws.Cells[grandTotalRow, totalValueCol].Formula =
                        $"=SUBTOTAL(9,{ws.Cells[dataStartRow, totalValueCol].Address}:{ws.Cells[lastDataRow, totalValueCol].Address})";
                }
                else
                {
                    for (int col = firstUnitCol; col <= totalValueCol; col++)
                        ws.Cells[grandTotalRow, col].Value = 0;
                }


                // Style the GRAND TOTAL row
                using (var rngGrand = ws.Cells[grandTotalRow, 1, grandTotalRow, totalCols])
                {
                    rngGrand.Style.Font.Bold = true;
                    rngGrand.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rngGrand.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#dcdcdc"));
                    rngGrand.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rngGrand.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rngGrand.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rngGrand.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rngGrand.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }
                ws.Row(grandTotalRow).Height = 20;

                // AutoFilter
                ws.Cells[r2, 1, Math.Max(grandTotalRow, r2), totalCols].AutoFilter = true;

                return package.GetAsByteArray();
            }
        }


        [HttpPost]
        public IActionResult UpsertEdits([FromBody] List<UpsertEditDto> edits)
        {
            try
            {
                if (edits == null || edits.Count == 0)
                    return Ok(new { success = false, message = "No edits received." });

                // Get current user id (adjust to your auth)
                var loginId = User.FindFirst("LoginID")?.Value;
                var userIdStr = SecurityManager.DecodeFrom64(loginId ?? "");
                var currentUserId = Commond.FormatExtension.Nulltoint(userIdStr);

                // Map to your insert model
                var plans = new List<Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling.TargetRollingPlanInsertModel>();
                foreach (var e in edits)
                {
                    // guard
                    if (string.IsNullOrWhiteSpace(e.ProjectID) || e.PlanTypeID <= 0 || e.Year <= 0 || e.Month < 1 || e.Month > 12)
                        continue;

                    var monthDate = new DateTime(e.Year, e.Month, 1);

                    // If NewValue is null, treat as zero and mark inactive (you can change this rule)
                    var isActive = e.NewValue.HasValue;
                    var amount = e.NewValue ?? 0m;

                    plans.Add(new Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling.TargetRollingPlanInsertModel
                    {
                        ProjectID = e.ProjectID,
                        PlanTypeID = e.PlanTypeID,
                        PlanAmountID = e.PlanAmountID,      // 183 unit / 184 value
                        MonthlyDate = monthDate,
                        Amount = amount,
                        FlagActive = isActive,
                        CreateBy = currentUserId,
                        UpdateBy = currentUserId
                    });
                }

                if (plans.Count == 0)
                    return Ok(new { success = false, message = "All edits were invalid after validation." });

                // Call your EF upsert service
                var summary = _projectAndTargetRollingService.UpsertTargetRollingPlans(plans);

                return Ok(new
                {
                    success = true,
                    inserted = summary.Inserted,
                    updated = summary.Updated,
                    skipped = summary.Skipped,
                    message = summary.Message
                });
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                return BadRequest(new
                {
                    success = false,
                    message = $"UpsertEdits failed: {ex.Message}" + (inner != null ? $" | INNER: {inner}" : "")
                });
            }
        }

    }
}
