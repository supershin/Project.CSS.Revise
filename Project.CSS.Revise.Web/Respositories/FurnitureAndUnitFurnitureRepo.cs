using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture;
using System.Data;
using static Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture.FurnitureAndUnitFurnitureModel;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IFurnitureAndUnitFurnitureRepo
    {
        public List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem> GetlistUnitFurniture(FurnitureAndUnitFurnitureModel.UnitFurnitureFilter filter);
        Task<bool> SaveFurnitureProjectMappingAsync(SaveFurnitureProjectMappingRequest req, int userId, CancellationToken ct = default);
        public UnitFurnitureModel? GetUnitFurniture(Guid unitId);
        Task<bool> UpdateFurnitureProjectMappingAsync(UpdateFurnitureProjectMappingRequest req, int userId, CancellationToken ct = default);
    }
    public class FurnitureAndUnitFurnitureRepo : IFurnitureAndUnitFurnitureRepo
    {
        private readonly CSSContext _context;

        public FurnitureAndUnitFurnitureRepo(CSSContext context)
        {
            _context = context;
        }

        public List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem> GetlistUnitFurniture(FurnitureAndUnitFurnitureModel.UnitFurnitureFilter filter)
        {
            var result = new List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                                SELECT T1.[ID]
                                    , CASE WHEN T2.ID IS NOT NULL THEN 1 ELSE 0 END AS ISCheck
                                    , T1.[UnitCode]
                                    , T1.[ProjectID]
                                    , T1.[UnitType]
                                    , T4.QTYFurnitureUnit
                                    , T2.[CheckStatusID]
                                    , T3.[Name] AS CheckStatusName
                                    , LTRIM(RTRIM(ISNULL(T5.[FirstName], N'') + N' ' + ISNULL(T5.[LastName], N''))) AS FullnameTH
                                    , T2.UpdateDate
                                FROM [tm_Unit] T1 WITH (NOLOCK)
                                LEFT JOIN TR_UnitFurniture T2 WITH (NOLOCK) ON T1.ID = T2.UnitID
                                LEFT JOIN [tm_Ext] T3 WITH (NOLOCK) ON T3.ID = T2.[CheckStatusID]
                                LEFT JOIN tm_BUProject_Mapping BPM WITH (NOLOCK) ON T1.ProjectID = BPM.ProjectID
                                LEFT JOIN [tm_BU] BU WITH (NOLOCK) ON BPM.BUID = BU.ID
                                LEFT JOIN (
                                    SELECT T1.[UnitFurnitureID]
                                        , SUM(T1.[Amount]) AS QTYFurnitureUnit
                                    FROM [TR_UnitFurniture_Detail] T1 WITH (NOLOCK)
                                    WHERE T1.[FlagActive] = 1
                                    GROUP BY T1.[UnitFurnitureID]
                                ) T4 ON T2.ID = T4.UnitFurnitureID
                                LEFT JOIN [tm_User] T5 WITH (NOLOCK) ON T2.UpdateBy = T5.ID
                                WHERE T1.[ProjectID] = @L_ProjectID
                                  AND T1.FlagActive = 1
                                  AND (
                                        @L_UnitType = ''
                                        OR (',' + @L_UnitType + ',' LIKE '%,' + CONVERT(VARCHAR, T1.[UnitType]) + ',%')
                                      )
                                 AND (
                                        @L_BUG = ''
                                        OR (',' + @L_BUG + ',' LIKE '%,' + CONVERT(VARCHAR, BU.ID) + ',%')
                                     )
                                 AND (
                                        @Src_UnitCode = ''
                                        OR T1.[UnitCode] LIKE '%' + @Src_UnitCode + '%'
                                     )
                                ORDER BY T1.[UnitCode];
                            ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_BUG", filter.L_BUG ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@L_UnitType", filter.L_UnitType ?? string.Empty));
                    cmd.Parameters.Add(new SqlParameter("@Src_UnitCode", filter.Src_UnitCode ?? string.Empty));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new FurnitureAndUnitFurnitureModel.UnitFurnitureListItem
                            {
                                ID = Commond.FormatExtension.NullToString(reader["ID"]),
                                ISCheck = Commond.FormatExtension.Nulltoint(reader["ISCheck"]) == 1,
                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                UnitType = Commond.FormatExtension.NullToString(reader["UnitType"]),
                                QTYFurnitureUnit = Commond.FormatExtension.NullToString(reader["QTYFurnitureUnit"]),
                                CheckStatusID = Commond.FormatExtension.NullToString(reader["CheckStatusID"]),
                                CheckStatusName = Commond.FormatExtension.NullToString(reader["CheckStatusName"]),
                                FullnameTH = Commond.FormatExtension.NullToString(reader["FullnameTH"]),
                                UpdateDate = Commond.FormatExtension.ToStringFrom_DD_MM_YYYY_To_DD_MM_YYYY(reader["UpdateDate"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        public async Task<bool> SaveFurnitureProjectMappingAsync(SaveFurnitureProjectMappingRequest req,int userId,CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(req.ProjectID))
            {
                throw new ArgumentException("ProjectID is required.");
            }               
            if (req.Units is null || req.Units.Count == 0)
            {
                throw new ArgumentException("Units is required.");
            }              
            if (req.Furnitures is null || req.Furnitures.Count == 0)
            {
                throw new ArgumentException("Furnitures is required.");
            }
               
            using var tx = await _context.Database.BeginTransactionAsync(ct);

            try
            {
                var now = DateTime.UtcNow; // or DateTime.Now if you prefer

                foreach (var u in req.Units)
                {
                    if (!Guid.TryParse(u.id, out var unitId))
                    {
                        throw new ArgumentException($"Invalid UnitID: {u.id}");
                    }
                        
                    // ---- 1) Upsert TR_UnitFurniture via C# (EF) ----
                    var uf = await _context.TR_UnitFurnitures.FirstOrDefaultAsync(x => x.ProjectID == req.ProjectID && x.UnitID == unitId, ct);

                    if (uf == null)
                    {
                        uf = new TR_UnitFurniture
                        {
                            ID = Guid.NewGuid(),
                            ProjectID = req.ProjectID!,
                            UnitID = unitId,
                            FlagActive = true,
                            CreateDate = now,
                            CreateBy = userId,
                            UpdateDate = now,
                            UpdateBy = userId
                        };
                        _context.TR_UnitFurnitures.Add(uf);
                    }
                    else
                    {
                        uf.FlagActive = true;
                        uf.UpdateDate = now;
                        uf.UpdateBy = userId;
                        _context.TR_UnitFurnitures.Update(uf);
                    }

                    await _context.SaveChangesAsync(ct); // ensure uf.ID is persisted

                    // ---- 2) REMOVE-FIRST (raw SQL bulk update) ----
                    // Deactivate all existing details for this UnitFurniture
                    await _context.Database.ExecuteSqlRawAsync(@" UPDATE TR_UnitFurniture_Detail SET FlagActive = 0, UpdateDate = GETDATE(),UpdateBy = {0} WHERE UnitFurnitureID = {1}", parameters: new object[] { userId, uf.ID }, cancellationToken: ct);

                    // ---- 3) Re-apply selected furnitures via C# (EF) ----
                    // Build a set of FurnitureIDs for quick lookups
                    var furnitureIds = req.Furnitures
                        .Select(f =>
                        {
                            if (!int.TryParse(f.id, out var fid))
                                throw new ArgumentException($"Invalid FurnitureID: {f.id}");
                            return (fid, qty: (decimal)f.qty);
                        })
                        .ToList();

                    // Load all existing details for this UF once
                    var existingDetails = await _context.TR_UnitFurniture_Details
                        .Where(d => d.UnitFurnitureID == uf.ID)
                        .ToListAsync(ct);

                    foreach (var (fId, qty) in furnitureIds)
                    {
                        var d = existingDetails.FirstOrDefault(x => x.FurnitureID == fId);
                        if (d != null)
                        {
                            // update existing
                            d.Amount = Commond.FormatExtension.Nulltoint(qty);
                            d.FlagActive = true;
                            d.UpdateDate = now;
                            d.UpdateBy = userId;
                            _context.TR_UnitFurniture_Details.Update(d);
                        }
                        else
                        {
                            // insert new
                            var nd = new TR_UnitFurniture_Detail
                            {
                                // ID is int and Identity
                                UnitFurnitureID = uf.ID,
                                FurnitureID = fId,
                                Amount = Commond.FormatExtension.Nulltoint(qty),
                                FlagActive = true,
                                CreateDate = now,
                                CreateBy = userId,
                                UpdateDate = now,
                                UpdateBy = userId
                            };
                            _context.TR_UnitFurniture_Details.Add(nd);
                        }
                    }
                    await _context.SaveChangesAsync(ct);
                }

                await tx.CommitAsync(ct);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(ct);
                throw;
            }
        }

        public UnitFurnitureModel? GetUnitFurniture(Guid unitId)
        {
            var result = (from u in _context.tm_Units
                          join uf in _context.TR_UnitFurnitures
                              on u.ID equals uf.UnitID into gj
                          from uf in gj.DefaultIfEmpty()
                          where u.ID == unitId
                          select new UnitFurnitureModel
                          {
                              UnitID = u.ID,
                              ProjectID = u.ProjectID,
                              UnitCode = u.UnitCode,
                              UnitFurnitureID = uf != null ? uf.ID : (Guid?)null,
                              CheckStatusID = uf != null && uf.CheckStatusID.HasValue ? uf.CheckStatusID.Value : 0,
                              CheckRemark = uf.CheckRemark,
                              Details = (from d in _context.TR_UnitFurniture_Details
                                         join f in _context.tm_Funitures on d.FurnitureID equals f.ID into fd
                                         from f in fd.DefaultIfEmpty()
                                         where uf != null
                                               && d.UnitFurnitureID == uf.ID
                                               && d.FlagActive == true
                                         select new UnitFurnitureDetailModel
                                         {
                                             ID = Commond.FormatExtension.Nulltoint(d.ID, 0),
                                             UnitFurnitureID = d.UnitFurnitureID,
                                             FurnitureID = Commond.FormatExtension.Nulltoint(d.FurnitureID, 0),
                                             FurnitureName = Commond.FormatExtension.NullToString(f.Name, ""),
                                             Amount = Commond.FormatExtension.Nulltoint(d.Amount, 0),
                                             FlagActive = d.FlagActive ?? false
                                         }).ToList()
                          }).FirstOrDefault();

            return result;
        }


        public async Task<bool> UpdateFurnitureProjectMappingAsync(UpdateFurnitureProjectMappingRequest req,int userId,CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(req.ProjectID))
                throw new ArgumentException("ProjectID is required.", nameof(req.ProjectID));

            if (req.UnitID == Guid.Empty)
                throw new ArgumentException("UnitID is required.", nameof(req.UnitID));

            if (req.Furnitures is null || req.Furnitures.Count == 0)
                throw new ArgumentException("Furnitures is required.", nameof(req.Furnitures));

            // De-dup & validate incoming furnitures
            var incoming = req.Furnitures
                .Select(f =>
                {
                    if (!int.TryParse(f.id, out var fid))
                        throw new ArgumentException($"Invalid FurnitureID: {f.id}");
                    var qty = (int)Math.Max(0, f.qty); // no negative qty
                    return new { FurnitureID = fid, Qty = qty };
                })
                .GroupBy(x => x.FurnitureID)
                .Select(g => new { FurnitureID = g.Key, Qty = g.Sum(x => x.Qty) })
                .ToList();

            await using var tx = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                var now = DateTime.Now; // keep consistent with GETDATE() you use below

                // 1) Upsert TR_UnitFurniture
                var uf = await _context.TR_UnitFurnitures
                    .FirstOrDefaultAsync(x => x.ProjectID == req.ProjectID && x.UnitID == req.UnitID, ct);

                // Respect "approved/locked" (309)
                if (uf is not null && uf.CheckStatusID == 309)
                    throw new InvalidOperationException("This unit furniture is approved and cannot be edited.");

                if (uf is null)
                {
                    uf = new TR_UnitFurniture
                    {
                        ID = Guid.NewGuid(),
                        ProjectID = req.ProjectID,
                        UnitID = req.UnitID,
                        CheckRemark = req.Remark ?? string.Empty,
                        FlagActive = true,
                        CreateDate = now,
                        CreateBy = userId,
                        UpdateDate = now,
                        UpdateBy = userId
                    };
                    _context.TR_UnitFurnitures.Add(uf);
                }
                else
                {
                    // update minimal fields (do NOT override status to approved here)
                    uf.CheckRemark = req.Remark ?? uf.CheckRemark;
                    uf.FlagActive = true;
                    uf.UpdateDate = now;
                    uf.UpdateBy = userId;
                    _context.TR_UnitFurnitures.Update(uf);
                }

                await _context.SaveChangesAsync(ct); // ensure uf.ID is persisted

                // 2) Deactivate all existing details for this UnitFurniture
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    UPDATE TR_UnitFurniture_Detail
                       SET FlagActive = 0,
                           UpdateDate = {now},
                           UpdateBy   = {userId}
                     WHERE UnitFurnitureID = {uf.ID};", ct);

                // 3) Re-apply selected furnitures (reactivate/update or insert)
                var existingDetails = await _context.TR_UnitFurniture_Details
                    .Where(d => d.UnitFurnitureID == uf.ID)
                    .ToListAsync(ct);

                foreach (var item in incoming)
                {
                    var d = existingDetails.FirstOrDefault(x => x.FurnitureID == item.FurnitureID);
                    if (d is null)
                    {
                        d = new TR_UnitFurniture_Detail
                        {
                            UnitFurnitureID = uf.ID,
                            FurnitureID = item.FurnitureID,
                            Amount = item.Qty,
                            FlagActive = true,
                            CreateDate = now,
                            CreateBy = userId,
                            UpdateDate = now,
                            UpdateBy = userId
                        };
                        _context.TR_UnitFurniture_Details.Add(d);
                    }
                    else
                    {
                        d.Amount = item.Qty;
                        d.FlagActive = true;
                        d.UpdateDate = now;
                        d.UpdateBy = userId;
                        _context.TR_UnitFurniture_Details.Update(d);
                    }
                }

                await _context.SaveChangesAsync(ct);

                await tx.CommitAsync(ct);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(ct);
                throw;
            }
        }

    }
}
