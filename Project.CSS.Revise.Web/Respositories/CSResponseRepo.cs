using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface ICSResponseRepo
    {
        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter);
    }
    public class CSResponseRepo : ICSResponseRepo
    {
        private readonly CSSContext _context;

        public CSResponseRepo(CSSContext context)
        {
            _context = context;
        }

        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter)
        {
            List<GetlistUnitCSResponseModel.ListData> result = new List<GetlistUnitCSResponseModel.ListData>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                                -- ===== TEST CASE =====
	                            --DECLARE @L_UserID NVARCHAR(100) = '';
                                --DECLARE @L_ProjectID NVARCHAR(100) = '102C028';
	                            --DECLARE @L_Build NVARCHAR(100) = 'A,B';
	                            --DECLARE @L_Floor NVARCHAR(100) = '';
	                            --DECLARE @L_Room NVARCHAR(100) = '10';
                                --DECLARE @L_TypeUserShow   NVARCHAR(10)  = '-1';  -- '1' = เฉพาะของฉัน, '-1' = ทั้งหมด, '0' = ของคนอื่น
                                -- ===== TEST CASE =====


                            SELECT P.[ProjectID]
                                    ,P.[ProjectName]
                                    ,P.[ProjectName_Eng]
                                    ,P.[ProjectType]
	                                ,U.[UnitCode]
                                    ,U.[AddrNo]
                                    ,U.[Build]
                                    ,U.[Floor]
                                    ,U.[Room]
                                    ,U.[UnitType]
                                    ,U.[Area]
                                    ,U.[UnitStatus]
	                                ,UUM.[UserID]
                                    ,US.[FirstName]
                                    ,US.[FirstName_Eng]
                                    ,US.[TitleID_Eng]
                                    ,US.[LastName]
                                    ,US.[LastName_Eng]
	                                ,CASE WHEN CONVERT(NVARCHAR(100), UUM.[UserID]) = @L_UserID THEN 1 ELSE 0 END AS IsCheck
                                FROM [tm_Project] P WITH (NOLOCK) 
                                LEFT JOIN [tm_Unit] U WITH (NOLOCK) ON U.ProjectID = P.ProjectID
                                LEFT JOIN [TR_UnitUser_Mapping] UUM WITH (NOLOCK) 
                                        ON P.ProjectID = UUM.ProjectID AND UUM.UnitCode = U.[UnitCode]
                                LEFT JOIN [tm_User] US WITH (NOLOCK) ON UUM.UserID = US.[ID]
                            WHERE U.[FlagActive] = 1
                                AND P.[FlagActive] = 1
                                AND P.[ProjectID] =  @L_ProjectID
                                AND (
		                            @L_Build = N''
		                            OR (N',' + @L_Build + N',' LIKE N'%,' + CONVERT(NVARCHAR(100), U.[Build]) + N',%')
	                                )
                                AND (
		                            @L_Floor = N''
		                            OR (N',' + @L_Floor + N',' LIKE N'%,' + CONVERT(NVARCHAR(100), U.[Floor]) + N',%')
	                                )
                                AND (
		                            @L_Room = N''
		                            OR (N',' + @L_Room + N',' LIKE N'%,' + CONVERT(NVARCHAR(100), U.[Room]) + N',%')
	                                )
                                AND (
                                        @L_TypeUserShow = '-1'
                                    OR (@L_TypeUserShow =  '1' AND CONVERT(NVARCHAR(100), UUM.[UserID]) =  @L_UserID)  -- เฉพาะของฉัน
                                    OR (@L_TypeUserShow =  '0' AND (UUM.[UserID] IS NULL OR CONVERT(NVARCHAR(100), UUM.[UserID]) <> @L_UserID))
                                    )
                                ORDER BY P.[ProjectID]
                                        ,U.[UnitCode]


                           "
                                    ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_UserID", filter.L_UserID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Build", filter.L_Build ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Floor", filter.L_Floor ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Room", filter.L_Room ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_TypeUserShow", filter.L_TypeUserShow ?? "-1"));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new GetlistUnitCSResponseModel.ListData
                            {
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]),
                                ProjectName_Eng = Commond.FormatExtension.NullToString(reader["ProjectName_Eng"]),
                                ProjectType = Commond.FormatExtension.NullToString(reader["ProjectType"]),

                                UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]),
                                AddrNo = Commond.FormatExtension.NullToString(reader["AddrNo"]),
                                Build = Commond.FormatExtension.NullToString(reader["Build"]),
                                Floor = Commond.FormatExtension.NullToString(reader["Floor"]),
                                Room = Commond.FormatExtension.NullToString(reader["Room"]),
                                UnitType = Commond.FormatExtension.NullToString(reader["UnitType"]),
                                Area = Commond.FormatExtension.NullToString(reader["Area"]),
                                UnitStatus = Commond.FormatExtension.NullToString(reader["UnitStatus"]),

                                UserID = Commond.FormatExtension.NullToString(reader["UserID"]),
                                FirstName = Commond.FormatExtension.NullToString(reader["FirstName"]),
                                FirstName_Eng = Commond.FormatExtension.NullToString(reader["FirstName_Eng"]),
                                TitleID_Eng = Commond.FormatExtension.NullToString(reader["TitleID_Eng"]),
                                LastName = Commond.FormatExtension.NullToString(reader["LastName"]),
                                LastName_Eng = Commond.FormatExtension.NullToString(reader["LastName_Eng"]),
                                IsCheck = Commond.FormatExtension.Nulltoint(reader["IsCheck"])
                            });
                        }
                    }
                }
            }


            return result;
        }

    }
}
