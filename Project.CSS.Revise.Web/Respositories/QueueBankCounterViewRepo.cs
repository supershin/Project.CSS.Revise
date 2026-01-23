using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IQueueBankCounterViewRepo
    {
        public List<ListCounterModel.ListCounterItem> GetListsCounterQueueBank(ListCounterModel.Filter filter);
        public List<ListCounterDetailsModel.ListCounterItem> GetListsCounterDetailsQueueBank(ListCounterDetailsModel.Filter filter);
        public List<dynamic> GetUnitDropdown(string projectId);
        public UpdateUnitRegisterModel.Message UpdateUnitRegister(UpdateUnitRegisterModel.Entity input);
        public UpdateUnitRegisterModel.Message RemoveUnitFromCounter(UpdateUnitRegisterModel.Entity input);
        public UpdateUnitRegisterModel.Message CheckoutBankCounter(BankCheckoutRequest input);
        public bool SaveRegisterCallStaffCounter(RegisterCallStaffCounter model, int userId);
        public DingDongModel.Result CheckCanDingDong(DingDongModel.Filter filter);
    }
    public class QueueBankCounterViewRepo : IQueueBankCounterViewRepo
    {
        private readonly CSSContext _context;

        public QueueBankCounterViewRepo(CSSContext context)
        {
            _context = context;
        }

        public List<ListCounterModel.ListCounterItem> GetListsCounterQueueBank(ListCounterModel.Filter filter)
        {
            List<ListCounterModel.ListCounterItem> result = new List<ListCounterModel.ListCounterItem>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                        -- DECLARE @ProjectID NVARCHAR(50) = 'HGM001';

                        -- Step 1: Generate counter list from mapping
                        WITH CounterList AS (
                            SELECT 
                                T1.ID,
                                T1.ProjectID,
                                T1.QueueTypeID,
                                Counter = T1.StartCounter + V.number,
                                T1.FlagActive
                            FROM TR_ProjectCounter_Mapping T1 WITH (NOLOCK)
                            JOIN master..spt_values V 
                                ON V.type = 'P'
                                AND V.number BETWEEN 0 AND (T1.EndCounter - T1.StartCounter)
                            WHERE 1 = 1
                                AND T1.ProjectID = @ProjectID
                                AND T1.FlagActive = 1
                                AND T1.QueueTypeID = 48
                        ),

                        -- Step 2: group RegisterLog + BankCounter by (Project, QueueType, Counter)
                        ActiveRegister AS (
                            SELECT
                                 RL.ProjectID
                                ,RL.QueueTypeID
                                ,RL.[Counter]                          AS RL_Counter

                                -- ถ้าจะอ้าง RegisterLogID/UnitID ให้ใช้ตัวล่าสุด (ID สูงสุด)
                                ,MAX(RL.ID)                            AS RegisterLogID
                                ,MAX(RL.UnitID)                        AS UnitID

                                 ,CASE
								    WHEN COUNT(DISTINCT TU.UnitCode) > 1
									    THEN CONCAT(MIN(TU.UnitCode), '+')
								    ELSE MIN(TU.UnitCode)
							      END AS UnitCode

                                ,MAX(RL.ResponsibleID)                 AS ResponsibleID
                                ,MAX(RL.FastFixDate)                   AS FastFixDate
                                ,MAX(RL.FinishDate)                    AS FinishDate
                                ,MAX(RL.CareerTypeID)                  AS CareerTypeID
                                ,MAX(RL.TransferTypeID)                AS TransferTypeID
                                ,MAX(RL.ReasonID)                      AS ReasonID
                                ,MAX(RL.FixedDuration)                 AS FixedDuration

                                -- Bank info (ยังคงดึงจาก record ที่ไม่ Check Out เท่านั้น)
                                ,MAX(RBC.BankID)                       AS BankID
                                ,MAX(RBC.BankCounterStatus)            AS BankCounterStatus
                                ,MAX(RBC.CheckInDate)                  AS CheckInDate

                                -- ✅ ถ้ามี InProcessDate ซักตัว → MAX() จะไม่เป็น NULL
                                ,MAX(RBC.InProcessDate)                AS InProcessDate

                                -- ตาม join เงื่อนไขเดิม CheckOutDate จะเป็น NULL เสมอในชุดนี้
                                ,MAX(RBC.CheckOutDate)                 AS CheckOutDate

                                ,MAX(TB.BankCode)                      AS BankCode
                                ,MAX(TB.BankName)                      AS BankName
                            FROM TR_RegisterLog RL WITH (NOLOCK)
                            LEFT JOIN tm_Unit TU WITH (NOLOCK)
                                ON RL.UnitID = TU.ID
                            LEFT JOIN TR_Register_BankCounter RBC WITH (NOLOCK)
                                ON RL.ID = RBC.RegisterLogID
                                AND RBC.BankCounterStatus <> 'Check Out'
                                AND RBC.CheckOutDate IS NULL   -- ⬅️ keep your original condition
                            LEFT JOIN tm_Bank TB WITH (NOLOCK)
                                ON RBC.BankID = TB.ID
                            WHERE 1 = 1
                                AND RL.ProjectID   = @ProjectID
                                AND RL.FlagActive  = 1
                                AND RL.QueueTypeID = 48
                                AND RL.FinishDate IS NULL
                            GROUP BY
                                RL.ProjectID,
                                RL.QueueTypeID,
                                RL.[Counter]
                        )

                        -- Step 3: join back to counter list (show all counters, even if no RL)
                        SELECT
                             C.ID                 AS ProjectCounterMappingID
                            ,C.ProjectID
                            ,C.QueueTypeID
                            ,C.[Counter]

                            ,AR.RegisterLogID
                            ,AR.UnitID
                            ,AR.UnitCode           -- 👈 now ""26/11,26/10"" when multiple units on same counter
                            ,AR.ResponsibleID
                            ,AR.FastFixDate
                            ,AR.FinishDate
                            ,AR.CareerTypeID
                            ,AR.TransferTypeID
                            ,AR.ReasonID
                            ,AR.FixedDuration

                            ,AR.RL_Counter         AS RegisterCounter

                            ,AR.BankID
                            ,AR.BankCounterStatus
                            ,AR.CheckInDate
                            ,AR.InProcessDate      -- 👈 will show if ANY active RBC row has value
                            ,AR.CheckOutDate       -- 👈 from active rows; with your filter this will be NULL

                            ,AR.BankCode
                            ,AR.BankName
                        FROM CounterList C
                        LEFT JOIN ActiveRegister AR
                            ON  C.ProjectID   = AR.ProjectID
                            AND C.QueueTypeID = AR.QueueTypeID
                            AND C.[Counter]   = AR.RL_Counter
                        ORDER BY C.[Counter];

                    ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", filter.ProjectID ?? string.Empty));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ListCounterModel.ListCounterItem
                            {
                                ProjectCounterMappingID = Commond.FormatExtension.NullToString(reader["ProjectCounterMappingID"]),
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                QueueTypeID = Commond.FormatExtension.NullToString(reader["QueueTypeID"]),
                                Counter = Commond.FormatExtension.NullToString(reader["Counter"]),

                                // RegisterLog fields
                                RegisterLogID = Commond.FormatExtension.NullToString(reader["RegisterLogID"]),
                                UnitID = Commond.FormatExtension.NullToString(reader["UnitID"]),
                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                ResponsibleID = Commond.FormatExtension.NullToString(reader["ResponsibleID"]),
                                FastFixDate = Commond.FormatExtension.NullToString(reader["FastFixDate"]),
                                FinishDate = Commond.FormatExtension.NullToString(reader["FinishDate"]),
                                CareerTypeID = Commond.FormatExtension.NullToString(reader["CareerTypeID"]),
                                TransferTypeID = Commond.FormatExtension.NullToString(reader["TransferTypeID"]),
                                ReasonID = Commond.FormatExtension.NullToString(reader["ReasonID"]),
                                FixedDuration = Commond.FormatExtension.NullToString(reader["FixedDuration"]),

                                // Bank Counter fields
                                BankID = Commond.FormatExtension.NullToString(reader["BankID"]),
                                BankCounterStatus = Commond.FormatExtension.NullToString(reader["BankCounterStatus"]),
                                CheckInDate = Commond.FormatExtension.NullToString(reader["CheckInDate"]),
                                InProcessDate = Commond.FormatExtension.NullToString(reader["InProcessDate"]),
                                CheckOutDate = Commond.FormatExtension.NullToString(reader["CheckOutDate"]),

                                // Bank master data
                                BankCode = Commond.FormatExtension.NullToString(reader["BankCode"]),
                                BankName = Commond.FormatExtension.NullToString(reader["BankName"])
                            };

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        public List<ListCounterDetailsModel.ListCounterItem> GetListsCounterDetailsQueueBank(ListCounterDetailsModel.Filter filter)
        {
            List<ListCounterDetailsModel.ListCounterItem> result = new List<ListCounterDetailsModel.ListCounterItem>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                --DECLARE @ProjectID NVARCHAR(50) = 'HGM001';
                                --DECLARE @Counter INT = 1;

                                SELECT RL.[ID]
                                      ,RL.[ProjectID]
                                      ,RL.[UnitID]
	                                  ,TU.UnitCode
                                      ,RBC.BankID 
	                                  ,TB.[BankCode]
                                      ,TB.[BankName]
                                  FROM [TR_RegisterLog] RL WITH(NOLOCK)
                                  LEFT JOIN tm_Unit TU WITH (NOLOCK)
                                    ON RL.UnitID = TU.ID
                                  LEFT JOIN TR_Register_BankCounter RBC WITH (NOLOCK)
                                    ON RL.ID = RBC.RegisterLogID
                                    AND RBC.BankCounterStatus <> 'Check Out'
                                    AND RBC.CheckOutDate IS NULL
                                  LEFT JOIN tm_Bank TB WITH (NOLOCK)
                                    ON RBC.BankID = TB.ID
                                  WHERE RL.ProjectID = @ProjectID
                                  AND RL.[Counter] = @Counter
                                  AND RL.FlagActive = 1
                                  AND RL.[FinishDate] IS NULL
                                  AND RL.[QCTypeID] = 10
                                  AND RL.[QueueTypeID]  = 48

                                  ORDER BY RL.[CreateDate] DESC
                    ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", filter.ProjectID ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@Counter", filter.Counter ?? 0));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ListCounterDetailsModel.ListCounterItem
                            {
                                ID = Commond.FormatExtension.NullToString(reader["ID"]),
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                UnitID = Commond.FormatExtension.NullToString(reader["UnitID"]),
                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                BankID = Commond.FormatExtension.NullToString(reader["BankID"]),
                                BankCode = Commond.FormatExtension.NullToString(reader["BankCode"]),
                                BankName = Commond.FormatExtension.NullToString(reader["BankName"])
                            };

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }

        public List<dynamic> GetUnitDropdown(string projectId)
        {
            var list = _context.tm_Units
                .Where(u => u.ProjectID == projectId && u.FlagActive == true)
                .OrderBy(u => u.UnitCode)
                .Select(u => new
                {
                    Value = u.ID,
                    Text = u.UnitCode
                })
                .ToList<dynamic>();

            return list;
        }

        public UpdateUnitRegisterModel.Message UpdateUnitRegister(UpdateUnitRegisterModel.Entity input)
        {
            var result = new UpdateUnitRegisterModel.Message
            {
                Issucces = false,
                TextResult = string.Empty
            };

            if (input == null ||
                string.IsNullOrWhiteSpace(input.ProjectID) ||
                string.IsNullOrWhiteSpace(input.UnitID))
            {
                result.TextResult = "Project or unit is invalid.";
                return result;
            }

            // ⭐ แปลง UnitID (string) -> Guid โดยใช้ NulltoGuid
            Guid unitGuid = Commond.FormatExtension.NulltoGuid(input.UnitID);

            if (unitGuid == Guid.Empty)
            {
                result.TextResult = "Unit ID is invalid.";
                return result;
            }

            // หา RegisterLog ตาม Project + Unit (เฉพาะที่ยัง Active)
            var registerLog = _context.TR_RegisterLogs
                .Where(r => r.ProjectID == input.ProjectID
                         && r.UnitID == unitGuid          // ✅ เทียบ Guid กับ Guid
                         && r.FlagActive == true)
                .OrderByDescending(r => r.CreateDate)
                .FirstOrDefault();

            if (registerLog == null)
            {
                result.Issucces = false;
                result.TextResult = "This unit is not registered.";
                return result;
            }

            // ถ้า Counter เป็น int? ให้ใช้ HasValue
            bool hadOldCounter = registerLog.Counter.HasValue;

            registerLog.Counter = input.Counter;
            registerLog.UpdateDate = DateTime.Now;
            // registerLog.UpdateBy = currentUserId; // แล้วแต่ระบบพ่อใหญ่

            _context.SaveChanges();

            result.Issucces = true;
            result.TextResult = hadOldCounter
                ? "Moved this unit to the new counter."
                : "Registered this unit to the counter.";

            return result;
        }

        public UpdateUnitRegisterModel.Message RemoveUnitFromCounter(UpdateUnitRegisterModel.Entity input)
        {
            var result = new UpdateUnitRegisterModel.Message
            {
                Issucces = false,
                TextResult = string.Empty
            };

            // 1) validate basic
            if (string.IsNullOrWhiteSpace(input.ProjectID) ||
                string.IsNullOrWhiteSpace(input.UnitID))
            {
                result.TextResult = "Project or Unit is invalid.";
                return result;
            }

            // 2) convert UnitID string → Guid (ใช้ helper ของพ่อใหญ่)
            var unitGuid = Commond.FormatExtension.NulltoGuid(input.UnitID);

            if (unitGuid == Guid.Empty)
            {
                result.TextResult = "UnitID is invalid.";
                return result;
            }

            // 3) หา RegisterLog ตาม Project + Unit (เอาตัวล่าสุดสุด)
            var registerLog = _context.TR_RegisterLogs
                .Where(r => r.ProjectID == input.ProjectID
                         && r.UnitID == unitGuid
                         && r.FlagActive == true)
                .OrderByDescending(r => r.CreateDate)
                .FirstOrDefault();

            if (registerLog == null)
            {
                result.TextResult = "Cannot find register for this unit in this project.";
                return result;
            }

            // ถ้าอยาก lock เฉพาะเคส QueueTypeID/QCTypeID/FinishDate ก็เพิ่มได้แบบนี้:
            // if (registerLog.QueueTypeID != 48 || registerLog.QCTypeID != 10 || registerLog.FinishDate != null)
            // {
            //     result.TextResult = "This register cannot be modified for bank counter.";
            //     return result;
            // }

            // 4) เช็คว่าตอนนี้มันผูก counter อยู่ไหม
            if (!registerLog.Counter.HasValue)
            {
                result.Issucces = false;
                result.TextResult = "This unit is not assigned to any counter.";
                return result;
            }

            // 5) set Counter = null แล้ว save
            registerLog.Counter = null;
            registerLog.UpdateDate = DateTime.Now;
            // registerLog.UpdateBy = currentUserName; // ถ้ามีระบบ user ก็ใส่เพิ่มเองได้เลย

            _context.SaveChanges();

            result.Issucces = true;
            result.TextResult = "Removed this unit from the counter successfully.";

            return result;
        }

        public UpdateUnitRegisterModel.Message CheckoutBankCounter(BankCheckoutRequest input)
        {
            var result = new UpdateUnitRegisterModel.Message
            {
                Issucces = false,
                TextResult = string.Empty
            };

            if (input.RegisterLogID == 0 || input.BankID <= 0)
            {
                result.TextResult = "Bank or register is invalid.";
                return result;
            }

            using var tran = _context.Database.BeginTransaction();
            try
            {
                // 1) หา row TR_Register_BankCounter ที่ยังไม่ Check Out
                var rows = _context.TR_Register_BankCounters
                    .Where(x => x.RegisterLogID == input.RegisterLogID
                             && x.BankID == input.BankID
                             && x.CheckOutDate == null)
                    .ToList();

                if (!rows.Any())
                {
                    result.TextResult = "This bank counter has already been checked out or not found.";
                    return result;
                }

                var now = DateTime.Now;

                foreach (var r in rows)
                {
                    r.BankCounterStatus = Constants.Register.BankCounterStatus.CHECK_OUT; // "Check Out"
                    r.CheckOutDate = now;
                    r.UpdateDate = now;
                }

                // 2) (option) เคลียร์ Counter ใน TR_RegisterLog
                var reg = _context.TR_RegisterLogs.FirstOrDefault(x => x.ID == input.RegisterLogID && x.FlagActive == true);

                // 3) สร้าง ContactLog แบบของเก่า
                if (reg != null)
                {
                    var unit = _context.tm_Units.FirstOrDefault(u => u.ID == reg.UnitID);
                    if (unit != null)
                    {
                        var contact = new TR_ContactLog
                        {
                            BankID = input.BankID,
                            Detail = input.ContactDetail.ToStringNullable()  // ถ้ามี extension นี้
                        };

                        contact = SetContactLogBankCheckout(contact, unit);
                        _context.TR_ContactLogs.Add(contact);
                    }
                }

                _context.SaveChanges();
                tran.Commit();

                result.Issucces = true;
                result.TextResult = "Bank has been checked out and contact log has been recorded.";
                return result;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                // log ex ตาม style โปรเจกต์
                result.Issucces = false;
                result.TextResult = "Error while checking out bank counter.";
                return result;
            }
        }

        private TR_ContactLog SetContactLogBankCheckout(TR_ContactLog item, tm_Unit unit)
        {
            item.ID = Guid.NewGuid();
            item.ProjectID = unit.ProjectID;
            item.UnitID = unit.ID;
            item.TeamID = Constants.Ext.CONTACT_TEAM_CUSTOMER_SERVICE;
            item.ContactDate = DateTime.Now;
            item.ContactTime = item.ContactDate.AsDate().ToString("HH:mm");
            item.ContactName = unit.CustomerName;
            item.FlagActive = true;
            item.CreateDate = DateTime.Now;
            item.UpdateDate = DateTime.Now;
            return item;
        }

        public bool SaveRegisterCallStaffCounter(RegisterCallStaffCounter model, int userId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.RegisterLogID <= 0)
            {
                throw new ArgumentException("RegisterLogID is required.");
            }

            var status = (model.CallStaffStatus ?? "").Trim().ToLower();

            if (status != "start" && status != "stop")
            {
                throw new ArgumentException("CallStaffStatus must be 'start' or 'stop'.");
            }

            var now = DateTime.Now;

            var entity = new TR_Register_CallStaffCounter
            {
                RegisterLogID = model.RegisterLogID,
                CallStaffStatus = status,
                ActionDate = now,
                CreateDate = now,
                CreateBy = userId
            };

            _context.TR_Register_CallStaffCounters.Add(entity);
            var rows = _context.SaveChanges();   // จำนวน row ที่ insert

            return rows > 0;   // ✅ true ถ้า insert สำเร็จ
        }

        public DingDongModel.Result CheckCanDingDong(DingDongModel.Filter filter)
        {
            var result = new DingDongModel.Result();

            string projectId = (filter.ProjectID ?? "").Trim();
            result.ProjectID = projectId;

            if (string.IsNullOrEmpty(projectId))
            {
                result.CanDingDong = false;
                return result;
            }

            DateTime day = (filter.Day ?? DateTime.Now).Date;
            DateTime dayStart = day;
            DateTime dayEnd = day.AddDays(1);

            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // =========================
                // 1) REGISTER SIGNATURE (latest)
                // =========================
                int registerSig = 0;

                string sqlRegisterSig = @"
                                            SELECT
                                                Signature = CHECKSUM_AGG(
                                                    BINARY_CHECKSUM(
                                                        ID,
                                                        UnitID,
                                                        Counter,
                                                        RegisterDate,
                                                        CreateDate
                                                    )
                                                )
                                            FROM [CSS].[dbo].[TR_RegisterLog] WITH (NOLOCK)
                                            WHERE ProjectID = @ProjectID
                                              AND CreateDate >= @DayStart
                                              AND CreateDate <  @DayEnd
                                              AND FlagActive = 1;
                                            ";

                using (SqlCommand cmd = new SqlCommand(sqlRegisterSig, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", projectId));
                    cmd.Parameters.Add(new SqlParameter("@DayStart", dayStart));
                    cmd.Parameters.Add(new SqlParameter("@DayEnd", dayEnd));

                    object? val = cmd.ExecuteScalar();
                    if (val != null && val != DBNull.Value)
                    {
                        registerSig = Convert.ToInt32(val);
                    }
                }

                // ✅ always return latest signature
                result.RegisterSignature = registerSig.ToString();

                // compare with baseline
                string lastSig = (filter.LastRegisterSignature ?? "").Trim();
                if (!string.IsNullOrEmpty(lastSig))
                {
                    result.RegisterChanged = (result.RegisterSignature != lastSig);
                }

                // =========================
                // 2) BANK LATEST UPDATEDATE (latest)
                // =========================
                DateTime? bankLatestUpdate = null;

                string sqlBankLatestUpdate = @"
                                                SELECT MAX(T2.UpdateDate) AS BankLatestUpdateDate
                                                FROM [CSS].[dbo].[TR_RegisterLog] T1 WITH (NOLOCK)
                                                LEFT JOIN [CSS].[dbo].[TR_Register_BankCounter] T2 WITH (NOLOCK)
                                                    ON T1.ID = T2.RegisterLogID
                                                WHERE T1.ProjectID = @ProjectID
                                                  AND T2.BankCounterStatus <> 'Check Out'
                                                  AND T1.CreateDate >= @DayStart
                                                  AND T1.CreateDate <  @DayEnd
                                                  AND T1.FlagActive = 1;
                                                ";

                using (SqlCommand cmd = new SqlCommand(sqlBankLatestUpdate, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", projectId));
                    cmd.Parameters.Add(new SqlParameter("@DayStart", dayStart));
                    cmd.Parameters.Add(new SqlParameter("@DayEnd", dayEnd));

                    object? val = cmd.ExecuteScalar();
                    if (val != null && val != DBNull.Value)
                    {
                        bankLatestUpdate = Convert.ToDateTime(val);
                    }
                }

                // ✅ always return latest bank update
                result.BankLatestUpdateDate = bankLatestUpdate;

                // compare with baseline
                if (filter.LastBankUpdateDate.HasValue && bankLatestUpdate.HasValue)
                {
                    result.BankChanged = bankLatestUpdate.Value > filter.LastBankUpdateDate.Value;
                }

                // =========================
                // 3) FINAL
                // =========================
                result.CanDingDong = (result.RegisterChanged || result.BankChanged);
            }

            return result;
        }
    }
}
