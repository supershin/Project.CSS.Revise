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
        Task<UploadResult> UploadFloorplansAsync(UploadFileProjectFloorPlan req, int userId, CancellationToken ct = default);
        Task<BasicResult> UpdateFloorplanAsync(UpdateFloorplanRequest req, int userId, CancellationToken ct = default);
        Task<BasicResult> DeleteFloorplanAsync(Guid floorPlanId, int userId, CancellationToken ct = default);
    }
    public class ProjectandunitfloorplanRepo : IProjectandunitfloorplanRepo
    {
        private readonly CSSContext _context;
        private readonly IWebHostEnvironment _env;
        public ProjectandunitfloorplanRepo(CSSContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
                                ,T1.[AddrNo]
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
                                AddrNo = Commond.FormatExtension.NullToString(reader["AddrNo"]),
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

        public async Task<UploadResult> UploadFloorplansAsync(UploadFileProjectFloorPlan req, int userId, CancellationToken ct = default)
        {
            // ---- validate
            if (string.IsNullOrWhiteSpace(req.ProjectID))
                return new UploadResult { Success = false, Message = "ProjectID is required." };

            if (req.Images is null || req.Images.Count == 0)
                return new UploadResult { Success = false, Message = "No files to upload." };

            // ---- resolve physical root
            var webRoot = !string.IsNullOrWhiteSpace(req.ApplicationPath)
                ? req.ApplicationPath!
                : _env.WebRootPath ?? Path.Combine(AppContext.BaseDirectory, "wwwroot");

            // ---- ensure target folder: /ProjectFloorPlan/{ProjectID}
            var projectId = req.ProjectID.Trim();
            var relFolder = Path.Combine("ProjectFloorPlan", projectId);                  // relative (no leading slash)
            var absFolder = Path.Combine(webRoot, relFolder);                             // physical path
            Directory.CreateDirectory(absFolder);                                         // create if missing

            // transaction
            await using var tx = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                var now = DateTime.Now;
                var saved = new List<(Guid, string, string)>();

                for (int i = 0; i < req.Images.Count; i++)
                {
                    var file = req.Images[i];
                    if (file.Length <= 0) continue;

                    // name preference: req.FileName[i] -> file.FileName
                    var desiredName = (i < req.FileName.Count && !string.IsNullOrWhiteSpace(req.FileName[i]))
                        ? req.FileName[i].Trim()
                        : file.FileName;

                    // sanitize and ensure extension
                    var safeName = SanitizeFileName(desiredName);
                    safeName = EnsureExtension(safeName, file.FileName);

                    // ensure unique inside this folder
                    safeName = EnsureUniqueFileName(absFolder, safeName);

                    // physical + relative web path
                    var absPath = Path.Combine(absFolder, safeName);
                    var relWebPath = "/" + Path.Combine(relFolder, safeName).Replace("\\", "/"); // e.g. /ProjectFloorPlan/ABC/xxx.png

                    // save file
                    await using (var fs = new FileStream(absPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await file.CopyToAsync(fs, ct);
                    }

                    // detect mime
                    var mime = !string.IsNullOrWhiteSpace(file.ContentType)
                        ? file.ContentType
                        : GuessMimeFromExtension(Path.GetExtension(safeName));

                    // insert row
                    var row = new TR_ProjectFloorPlan
                    {
                        ID = Guid.NewGuid(),
                        ProjectID = projectId,
                        FileName = safeName,
                        FilePath = relWebPath,      // always store as /ProjectFloorPlan/{ProjectID}/{file}
                        MimeType = mime,
                        FlagActive = true,
                        CreateDate = now,
                        CreateBy = userId,
                        UpdateDate = now,
                        UpdateBy = userId
                    };

                    _context.TR_ProjectFloorPlans.Add(row);
                    await _context.SaveChangesAsync(ct);

                    saved.Add((row.ID, row.FileName, row.FilePath));
                }

                await tx.CommitAsync(ct);

                return new UploadResult
                {
                    Success = true,
                    Message = saved.Count > 0 ? "Uploaded successfully." : "No files were saved.",
                    SavedCount = saved.Count,
                    Items = saved
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync(ct);
                return new UploadResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<BasicResult> UpdateFloorplanAsync(UpdateFloorplanRequest req, int userId, CancellationToken ct = default)
        {
            if (req.FloorPlanID == Guid.Empty)
                return new BasicResult { Success = false, Message = "FloorPlanID is required." };
            if (string.IsNullOrWhiteSpace(req.ProjectID))
                return new BasicResult { Success = false, Message = "ProjectID is required." };

            var row = await _context.TR_ProjectFloorPlans
                .FirstOrDefaultAsync(x => x.ID == req.FloorPlanID, ct);

            if (row is null)
                return new BasicResult { Success = false, Message = "Floorplan not found." };

            // 1) Block if mapped
            var inUse = await _context.TR_ProjectUnitFloorPlans
                .AnyAsync(x => x.ProjectFloorPlanID == req.FloorPlanID && x.FlagActive == true, ct);

            if (inUse)
                return new BasicResult { Success = false, Message = "This floor plan is already mapped to units. Unmap it before editing." };

            var webRoot = _env.WebRootPath ?? Path.Combine(AppContext.BaseDirectory, "wwwroot");
            var relFolder = Path.Combine("ProjectFloorPlan", row.ProjectID);
            var absFolder = Path.Combine(webRoot, relFolder);
            Directory.CreateDirectory(absFolder);

            var now = DateTime.Now;
            string? newFileName = null;
            string? newRelPath = null;
            string? newMime = null;

            // 2) Replace file if provided
            if (req.NewImage is not null && req.NewImage.Length > 0)
            {
                var desired = !string.IsNullOrWhiteSpace(req.NewFileName) ? req.NewFileName.Trim() : (req.NewImage.FileName ?? "image");
                desired = SanitizeFileName(desired);
                desired = EnsureExtension(desired, req.NewImage.FileName);
                desired = EnsureUniqueFileName(absFolder, desired);

                var absPath = Path.Combine(absFolder, desired);
                await using (var fs = new FileStream(absPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await req.NewImage.CopyToAsync(fs, ct);
                }

                newFileName = desired;
                newRelPath = "/" + Path.Combine(relFolder, desired).Replace("\\", "/");
                newMime = string.IsNullOrWhiteSpace(req.NewImage.ContentType)
                    ? GuessMimeFromExtension(Path.GetExtension(desired))
                    : req.NewImage.ContentType;
            }
            else
            {
                // 3) Rename only (physical + DB) if name changed
                if (!string.IsNullOrWhiteSpace(req.NewFileName) && !string.Equals(req.NewFileName, row.FileName, StringComparison.Ordinal))
                {
                    var desired = SanitizeFileName(req.NewFileName.Trim());
                    desired = EnsureExtension(desired, row.FileName);
                    desired = EnsureUniqueFileName(absFolder, desired);

                    var oldAbs = Path.Combine(webRoot, row.FilePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    var newAbs = Path.Combine(absFolder, desired);

                    // if old physical exists, rename it
                    if (System.IO.File.Exists(oldAbs))
                    {
                        System.IO.File.Move(oldAbs, newAbs, overwrite: false);
                    }
                    // update new file name/path
                    newFileName = desired;
                    newRelPath = "/" + Path.Combine(relFolder, desired).Replace("\\", "/");
                }
            }

            // 4) Persist
            if (newFileName is not null) row.FileName = newFileName;
            if (newRelPath is not null) row.FilePath = newRelPath;
            if (newMime is not null) row.MimeType = newMime;

            row.UpdateBy = userId;
            row.UpdateDate = now;

            await _context.SaveChangesAsync(ct);

            return new BasicResult { Success = true, Message = "Floor plan updated." };
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "file";
            // Trim and replace invalid file-name chars
            var trimmed = name.Trim();
            foreach (var c in Path.GetInvalidFileNameChars())
                trimmed = trimmed.Replace(c, '_');

            // Collapse dangerous reserved names on Windows
            var bad = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { "CON", "PRN", "AUX", "NUL", "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
          "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9" };
            if (bad.Contains(Path.GetFileNameWithoutExtension(trimmed)))
                trimmed = "_" + trimmed;

            return trimmed;
        }

        private static string EnsureExtension(string desiredName, string fallbackOriginal)
        {
            var ext = Path.GetExtension(desiredName);
            if (!string.IsNullOrWhiteSpace(ext)) return desiredName;

            var fbExt = Path.GetExtension(fallbackOriginal);
            if (string.IsNullOrWhiteSpace(fbExt)) fbExt = ".png";
            return desiredName + fbExt;
        }

        private static string EnsureUniqueFileName(string folder, string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            var candidate = fileName;
            int i = 1;

            while (System.IO.File.Exists(Path.Combine(folder, candidate)))
            {
                candidate = $"{name} ({i}){ext}";
                i++;
            }
            return candidate;
        }

        private static string GuessMimeFromExtension(string? ext)
        {
            ext = (ext ?? "").ToLowerInvariant();
            return ext switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".svg" => "image/svg+xml",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            };
        }

        public async Task<BasicResult> DeleteFloorplanAsync(Guid floorPlanId, int userId, CancellationToken ct = default)
        {
            if (floorPlanId == Guid.Empty)
                return new BasicResult { Success = false, Message = "FloorPlanID is required." };

            // 1) หา Floorplan ที่ยัง Active อยู่
            var fp = await _context.TR_ProjectFloorPlans.FirstOrDefaultAsync(x => x.ID == floorPlanId && x.FlagActive == true, ct);

            if (fp == null)
                return new BasicResult { Success = false, Message = "Floor plan not found or already inactive." };

            // 2) บล็อคถ้ายังถูก map อยู่กับ Unit ใด ๆ ที่ยัง Active
            var inUse = await _context.TR_ProjectUnitFloorPlans.AnyAsync(x => x.ProjectFloorPlanID == floorPlanId && x.FlagActive == true, ct);

            if (inUse)
                return new BasicResult { Success = false, Message = "This floor plan is mapped to units. Unmap it before deleting." };

            // 3) Soft delete
            fp.FlagActive = false;
            fp.UpdateBy = userId;
            fp.UpdateDate = DateTime.Now; // หรือ DateTime.UtcNow แล้วแต่คอนเวนชันของโปรเจ็กต์

            try
            {
                await _context.SaveChangesAsync(ct);
                return new BasicResult { Success = true, Message = "Floor plan deleted (soft delete)." };
            }
            catch (Exception ex)
            {
                // ถ้าต้องการ log ไว้
                // _logger.LogError(ex, "DeleteFloorplanAsync failed for {FloorPlanId}", floorPlanId);
                return new BasicResult { Success = false, Message = "Delete failed. " + ex.Message };
            }
        }

    }
}
