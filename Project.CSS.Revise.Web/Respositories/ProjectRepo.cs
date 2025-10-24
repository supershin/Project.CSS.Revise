using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.Project;
using Project.CSS.Revise.Web.Service;
using System.Data;
using System.Reflection;
namespace Project.CSS.Revise.Web.Respositories
{
    public interface IProjectRepo
    {
        public List<ProjectSettingModel.ListProjectItem> GetlistProjectTable(ProjectSettingModel.ProjectFilter filter);
        public ProjectSettingModel.ReturnMessage SaveEditProject(ProjectSettingModel.DataProjectIUD Model);
        public ProjectSettingModel.ReturnMessage SaveUpdateUnitViewTempBlk(string projectID, int UserID);
    }
    public static class ObjectCopyExtensions
    {
        public static TTarget CopyToSafe<TTarget>(this object source, TTarget target)
        {
            if (source == null || target == null) return target;

            var sProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                               .Where(p => p.CanRead);

            var tProps = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                               .Where(p => p.CanWrite)
                               .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            foreach (var sp in sProps)
            {
                if (!tProps.TryGetValue(sp.Name, out var tp)) continue;

                var val = sp.GetValue(source, null);
                if (val == null)
                {
                    if (!tp.PropertyType.IsValueType || Nullable.GetUnderlyingType(tp.PropertyType) != null)
                        tp.SetValue(target, null);
                    continue;
                }

                try
                {
                    var tType = Nullable.GetUnderlyingType(tp.PropertyType) ?? tp.PropertyType;
                    if (tType.IsAssignableFrom(val.GetType()))
                    {
                        tp.SetValue(target, val);
                    }
                    else
                    {
                        var conv = Convert.ChangeType(val, tType, System.Globalization.CultureInfo.InvariantCulture);
                        tp.SetValue(target, conv);
                    }
                }
                catch
                {
                    // ข้าม field ที่แปลงไม่ได้
                }
            }
            return target;
        }
    }

    public class ProjectRepo : IProjectRepo
    {
        private readonly CSSContext _context;
        private readonly SystemConstantCentralize _central;
        public ProjectRepo(CSSContext context, SystemConstantCentralize central)
        {
            _context = context;
            _central = central;
        }

        public List<ProjectSettingModel.ListProjectItem> GetlistProjectTable(ProjectSettingModel.ProjectFilter filter)
        {
            var result = new List<ProjectSettingModel.ListProjectItem>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"

                        --DECLARE @L_ProjectID        NVARCHAR(100) = '' ;
                        --DECLARE @L_BUID             NVARCHAR(100) = '' ;
                        --DECLARE @L_ProjectStatus    NVARCHAR(100) = '' ;
                        --DECLARE @L_ProjectPartner   NVARCHAR(100) = '' ;
                        --DECLARE @L_Company          NVARCHAR(100) = '' ;

                        SELECT 
                            p.ProjectID
                          , cp.CompanyID
                          , c.[Name]        AS Companyname
                          , b.ID            AS BUID
                          , b.[Name]        AS BUname
                          , p.PartnerID
                          , e.[Name]        AS Partnername
                          , p.ProjectName
                          , p.ProjectName_Eng
                          , p.ProjectType
                          , ps.StatusID     AS ProjectStatusID
                          , eps.[Name]      AS ProjectStatus
                          , lf.[ID]         AS LandOfficeID
                          , lf.[Name]       AS LandOfficename
                          , z.ProjectZoneID         -- e.g. 206,450
                          , z.ProjectZonename       -- e.g. Zone 2,All Zone
                        FROM tm_Project p
                        LEFT JOIN tm_BUProject_Mapping m   ON p.ProjectID = m.ProjectID
                        LEFT JOIN tm_BU b                  ON m.BUID      = b.ID
                        LEFT JOIN TR_ProjectStatus ps      ON p.ProjectID = ps.ProjectID
                        LEFT JOIN tm_Ext eps               ON ps.StatusID = eps.ID   
                        LEFT JOIN TR_CompanyProject cp     ON p.ProjectID = cp.ProjectID
                        LEFT JOIN tm_Company c             ON cp.CompanyID = c.ID
                        LEFT JOIN tm_Ext e                 ON p.PartnerID  = e.ID
                        LEFT JOIN TR_ProjectLandOffice plf ON p.ProjectID = plf.ProjectID
                        LEFT JOIN tm_LandOffice lf         ON plf.LandOfficeID = lf.ID

                        -- 👇 รวม Zone ต่อโปรเจกต์ด้วย STRING_AGG
                        OUTER APPLY (
                            SELECT
                                STRING_AGG(CONVERT(varchar(50), dz.ProjectZoneID), ',') 
                                    WITHIN GROUP (ORDER BY dz.ProjectZoneID)                         AS ProjectZoneID,
                                STRING_AGG(epz.[Name], ',') 
                                    WITHIN GROUP (ORDER BY dz.ProjectZoneID)                         AS ProjectZonename
                            FROM (
                                SELECT DISTINCT ProjectID, ProjectZoneID
                                FROM TR_ProjectZone_Mapping
                                WHERE ProjectID = p.ProjectID
                            ) dz
                            JOIN tm_Ext epz ON epz.ID = dz.ProjectZoneID
                        ) z

                        WHERE p.FlagActive = 1
                          AND (
                                @L_ProjectID = ''
                                OR CHARINDEX(',' + CAST(p.ProjectID AS NVARCHAR) + ',', ',' + @L_ProjectID + ',') > 0
                              )
                          AND (
                                @L_BUID = ''
                                OR CHARINDEX(',' + CAST(b.ID AS NVARCHAR) + ',', ',' + @L_BUID + ',') > 0
                              )
                          AND (
                                @L_ProjectStatus = ''
                                OR CHARINDEX(',' + CAST(ps.StatusID AS NVARCHAR) + ',', ',' + @L_ProjectStatus + ',') > 0
                              )
                          AND (
                                @L_ProjectPartner = ''
                                OR CHARINDEX(',' + CAST(p.PartnerID AS NVARCHAR) + ',', ',' + @L_ProjectPartner + ',') > 0
                              )
                          AND (
                                @L_Company = ''
                                OR CHARINDEX(',' + CAST(cp.CompanyID AS NVARCHAR) + ',', ',' + @L_Company + ',') > 0
                              )
                        ORDER BY b.ID;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_BUID", filter.L_BUID ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectStatus", filter.L_ProjectStatus ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectPartner", filter.L_ProjectPartner ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_Company", filter.L_Company ?? string.Empty));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ProjectSettingModel.ListProjectItem
                            {
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),

                                CompanyID = Commond.FormatExtension.NullToString(reader["CompanyID"]),
                                CompanyName = Commond.FormatExtension.NullToString(reader["Companyname"]),

                                BUID = Commond.FormatExtension.NullToString(reader["BUID"]),
                                BUName = Commond.FormatExtension.NullToString(reader["BUname"]),

                                PartnerID = Commond.FormatExtension.NullToString(reader["PartnerID"]),
                                PartnerName = Commond.FormatExtension.NullToString(reader["Partnername"]),

                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]),
                                ProjectName_Eng = Commond.FormatExtension.NullToString(reader["ProjectName_Eng"]),
                                ProjectType = Commond.FormatExtension.NullToString(reader["ProjectType"]),
                                ProjectStatusID = Commond.FormatExtension.NullToString(reader["ProjectStatusID"]),
                                ProjectStatus = Commond.FormatExtension.NullToString(reader["ProjectStatus"]),

                                LandOfficeID = Commond.FormatExtension.NullToString(reader["LandOfficeID"]),
                                LandOfficeName = Commond.FormatExtension.NullToString(reader["LandOfficename"]),

                                ProjectZoneID = Commond.FormatExtension.NullToString(reader["ProjectZoneID"]),
                                ProjectZonename = Commond.FormatExtension.NullToString(reader["ProjectZonename"]),
                            });
                        }
                    }
                }
            }

            return result;
        }

        public ProjectSettingModel.ReturnMessage SaveEditProject(ProjectSettingModel.DataProjectIUD Model)
        {
            var ret = new ProjectSettingModel.ReturnMessage { IsSuccess = false, Message = "" };

            // ---------- Validate required ----------
            var missing = new System.Collections.Generic.List<string>();
            string projectId = (Model?.ProjectID ?? "").Trim();

            if (string.IsNullOrWhiteSpace(projectId)) missing.Add(nameof(Model.ProjectID));
            if (!Model?.BUID.HasValue ?? true) missing.Add(nameof(Model.BUID));
            if (!Model?.PartnerID.HasValue ?? true) missing.Add(nameof(Model.PartnerID));
            if (string.IsNullOrWhiteSpace(Model?.ProjectName)) missing.Add(nameof(Model.ProjectName));
            if (string.IsNullOrWhiteSpace(Model?.ProjectType)) missing.Add(nameof(Model.ProjectType));
            if (!Model?.ProjectStatus.HasValue ?? true) missing.Add(nameof(Model.ProjectStatus));
            if (!Model?.ProjectZoneID.HasValue ?? true) missing.Add(nameof(Model.ProjectZoneID));
            if (!Model?.UserID.HasValue ?? true) missing.Add(nameof(Model.UserID));

            var type = (Model?.ProjectType ?? "").Trim().ToUpperInvariant();
            if (!(type == "C" || type == "H"))
                missing.Add(nameof(Model.ProjectType) + " must be 'C' or 'H'");

            if (missing.Count > 0)
            {
                ret.Message = "Required fields missing/invalid: " + string.Join(", ", missing);
                return ret;
            }

            using var tx = _context.Database.BeginTransaction();
            try
            {
                // ---------- tm_Project ----------
                var proj = _context.tm_Projects.SingleOrDefault(p => p.ProjectID == projectId);
                if (proj == null)
                {
                    ret.Message = $"Project not found: {projectId}";
                    return ret;
                }

                proj.PartnerID = Model?.PartnerID;
                proj.ProjectName = Model?.ProjectName?.Trim();
                proj.ProjectName_Eng = Model?.ProjectName_Eng?.Trim();
                proj.ProjectType = type;                  // "C" or "H"
                proj.UpdateDate = DateTime.Now;
                proj.UpdateBy = Model?.UserID;

                _context.SaveChanges();

                // ---------- TR_ProjectStatus (upsert by ProjectID) ----------
                var statusRow = _context.TR_ProjectStatuses.SingleOrDefault(s => s.ProjectID == projectId);
                if (statusRow == null)
                {
                    statusRow = new TR_ProjectStatus
                    {
                        ProjectID = projectId,
                        StatusID = Model?.ProjectStatus
                    };
                    _context.TR_ProjectStatuses.Add(statusRow);
                }
                else
                {
                    statusRow.StatusID = Model?.ProjectStatus;
                    _context.TR_ProjectStatuses.Update(statusRow);
                }
                _context.SaveChanges();

                // ---------- tm_BUProject_Mapping (upsert by ProjectID) ----------
                var buMap = _context.tm_BUProject_Mappings.SingleOrDefault(m => m.ProjectID == projectId);
                if (buMap == null)
                {
                    buMap = new tm_BUProject_Mapping
                    {
                        ProjectID = projectId,
                        BUID = Model?.BUID
                    };
                    _context.tm_BUProject_Mappings.Add(buMap);
                }
                else
                {
                    buMap.BUID = Model?.BUID;
                    _context.tm_BUProject_Mappings.Update(buMap);
                }
                _context.SaveChanges();

                // ---------- NEW: TR_CompanyProject (upsert by ProjectID) ----------
                if (Model?.CompanyID.HasValue == true)
                {
                    var compMap = _context.TR_CompanyProjects.SingleOrDefault(x => x.ProjectID == projectId);
                    if (compMap == null)
                    {
                        compMap = new TR_CompanyProject
                        {
                            ProjectID = projectId,
                            CompanyID = Model.CompanyID
                        };
                        _context.TR_CompanyProjects.Add(compMap);
                    }
                    else
                    {
                        compMap.CompanyID = Model.CompanyID;
                        _context.TR_CompanyProjects.Update(compMap);
                    }
                    _context.SaveChanges();
                }
                // If CompanyID is null, we leave existing mapping as-is (no delete).

                // ---------- TR_ProjectLandOffice (upsert by ProjectID) ----------
                var landOf = _context.TR_ProjectLandOffices.SingleOrDefault(m => m.ProjectID == projectId);
                if (landOf == null)
                {
                    landOf = new TR_ProjectLandOffice
                    {
                        ProjectID = projectId,
                        LandOfficeID = Model?.LandOfficeID
                    };
                    _context.TR_ProjectLandOffices.Add(landOf);
                }
                else
                {
                    landOf.LandOfficeID = Model?.LandOfficeID;
                    _context.TR_ProjectLandOffices.Update(landOf);
                }
                _context.SaveChanges();

                // ---------- TR_ProjectZone_Mapping (delete all + insert new) ----------
                _context.Database.ExecuteSqlRaw(
                    "DELETE FROM TR_ProjectZone_Mapping WHERE ProjectID = {0}", projectId
                );

                var newZone = new TR_ProjectZone_Mapping
                {
                    ProjectID = projectId,
                    ProjectZoneID = Model.ProjectZoneID!.Value
                };
                _context.Set<TR_ProjectZone_Mapping>().Add(newZone);
                _context.SaveChanges();

                tx.Commit();
                ret.IsSuccess = true;
                ret.Message = "Edit saved successfully.";
                return ret;
            }
            catch (Exception ex)
            {
                try { tx.Rollback(); } catch { /* ignore */ }
                ret.IsSuccess = false;
                ret.Message = "Save failed: " + ex.Message;
                return ret;
            }
        }

        public ProjectSettingModel.ReturnMessage SaveUpdateUnitViewTempBlk(string projectID , int UserID)
        {
            var ret = new ProjectSettingModel.ReturnMessage
            {
                IsSuccess = false,
                Message = ""
            };

            try
            {
                if (string.IsNullOrWhiteSpace(projectID))
                {
                    ret.Message = "ProjectID is required.";
                    return ret;
                }

                // Set EF command timeout to 1 hour
                _context.Database.SetCommandTimeout(TimeSpan.FromHours(1));

                // 1) Get data from CENTRALIZE API (sync method)
                var units = GetUnitViewTempBlk_API(projectID);
                if (units == null || units.Count == 0)
                {
                    ret.Message = "No unit data found for synchronization.";
                    return ret;
                }

                using (var tx = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // 2) Delete old records via stored procedure
                        DeleteViewTempBlkUnit(projectID);

                        // 3) Insert new records using EF (AddRange + single SaveChanges)
                        InsertUnitViewTempBlk_ByEF(units);

                        // 4) Continue with related updates (same as original code)
                        UpdateUnitContract(projectID);
                        InserSyncLogs(projectID, "Sync Project From CRM" , UserID);

                        tx.Commit();

                        ret.IsSuccess = true;
                        ret.Message = $"Successfully synchronized {units.Count} records for ProjectID = {projectID}.";
                        return ret;
                    }
                    catch (Exception ex)
                    {
                        try { tx.Rollback(); } catch { /* ignore */ }

                        ret.IsSuccess = false;
                        ret.Message = "Transaction failed: " + ex.Message;
                        return ret;
                    }
                }
            }
            catch (Exception ex)
            {
                ret.IsSuccess = false;
                ret.Message = "Save failed: " + ex.Message;
                return ret;
            }
        }


        public List<ViewTempBlkUnit> GetUnitViewTempBlk_API(string ProjectID)
        {
            try
            {
                WebAPIRest api = new WebAPIRest();
                string url = string.Format(_central.CentralizeApiUrl + "api/Master/GetViewTempBlkList");
                var data = api.RequestPostWebAPI<ObjectAPI<List<ViewTempBlkUnit>>>(url, new { ProjectID = ProjectID }, _central.CentralizeAuthorize).Data;
                return data.ToList();
            }
            catch (Exception)
            {
                return new List<ViewTempBlkUnit>();
            }
        }

        // ========== INSERT แบบ EF (เทียบเท่าของเดิม แต่เร็วกว่า) ==========
        private void InsertUnitViewTempBlk_ByEF(IEnumerable<ViewTempBlkUnit> items)
        {
            if (items == null) return;
            var list = new List<vw_ITF_TempBlakUnit>(capacity: 1024);

            foreach (var obj in items)
            {
                var entity = new vw_ITF_TempBlakUnit();
                obj.CopyToSafe(entity);     // ดู extension ด้านล่าง
                entity.ID = Guid.NewGuid(); // ของเดิมก็ gen Guid ใหม่
                _context.Entry(entity).State = EntityState.Added;
                list.Add(entity);
            }

            if (list.Count > 0)
            {
                _context.vw_ITF_TempBlakUnits.AddRange(list);
                _context.SaveChanges(); // ครั้งเดียว
            }
        }

        // ========== Stored Proc: DELETE (ของเดิม ใช้ต่อได้เลย) ==========
        public int DeleteViewTempBlkUnit(string projectID)
        {
            if (string.IsNullOrWhiteSpace(projectID))
                return 0;
            var p = new SqlParameter("@ProjectID", projectID);
            const string sql = @"DELETE FROM vw_ITF_TempBlakUnit WHERE FREPRJNO = @ProjectID;";
            return _context.Database.ExecuteSqlRaw(sql, p);
        }


        // (ถ้าจะใช้) เรียกคู่เดิมหลัง insert เสร็จ
        private void UpdateUnitContract(string projectID)
        {
            var p = new SqlParameter("@ProjectID", projectID);
            _context.Database.ExecuteSqlRaw("EXEC sp_InitialData @ProjectID", p);
            _context.Database.ExecuteSqlRaw("EXEC sp_PR_UpdateContractCodeVerify");
        }

        // (ถ้าจะใช้) log sync
        private void InserSyncLogs(string projectID, string detail , int USerID)
        {
            var item = new TR_Sync_Log
            {
                ProjectID = projectID,
                Detail = detail,
                CreateDate = DateTime.Now,
                CreateBy = USerID
            };
            _context.TR_Sync_Logs.Add(item);
            _context.SaveChanges();
        }
    }
}
