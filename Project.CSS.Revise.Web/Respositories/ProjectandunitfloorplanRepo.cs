using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using static Project.CSS.Revise.Web.Models.Pages.Projectandunitfloorplan.ProjectandunitfloorplanModel;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IProjectandunitfloorplanRepo
    {
        public List<ListProjectFloorplan> GetlistProjectFloorPlan(ListProjectFloorplan model);
        public List<ListUnit> GetlistUnit(ListUnit model);
    }
    public class ProjectandunitfloorplanRepo : IProjectandunitfloorplanRepo
    {
        private readonly CSSContext _context;

        public ProjectandunitfloorplanRepo(CSSContext context)
        {
            _context = context;
        }

        public List<ListProjectFloorplan> GetlistProjectFloorPlan(ListProjectFloorplan model)
        {
            var query = _context.TR_ProjectFloorPlans
                .Where(g => g.FlagActive == true && g.ProjectID == model.ProjectID)
                .Select(t1 => new ListProjectFloorplan
                {
                    ID = t1.ID,
                    FileName = t1.FileName ?? string.Empty,
                    FilePath = t1.FilePath ?? string.Empty,
                    MimeType = t1.MimeType ?? string.Empty
                }).ToList();
            return query;
        }

        public List<ListUnit> GetlistUnit(ListUnit model)
        {
            List<ListUnit> result = new List<ListUnit>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                                SELECT [ID]
                                      ,[UnitCode]
                                      ,[UnitType]
                                  FROM [tm_Unit]
                                  WHERE ProjectID = @L_ProjectID
                                    AND (
	                                @L_UnitType = N''
	                                OR (N',' + @L_UnitType + N',' LIKE N'%,' + [UnitType] + N',%')
	                                )
                                  AND FlagActive = 1
                                  ORDER BY [UnitCode] ASC

                           "
                                    ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", model.ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_UnitType", model.UnitType ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ListUnit
                            {
                                ID = Commond.FormatExtension.NulltoGuid(reader["ID"]),
                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                UnitType = Commond.FormatExtension.NullToString(reader["UnitType"]),
                            });
                        }
                    }
                }
            }


            return result;
        }



    }
}
