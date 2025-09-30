using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IFurnitureAndUnitFurnitureRepo
    {
        public List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem> GetlistUnitFurniture(FurnitureAndUnitFurnitureModel.UnitFurnitureFilter filter);
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

    }
}
