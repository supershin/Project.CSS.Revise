using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IQueueInspectRepo
    {
        public string RemoveRegisterLog(int id);
        public List<SelectListItem> GetListUnitForRegisterInspect(string ProjectID);
    }
    public class QueueInspectRepo : IQueueInspectRepo
    {
        private readonly CSSContext _context;
        private readonly IHttpContextAccessor _http;

        public QueueInspectRepo(CSSContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public string RemoveRegisterLog(int id)
        {
            var user = _http.HttpContext?.User;
            string? LoginID = user?.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);

            var item = _context.TR_RegisterLogs.FirstOrDefault(e => e.ID == id);

            if (item == null) 
            {
                return "NOT_FOUND";
            };            

            var projectId = item.ProjectID ?? "";

            item.FlagActive = false;
            item.UpdateDate = DateTime.Now;
            item.UpdateBy = Commond.FormatExtension.Nulltoint(UserID);
            _context.SaveChanges();

            return projectId;
        }

        public List<SelectListItem> GetListUnitForRegisterInspect(string ProjectID)
        {
            var result = new List<SelectListItem>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT 
                                      u.ID
                                    , u.UnitCode
                                    , u.AddrNo
                                FROM tm_Unit u
                                WHERE u.ID NOT IN (
                                    SELECT UnitID
                                    FROM TR_RegisterLog
                                    WHERE FlagActive = 1  
                                      AND QCTypeID = 10
                                      AND QueueTypeID = 49
                                )
                                AND u.ProjectID = @L_ProjectID
                                AND u.FlagActive = 1
                                ORDER BY u.UnitCode;
                            ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@L_ProjectID", ProjectID ?? string.Empty);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new SelectListItem
                            {
                                Value = Commond.FormatExtension.NullToString(reader["ID"]),
                                Text = Commond.FormatExtension.NullToString(reader["UnitCode"])
                            });
                        }
                    }
                }
            }

            return result;
        }

    }
}
