using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.AppointmentLimit;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IAppointmentLimitRepo
    {
        public List<AppointmentLimitModel.ProjectAppointLimitPivotRow> GetlistAppointmentLimit(AppointmentLimitModel.ProjectAppointLimitPivotRow filter);
        public AppointmentLimitModel.ProjectAppointLimitIUD InsertOrUpdateProjectAppointLimit(IEnumerable<AppointmentLimitModel.ProjectAppointLimitIUD> items, int UserID);
    }
    public class AppointmentLimitRepo : IAppointmentLimitRepo
    {
        private readonly CSSContext _context;

        public AppointmentLimitRepo(CSSContext context)
        {
            _context = context;
        }

        public List<AppointmentLimitModel.ProjectAppointLimitPivotRow> GetlistAppointmentLimit(AppointmentLimitModel.ProjectAppointLimitPivotRow filter)
        {
            List<AppointmentLimitModel.ProjectAppointLimitPivotRow> result = new List<AppointmentLimitModel.ProjectAppointLimitPivotRow>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                                -- ===== TEST CASE =====
                                --DECLARE @ProjectID NVARCHAR(50) = 'CAM002';
                                -- ===== TEST CASE =====

                                  SELECT
                                      T1.ProjectID
                                    , T1.DayID
                                    , T2.[Name] AS DaysName
                                    , SUM(CASE WHEN T1.TimeID = 222 THEN T1.UnitLimitValue ELSE 0 END) AS T222_0900
                                    , SUM(CASE WHEN T1.TimeID = 223 THEN T1.UnitLimitValue ELSE 0 END) AS T223_1000
                                    , SUM(CASE WHEN T1.TimeID = 224 THEN T1.UnitLimitValue ELSE 0 END) AS T224_1100
                                    , SUM(CASE WHEN T1.TimeID = 225 THEN T1.UnitLimitValue ELSE 0 END) AS T225_1200
                                    , SUM(CASE WHEN T1.TimeID = 226 THEN T1.UnitLimitValue ELSE 0 END) AS T226_1300
                                    , SUM(CASE WHEN T1.TimeID = 227 THEN T1.UnitLimitValue ELSE 0 END) AS T227_1400
                                    , SUM(CASE WHEN T1.TimeID = 228 THEN T1.UnitLimitValue ELSE 0 END) AS T228_1500
	                                , SUM(CASE WHEN T1.TimeID = 324 THEN T1.UnitLimitValue ELSE 0 END) AS T324_1600
                                    , SUM(CASE WHEN T1.TimeID = 325 THEN T1.UnitLimitValue ELSE 0 END) AS T325_1700
                                FROM TR_ProjectAppointLimit_Mapping T1
                                LEFT JOIN tm_Ext T2 ON T1.DayID  = T2.ID
                                LEFT JOIN tm_Ext T3 ON T1.TimeID = T3.ID
                                WHERE T1.ProjectID = @ProjectID
                                  AND T1.FlagActive = 1
                                GROUP BY
                                      T1.ProjectID
                                    , T1.DayID
                                    , T2.[Name]
                                ORDER BY
                                      T1.ProjectID
                                    , T1.DayID

                           "
                                    ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", filter.ProjectID ?? ""));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new AppointmentLimitModel.ProjectAppointLimitPivotRow
                            {
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                DayID = Commond.FormatExtension.Nulltoint(reader["DayID"]),
                                DaysName = Commond.FormatExtension.NullToString(reader["DaysName"]),

                                T222_0900 = Commond.FormatExtension.Nulltoint(reader["T222_0900"]),
                                T223_1000 = Commond.FormatExtension.Nulltoint(reader["T223_1000"]),
                                T224_1100 = Commond.FormatExtension.Nulltoint(reader["T224_1100"]),
                                T225_1200 = Commond.FormatExtension.Nulltoint(reader["T225_1200"]),
                                T226_1300 = Commond.FormatExtension.Nulltoint(reader["T226_1300"]),
                                T227_1400 = Commond.FormatExtension.Nulltoint(reader["T227_1400"]),
                                T228_1500 = Commond.FormatExtension.Nulltoint(reader["T228_1500"]),
                                T324_1600 = Commond.FormatExtension.Nulltoint(reader["T324_1600"]),
                                T325_1700 = Commond.FormatExtension.Nulltoint(reader["T325_1700"]),

                            });
                        }
                    }
                }
            }


            return result;
        }

        public AppointmentLimitModel.ProjectAppointLimitIUD InsertOrUpdateProjectAppointLimit(IEnumerable<AppointmentLimitModel.ProjectAppointLimitIUD> items , int UserID)
        {
            var result = new AppointmentLimitModel.ProjectAppointLimitIUD
            {
                Issuccess = false,
                Message = "No items."
            };

            if (items == null) return result;

            var list = items.Where(x => x != null).ToList();
            if (list.Count == 0) return result;

            var now = DateTime.Now;

            var projectId = list.First().ProjectID ?? "";

            using var tx = _context.Database.BeginTransaction();
            try
            {
                // Prefetch existing rows for this project once (tracked)
                var existing = _context.TR_ProjectAppointLimit_Mappings.Where(x => x.ProjectID == projectId).ToList();

                int ins = 0, upd = 0;

                foreach (var item in list)
                {
                    var pid = item.ProjectID ?? "";
                    var dayId = item.DayID;
                    var timeId = item.TimeID;
                    var value = Math.Max(0, item.UnitLimitValue); // keep non-negative

                    // find existing by composite key
                    var row = existing.FirstOrDefault(x =>x.ProjectID == pid &&x.DayID == dayId && x.TimeID == timeId);

                    if (row != null)
                    {
                        // UPDATE
                        row.UnitLimitValue = value;
                        row.FlagActive = true;
                        row.UpdateDate = now;
                        row.UpdateBy = UserID;
                        upd++;
                    }
                    else
                    {
                        // INSERT
                        var newRow = new TR_ProjectAppointLimit_Mapping
                        {
                            ProjectID = pid,
                            DayID = dayId,
                            TimeID = timeId,
                            UnitLimitValue = value,
                            FlagActive = true,
                            CreateDate = now,
                            CreateBy = UserID,
                            UpdateDate = now,
                            UpdateBy = UserID
                        };
                        _context.TR_ProjectAppointLimit_Mappings.Add(newRow);
                        ins++;
                    }
                }

                _context.SaveChanges();
                tx.Commit();

                result.Issuccess = true;
                //result.Message = $"Upsert complete. Inserted {ins}, Updated {upd}.";
                result.Message = "Appointment limits saved successfully.";
                return result;
            }
            catch (Exception ex)
            {
                tx.Rollback();
                result.Issuccess = false;
                result.Message = ex.InnerException?.Message ?? ex.Message;
                return result;
            }
        }

    }
}
