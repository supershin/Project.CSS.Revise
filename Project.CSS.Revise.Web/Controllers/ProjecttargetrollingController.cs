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

        /// <summary>
        /// ตั้งชื่อไฟล์จาก L_PlanTypeName ตามกติกา:
        /// - 1 ชื่อ  => "Target_yyyy-MM-dd HH:mm.xlsx"
        /// - หลายชื่อ => "T_R_WT_WR_MLL_yyyy-MM-dd HH:mm.xlsx" (ตามลำดับมาตรฐาน)
        /// - ว่าง หรือ ครบทั้ง 6 => "ALL_yyyy-MM-dd HH:mm.xlsx"
        /// </summary>
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


        // ✅ ช่วยแปลงค่าให้เป็น DBNull เมื่อเป็น null / "" / แปลงไม่ได้
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

                int r1 = 1; // first header row
                int r2 = 2; // second header row
                int c = 1;

                // Fixed headers row 1
                ws.Cells[r1, c].Value = "ProjectID"; c++;
                ws.Cells[r1, c].Value = "Project Name"; c++;
                ws.Cells[r1, c].Value = "Project Plan Type"; c++;
                ws.Cells[r1, c].Value = "Year"; c++;

                // Months merged cells in row 1
                foreach (var m in months)
                {
                    ws.Cells[r1, c].Value = m;
                    ws.Cells[r1, c, r1, c + 1].Merge = true; // Unit+Value
                    c += 2;
                }

                // ✅ TOTAL group header (merge 2 cols)
                ws.Cells[r1, c].Value = "TOTAL";
                ws.Cells[r1, c, r1, c + 1].Merge = true;
                int totalUnitCol = c;       // first of TOTAL (Unit)
                int totalValueCol = c + 1;  // second of TOTAL (Value)
                c += 2;

                // Row 2 sub headers
                int c2 = 1;
                ws.Cells[r2, c2++].Value = ""; // ProjectID
                ws.Cells[r2, c2++].Value = ""; // Project Name
                ws.Cells[r2, c2++].Value = ""; // Project Plan Type
                ws.Cells[r2, c2++].Value = ""; // Year
                foreach (var _ in months)
                {
                    ws.Cells[r2, c2++].Value = "Unit";
                    ws.Cells[r2, c2++].Value = "Value";
                }
                ws.Cells[r2, c2++].Value = "Unit";   // TOTAL Unit
                ws.Cells[r2, c2++].Value = "Value";  // TOTAL Value

                // Styling headers
                using (var rngTop = ws.Cells[r1, 1, r1, c - 1])
                {
                    rngTop.Style.Font.Bold = true;
                    rngTop.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    rngTop.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    rngTop.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rngTop.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#bfbfbf"));
                    rngTop.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Gray);
                }
                using (var rngSub = ws.Cells[r2, 1, r2, c - 1])
                {
                    rngSub.Style.Font.Bold = true;
                    rngSub.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    rngSub.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    rngSub.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rngSub.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#e6e6e6"));
                    rngSub.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Gray);
                }

                ws.Row(r1).Height = 22;
                ws.Row(r2).Height = 20;
                ws.View.FreezePanes(3, 5); // Freeze below header & after Year

                // helpers
                string CellAddr(int row, int col) => ws.Cells[row, col].Address;

                // ===== Data rows =====
                int dataStartRow = 3;
                int totalCols = c - 1; // last used column index
                int firstUnitCol = 5;  // after 4 fixed cols, Unit starts at col 5
                int lastValueCol = 4 + months.Length * 2;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    int excelRow = dataStartRow + i;
                    int dc = 1;

                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("ProjectID") ? dr["ProjectID"] : null;
                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("ProjectName") ? dr["ProjectName"] : null;
                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("ProjectPlanType") ? dr["ProjectPlanType"] : null;
                    ws.Cells[excelRow, dc++].Value = dr.Table.Columns.Contains("Year") ? dr["Year"] : null;

                    // Write month cells; แสดง "-" เมื่อค่าว่าง
                    foreach (var m in months)
                    {
                        var unitColName = m + "_Unit";
                        var valueColName = m + "_Value";

                        // Unit
                        if (dr.Table.Columns.Contains(unitColName) && dr[unitColName] != DBNull.Value)
                            ws.Cells[excelRow, dc].Value = dr[unitColName];
                        else
                            ws.Cells[excelRow, dc].Value = "-";
                        dc++;

                        // Value
                        if (dr.Table.Columns.Contains(valueColName) && dr[valueColName] != DBNull.Value)
                            ws.Cells[excelRow, dc].Value = dr[valueColName];
                        else
                            ws.Cells[excelRow, dc].Value = "-";
                        dc++;
                    }

                    // ===== TOTAL formulas (SUM เฉพาะช่องที่เป็นตัวเลข; ช่อง "-" จะไม่ถูกรวมเพราะเป็นข้อความ)
                    // Sum Unit columns: 5,7,9,...,(4 + 2*months)
                    var unitCells = new List<string>();
                    for (int col = firstUnitCol; col <= lastValueCol; col += 2) unitCells.Add(CellAddr(excelRow, col));
                    ws.Cells[excelRow, totalUnitCol].Formula = $"=SUM({string.Join(",", unitCells)})";

                    // Sum Value columns: 6,8,10,...,(4 + 2*months)
                    var valueCells = new List<string>();
                    for (int col = firstUnitCol + 1; col <= lastValueCol; col += 2) valueCells.Add(CellAddr(excelRow, col));
                    ws.Cells[excelRow, totalValueCol].Formula = $"=SUM({string.Join(",", valueCells)})";
                }

                // ===== Fixed widths (no AutoFit) =====
                // Fixed columns
                ws.Column(1).Width = 15;  // ProjectID
                ws.Column(2).Width = 28;  // Project Name
                ws.Column(3).Width = 20;  // Project Plan Type
                ws.Column(4).Width = 10;  // Year

                // Unit columns → 15
                for (int col = 5; col <= lastValueCol; col += 2)
                    ws.Column(col).Width = 7;
                ws.Column(totalUnitCol).Width = 7;

                // Value columns → ~250px ≈ width 36
                for (int col = 6; col <= lastValueCol; col += 2)
                    ws.Column(col).Width = 30;
                ws.Column(totalValueCol).Width = 30;

                // ===== Number formats =====
                for (int col = 6; col <= lastValueCol; col += 2)
                    ws.Column(col).Style.Numberformat.Format = "#,##0.00";   // Value
                ws.Column(totalValueCol).Style.Numberformat.Format = "#,##0.00";

                for (int col = 5; col <= lastValueCol; col += 2)
                    ws.Column(col).Style.Numberformat.Format = "#,##0";      // Unit (integer)
                ws.Column(totalUnitCol).Style.Numberformat.Format = "#,##0";


                // Center headers & month subheaders
                ws.Cells[r1, 1, r2, totalCols].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells[r1, 1, r2, totalCols].Style.WrapText = true;

                return package.GetAsByteArray();
            }
        }


        //private DataTable ConvertToDataTable(List<RollingPlanSummaryModel> dataList)
        //{
        //    var dt = new DataTable();

        //    // Fixed columns
        //    dt.Columns.Add("ProjectID", typeof(string));
        //    dt.Columns.Add("ProjectName", typeof(string));
        //    dt.Columns.Add("ProjectPlanType", typeof(string));
        //    dt.Columns.Add("Year", typeof(int));

        //    // Month columns
        //    string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    foreach (var m in months)
        //    {
        //        dt.Columns.Add($"{m}_Unit", typeof(decimal));
        //        dt.Columns.Add($"{m}_Value", typeof(decimal));
        //    }

        //    // Rows
        //    foreach (var item in dataList)
        //    {
        //        var row = dt.NewRow();

        //        row["ProjectID"] = item.ProjectID ?? (object)DBNull.Value;
        //        row["ProjectName"] = item.ProjectName ?? (object)DBNull.Value;
        //        row["ProjectPlanType"] = item.PlanTypeName ?? (object)DBNull.Value;
        //        row["Year"] = int.TryParse(item.PlanYear, out var y) ? y : (object)DBNull.Value;

        //        // map month values
        //        row["Jan_Unit"] = item.Jan_Unit ?? (object)DBNull.Value;
        //        row["Jan_Value"] = item.Jan_Value ?? (object)DBNull.Value;

        //        row["Feb_Unit"] = item.Feb_Unit ?? (object)DBNull.Value;
        //        row["Feb_Value"] = item.Feb_Value ?? (object)DBNull.Value;

        //        row["Mar_Unit"] = item.Mar_Unit ?? (object)DBNull.Value;
        //        row["Mar_Value"] = item.Mar_Value ?? (object)DBNull.Value;

        //        row["Apr_Unit"] = item.Apr_Unit ?? (object)DBNull.Value;
        //        row["Apr_Value"] = item.Apr_Value ?? (object)DBNull.Value;

        //        row["May_Unit"] = item.May_Unit ?? (object)DBNull.Value;
        //        row["May_Value"] = item.May_Value ?? (object)DBNull.Value;

        //        row["Jun_Unit"] = item.Jun_Unit ?? (object)DBNull.Value;
        //        row["Jun_Value"] = item.Jun_Value ?? (object)DBNull.Value;

        //        row["Jul_Unit"] = item.Jul_Unit ?? (object)DBNull.Value;
        //        row["Jul_Value"] = item.Jul_Value ?? (object)DBNull.Value;

        //        row["Aug_Unit"] = item.Aug_Unit ?? (object)DBNull.Value;
        //        row["Aug_Value"] = item.Aug_Value ?? (object)DBNull.Value;

        //        row["Sep_Unit"] = item.Sep_Unit ?? (object)DBNull.Value;
        //        row["Sep_Value"] = item.Sep_Value ?? (object)DBNull.Value;

        //        row["Oct_Unit"] = item.Oct_Unit ?? (object)DBNull.Value;
        //        row["Oct_Value"] = item.Oct_Value ?? (object)DBNull.Value;

        //        row["Nov_Unit"] = item.Nov_Unit ?? (object)DBNull.Value;
        //        row["Nov_Value"] = item.Nov_Value ?? (object)DBNull.Value;

        //        row["Dec_Unit"] = item.Dec_Unit ?? (object)DBNull.Value;
        //        row["Dec_Value"] = item.Dec_Value ?? (object)DBNull.Value;

        //        dt.Rows.Add(row);
        //    }

        //    return dt;
        //}

        //private byte[] WriteExcelProjectAndTargetRolling(DataTable dt)
        //{
        //    using (var package = new ExcelPackage())
        //    {
        //        var ws = package.Workbook.Worksheets.Add("Sheet 1");

        //        if (dt == null || dt.Columns.Count == 0)
        //            throw new Exception("No columns found in DataTable.");

        //        string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        //        int r1 = 1; // first header row
        //        int r2 = 2; // second header row
        //        int c = 1;

        //        // Fixed headers row 1
        //        ws.Cells[r1, c].Value = "ProjectID"; c++;
        //        ws.Cells[r1, c].Value = "Project Name"; c++;
        //        ws.Cells[r1, c].Value = "Project Plan Type"; c++;
        //        ws.Cells[r1, c].Value = "Year"; c++;

        //        // Months merged cells in row 1
        //        foreach (var m in months)
        //        {
        //            ws.Cells[r1, c].Value = m;
        //            ws.Cells[r1, c, r1, c + 1].Merge = true; // merge month over Unit + Value
        //            c += 2;
        //        }

        //        // Row 2 headers (sub columns)
        //        int c2 = 1;
        //        ws.Cells[r2, c2++].Value = ""; // ProjectID
        //        ws.Cells[r2, c2++].Value = ""; // Project Name
        //        ws.Cells[r2, c2++].Value = ""; // Project Plan Type
        //        ws.Cells[r2, c2++].Value = ""; // Year
        //        foreach (var _ in months)
        //        {
        //            ws.Cells[r2, c2++].Value = "Unit";
        //            ws.Cells[r2, c2++].Value = "Value";
        //        }

        //        // Styling top headers
        //        using (var rngTop = ws.Cells[r1, 1, r1, c - 1])
        //        {
        //            rngTop.Style.Font.Bold = true;
        //            rngTop.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            rngTop.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //            rngTop.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            rngTop.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#bfbfbf"));
        //            rngTop.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
        //        }
        //        using (var rngSub = ws.Cells[r2, 1, r2, c - 1])
        //        {
        //            rngSub.Style.Font.Bold = true;
        //            rngSub.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //            rngSub.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //            rngSub.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            rngSub.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#e6e6e6"));
        //            rngSub.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
        //        }

        //        ws.Row(r1).Height = 22;
        //        ws.Row(r2).Height = 20;
        //        ws.View.FreezePanes(3, 5); // Freeze below header & after Year

        //        // ===== Data rows =====
        //        int dataStartRow = 3;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            var dr = dt.Rows[i];
        //            int dc = 1;

        //            ws.Cells[dataStartRow + i, dc++].Value = dr.Table.Columns.Contains("ProjectID") ? dr["ProjectID"] : null;
        //            ws.Cells[dataStartRow + i, dc++].Value = dr.Table.Columns.Contains("ProjectName") ? dr["ProjectName"] : null;
        //            ws.Cells[dataStartRow + i, dc++].Value = dr.Table.Columns.Contains("ProjectPlanType") ? dr["ProjectPlanType"] : null;
        //            ws.Cells[dataStartRow + i, dc++].Value = dr.Table.Columns.Contains("Year") ? dr["Year"] : null;

        //            foreach (var m in months)
        //            {
        //                var unitCol = m + "_Unit";
        //                var valueCol = m + "_Value";

        //                ws.Cells[dataStartRow + i, dc++].Value = dr.Table.Columns.Contains(unitCol) ? dr[unitCol] : null;
        //                ws.Cells[dataStartRow + i, dc++].Value = dr.Table.Columns.Contains(valueCol) ? dr[valueCol] : null;
        //            }
        //        }

        //        // ===== Column widths =====
        //        ws.Column(1).Width = 15; // ProjectID
        //        ws.Column(2).Width = 28; // Project Name
        //        ws.Column(3).Width = 20; // Project Plan Type
        //        ws.Column(4).Width = 10; // Year
        //        int totalCols = 4 + months.Length * 2;
        //        for (int col = 5; col <= totalCols; col++)
        //        {
        //            ws.Column(col).Width = 12;
        //        }

        //        // ===== Number formats =====
        //        for (int col = 6; col <= totalCols; col += 2) // Value columns
        //            ws.Column(col).Style.Numberformat.Format = "#,##0.00";

        //        for (int col = 5; col <= totalCols; col += 2) // Unit columns
        //            ws.Column(col).Style.Numberformat.Format = "#,##0.##";

        //        // Borders for all table cells
        //        using (var rngAll = ws.Cells[1, 1, dataStartRow + dt.Rows.Count - 1, totalCols])
        //        {
        //            rngAll.Style.Border.Top.Style = ExcelBorderStyle.Hair;
        //            rngAll.Style.Border.Left.Style = ExcelBorderStyle.Hair;
        //            rngAll.Style.Border.Right.Style = ExcelBorderStyle.Hair;
        //            rngAll.Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
        //        }

        //        return package.GetAsByteArray();
        //    }
        //}

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
