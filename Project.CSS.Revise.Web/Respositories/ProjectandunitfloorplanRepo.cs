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
        Task<bool> SaveMappingAsync(SaveMappingFloorplanModel model, int userId, CancellationToken ct = default);
        public List<ListFloorPlanByUnit> GetFloorPlansByUnit(Guid unitId);
        bool DeactivateUnitFloorPlan(Guid id, int userId);
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
                        --Declare @L_ProjectID Nvarchar(100) = 'EQC018'
                        --Declare @L_UnitType Nvarchar(100) = ''
	
	                      SELECT T1.[ID]
                                ,T1.[UnitCode]
                                ,T1.[UnitType]
			                    ,T2.Cnt_FloorPlan
                            FROM [tm_Unit] T1 WITH (NOLOCK)
		                    LEFT JOIN (
					                    SELECT 
						                    T1.[UnitID]
						                    ,COUNT (T1.[ProjectFloorPlanID]) AS Cnt_FloorPlan
					                    FROM [TR_ProjectUnitFloorPlan] T1 WITH(NOLOCK)
					                    WHERE T1.FlagActive = 1
					                    GROUP BY T1.[UnitID]
		                              ) T2 ON T1.[ID] = T2.UnitID
                            WHERE T1.ProjectID = @L_ProjectID
                            AND (
	                        @L_UnitType = N''
	                        OR (N',' + @L_UnitType + N',' LIKE N'%,' + T1.[UnitType] + N',%')
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
                                Cnt_FloorPlan = Commond.FormatExtension.Nulltoint(reader["Cnt_FloorPlan"])
                            });
                        }
                    }
                }
            }


            return result;
        }

        public async Task<bool> SaveMappingAsync(SaveMappingFloorplanModel model, int userId, CancellationToken ct = default)
        {
            // quick payload checks
            if (model is null) return false;
            if (string.IsNullOrWhiteSpace(model.ProjectID)) return false;
            if (model.FloorPlanIDs is null || model.FloorPlanIDs.Count == 0) return false;
            if (model.UnitIDs is null || model.UnitIDs.Count == 0) return false;

            var projectId = model.ProjectID.Trim();
            var floorplans = model.FloorPlanIDs.Where(g => g != Guid.Empty).Distinct().ToList();
            var units = model.UnitIDs.Where(g => g != Guid.Empty).Distinct().ToList();
            if (floorplans.Count == 0 || units.Count == 0) return false;

            var now = DateTime.Now;

            await using var tx = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                // 1) Deactivate all mappings for this Project + these Units
                foreach (var unitId in units)
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        UPDATE TR_ProjectUnitFloorPlan
                           SET FlagActive = 0,
                               UpdateDate = {now},
                               UpdateBy   = {userId}
                         WHERE ProjectID = {projectId}
                           AND UnitID    = {unitId};", ct);
                }

                // 2) Reactivate existing or insert new for each (UnitID, FloorPlanID)
                foreach (var unitId in units)
                {
                    foreach (var fpId in floorplans)
                    {
                        var updated = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                            UPDATE TR_ProjectUnitFloorPlan
                               SET FlagActive = 1,
                                   UpdateDate = {now},
                                   UpdateBy   = {userId}
                             WHERE ProjectID          = {projectId}
                               AND UnitID             = {unitId}
                               AND ProjectFloorPlanID = {fpId};", ct);

                        if (updated == 0)
                        {
                            var newId = Guid.NewGuid();
                            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                                INSERT INTO TR_ProjectUnitFloorPlan
                                    (ID, ProjectID, ProjectFloorPlanID, UnitID, FlagActive, CreateDate, CreateBy, UpdateDate, UpdateBy)
                                VALUES
                                    ({newId}, {projectId}, {fpId}, {unitId}, 1, {now}, {userId}, {now}, {userId});", ct);
                        }
                    }
                }

                await tx.CommitAsync(ct);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(ct);
                return false;
            }
        }

        public List<ListFloorPlanByUnit> GetFloorPlansByUnit(Guid unitId)
        {
            var query =
                from t1 in _context.TR_ProjectUnitFloorPlans
                where t1.UnitID == unitId && t1.FlagActive == true
                join t2 in _context.TR_ProjectFloorPlans
                    on t1.ProjectFloorPlanID equals t2.ID into gj
                from t2 in gj.DefaultIfEmpty() // LEFT JOIN
                select new ListFloorPlanByUnit
                {
                    ID = t1.ID,
                    FloorPlanID = t1.ProjectFloorPlanID,
                    FileName = t2 != null ? (t2.FileName ?? string.Empty) : string.Empty,
                    FilePath = t2 != null ? (t2.FilePath ?? string.Empty) : string.Empty,
                    MimeType = t2 != null ? (t2.MimeType ?? string.Empty) : string.Empty
                };

            return query.OrderBy(x => x.FileName).ToList();                  
        }

        public bool DeactivateUnitFloorPlan(Guid id, int userId)
        {
            if (id == Guid.Empty) return false;

            var now = DateTime.Now;
            // Sync version
            var affected = _context.Database.ExecuteSqlInterpolated($@"
            UPDATE TR_ProjectUnitFloorPlan
               SET FlagActive = 0,
                   UpdateDate = {now},
                   UpdateBy   = {userId}
             WHERE ID = {id};");

            return affected > 0;
        }
    }
}
