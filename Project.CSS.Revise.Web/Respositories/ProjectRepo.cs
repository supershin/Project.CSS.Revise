using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.Project;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IProjectRepo
    {
        public List<ProjectSettingModel.ListProjectItem> GetlistProjectTable(ProjectSettingModel.ProjectFilter filter);
        public ProjectSettingModel.ReturnMessage SaveEditProject(ProjectSettingModel.DataProjectIUD Model);
    }
    public class ProjectRepo : IProjectRepo
    {
        private readonly CSSContext _context;

        public ProjectRepo(CSSContext context)
        {
            _context = context;
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

    }
}
