using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IProjectAndTargetRollingRepo
    {
        public List<RollingPlanSummaryModel> GetListTargetRollingPlan(RollingPlanSummaryModel filter);
        public List<RollingPlanTotalModel> GetDataTotalTargetRollingPlan(RollingPlanTotalModel filter);
    }
    public class ProjectAndTargetRollingRepo : IProjectAndTargetRollingRepo
    {
        private readonly CSSContext _context;

        public ProjectAndTargetRollingRepo(CSSContext context)
        {
            _context = context;
        }


        public List<RollingPlanSummaryModel> GetListTargetRollingPlan(RollingPlanSummaryModel filter)
        {
            List<RollingPlanSummaryModel> result = new List<RollingPlanSummaryModel>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                        -- ===== TEST CASE =====
                        --DECLARE @L_Year NVARCHAR(50) = '2024,2025';
                        --DECLARE @L_Quarter NVARCHAR(100) = '';
                        --DECLARE @L_Month NVARCHAR(100) = '';
                        --DECLARE @L_ProjectID NVARCHAR(100) = '102C028';
                        --DECLARE @L_Bu NVARCHAR(100) = '';
                        --DECLARE @L_PlanTypeID NVARCHAR(100) = '181,182,395,396';
                        -- ===== TEST CASE =====

                            SELECT
                                 T1.ProjectID
                                ,P.ProjectName
                                ,T1.PlanTypeID
                                ,PT.[Name] AS PlanTypeName
                                ,YEAR(T1.MonthlyDate) AS PlanYear
                                -- ===== JAN to DEC =====
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 1 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Jan_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 1 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Jan_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 2 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Feb_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 2 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Feb_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 3 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Mar_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 3 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Mar_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 4 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Apr_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 4 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Apr_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 5 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS May_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 5 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS May_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 6 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Jun_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 6 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Jun_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 7 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Jul_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 7 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Jul_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 8 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Aug_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 8 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Aug_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 9 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Sep_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 9 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Sep_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 10 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Oct_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 10 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Oct_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 11 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Nov_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 11 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Nov_Value
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 12 AND T1.PlanAmountID = 183 THEN T1.Amount END) AS Dec_Unit
                                ,MAX(CASE WHEN MONTH(T1.MonthlyDate) = 12 AND T1.PlanAmountID = 184 THEN T1.Amount END) AS Dec_Value
                                -- ===== TOTALS =====
                                ,SUM(CASE WHEN T1.PlanAmountID = 183 THEN T1.Amount ELSE 0 END) AS Total_Unit
                                ,SUM(CASE WHEN T1.PlanAmountID = 184 THEN T1.Amount ELSE 0 END) AS Total_Value
                            FROM TR_TargetRollingPlan T1 WITH (NOLOCK)
                            LEFT JOIN tm_Project P WITH (NOLOCK) ON T1.ProjectID = P.ProjectID
                            LEFT JOIN tm_Ext PT WITH (NOLOCK) ON T1.PlanTypeID = PT.ID
                            LEFT JOIN tm_BUProject_Mapping BPM WITH (NOLOCK) ON T1.ProjectID = BPM.ProjectID
                            WHERE T1.FlagActive = 1
                              AND T1.PlanAmountID IN (183, 184)
                              -- ===== YEAR FILTER =====
                              AND (
                                    @L_Year = ''
                                    OR (',' + @L_Year + ',' LIKE '%,' + CAST(YEAR(T1.MonthlyDate) AS NVARCHAR) + ',%')
                                  )
                              -- ===== PLAN TYPE FILTER =====
                              AND (
                                    @L_PlanTypeID = ''
                                    OR (',' + @L_PlanTypeID + ',' LIKE '%,' + CONVERT(VARCHAR, T1.PlanTypeID) + ',%')
                                  )
                              -- ===== BU FILTER =====
                              AND (
                                    @L_Bu = ''
                                    OR (',' + @L_Bu + ',' LIKE '%,' + CONVERT(VARCHAR, BPM.BUID) + ',%')
                                  )
                              -- ===== PROJECT FILTER =====
                              AND (
                                    @L_ProjectID = ''
                                    OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
                                  )
                              -- ===== QUARTER FILTER =====
                              AND (
                                    @L_Quarter = ''
                                    OR (
                                        (',' + @L_Quarter + ',' LIKE '%,Q1,%' AND MONTH(T1.MonthlyDate) IN (1,2,3)) OR
                                        (',' + @L_Quarter + ',' LIKE '%,Q2,%' AND MONTH(T1.MonthlyDate) IN (4,5,6)) OR
                                        (',' + @L_Quarter + ',' LIKE '%,Q3,%' AND MONTH(T1.MonthlyDate) IN (7,8,9)) OR
                                        (',' + @L_Quarter + ',' LIKE '%,Q4,%' AND MONTH(T1.MonthlyDate) IN (10,11,12))
                                    )
                                  )
                              -- ===== MONTH FILTER =====
                                AND (
                                    @L_Month = ''
                                    OR ',' + @L_Month + ',' LIKE '%,' + CAST(MONTH(T1.MonthlyDate) AS VARCHAR) + ',%'
                                )
                            GROUP BY
                                 T1.ProjectID
                                ,P.ProjectName
                                ,T1.PlanTypeID
                                ,PT.[Name]
                                ,YEAR(T1.MonthlyDate)
                            ORDER BY
                                 T1.ProjectID
                                ,T1.PlanTypeID
                                ,YEAR(T1.MonthlyDate)

                       "
                                    ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_Year", filter.L_Year ?? "2025"));
                    cmd.Parameters.Add(new SqlParameter("@L_Quarter", filter.L_Quarter ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Month", filter.L_Month ?? ""));             
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Bu", filter.L_Bu ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_PlanTypeID", filter.L_PlanTypeID ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new RollingPlanSummaryModel
                            {
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]),
                                PlanTypeID = Commond.FormatExtension.NullToString(reader["PlanTypeID"]),
                                PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]),
                                PlanYear = Commond.FormatExtension.NullToString(reader["PlanYear"]),

                                Jan_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jan_Unit"]),
                                Jan_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Jan_Value"]),
                                Feb_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Feb_Unit"]),
                                Feb_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Feb_Value"]),
                                Mar_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Mar_Unit"]),
                                Mar_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Mar_Value"]),
                                Apr_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Apr_Unit"]),
                                Apr_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Apr_Value"]),
                                May_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["May_Unit"]),
                                May_Value = Commond.FormatExtension.ConvertToShortUnit(reader["May_Value"]),
                                Jun_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jun_Unit"]),
                                Jun_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Jun_Value"]),
                                Jul_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jul_Unit"]),
                                Jul_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Jul_Value"]),
                                Aug_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Aug_Unit"]),
                                Aug_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Aug_Value"]),
                                Sep_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Sep_Unit"]),
                                Sep_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Sep_Value"]),
                                Oct_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Oct_Unit"]),
                                Oct_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Oct_Value"]),
                                Nov_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Nov_Unit"]),
                                Nov_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Nov_Value"]),
                                Dec_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Dec_Unit"]),
                                Dec_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Dec_Value"]),

                                Total_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Total_Unit"]),
                                Total_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Total_Value"]),
                            });
                        }
                    }
                }
            }


            return result;
        }

        public List<RollingPlanTotalModel> GetDataTotalTargetRollingPlan(RollingPlanTotalModel filter)
        {
            List<RollingPlanTotalModel> result = new List<RollingPlanTotalModel>();
            string connectionString = _context.Database.GetDbConnection().ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "";
                sql = @"
                        -- ===== TEST CASE =====
                        --DECLARE @L_Year NVARCHAR(50) = '2024,2025';
                        --DECLARE @L_Quarter NVARCHAR(100) = '';
                        --DECLARE @L_Month NVARCHAR(100) = '';
                        --DECLARE @L_ProjectID NVARCHAR(100) = '102C028';
                        --DECLARE @L_Bu NVARCHAR(100) = '';
                        --DECLARE @L_PlanTypeID NVARCHAR(100) = '181,182,395,396';
                        -- ===== TEST CASE =====

                        SELECT
                              T3.[Name] AS PlanTypeName 
	                         ,T2.[Name] AS PlanAmountName 
	                         ,SUB1.TOTAL
                        FROM (
		                        SELECT T1.[PlanTypeID]
			                          ,T1.[PlanAmountID]
			                          ,SUM(T1.[Amount]) AS TOTAL
		                        FROM [TR_TargetRollingPlan] T1 WITH (NOLOCK)
		                        LEFT JOIN tm_BUProject_Mapping BPM WITH (NOLOCK) ON T1.ProjectID = BPM.ProjectID
		                        WHERE T1.FlagActive = 1
		                          AND T1.PlanAmountID IN (183, 184)
		                          -- ===== YEAR FILTER =====
		                          AND (
				                        @L_Year = ''
				                        OR (',' + @L_Year + ',' LIKE '%,' + CAST(YEAR(T1.MonthlyDate) AS NVARCHAR) + ',%')
			                          )
		                          -- ===== PLAN TYPE FILTER =====
		                          AND (
				                        @L_PlanTypeID = ''
				                        OR (',' + @L_PlanTypeID + ',' LIKE '%,' + CONVERT(VARCHAR, T1.PlanTypeID) + ',%')
			                          )
		                          -- ===== BU FILTER =====
		                          AND (
				                        @L_Bu = ''
				                        OR (',' + @L_Bu + ',' LIKE '%,' + CONVERT(VARCHAR, BPM.BUID) + ',%')
			                          )
		                          -- ===== PROJECT FILTER =====
		                          AND (
				                        @L_ProjectID = ''
				                        OR (',' + @L_ProjectID + ',' LIKE '%,' + T1.ProjectID + ',%')
			                          )
		                          -- ===== QUARTER FILTER =====
		                          AND (
				                        @L_Quarter = ''
				                        OR (
					                        (',' + @L_Quarter + ',' LIKE '%,Q1,%' AND MONTH(T1.MonthlyDate) IN (1,2,3)) OR
					                        (',' + @L_Quarter + ',' LIKE '%,Q2,%' AND MONTH(T1.MonthlyDate) IN (4,5,6)) OR
					                        (',' + @L_Quarter + ',' LIKE '%,Q3,%' AND MONTH(T1.MonthlyDate) IN (7,8,9)) OR
					                        (',' + @L_Quarter + ',' LIKE '%,Q4,%' AND MONTH(T1.MonthlyDate) IN (10,11,12))
				                        )
			                          )
		                          -- ===== MONTH FILTER =====
		                        AND (
			                        @L_Month = ''
			                        OR ',' + @L_Month + ',' LIKE '%,' + CAST(MONTH(T1.MonthlyDate) AS VARCHAR) + ',%'
		                        )
		                        GROUP BY T1.PlanTypeID
				                        ,T1.[PlanAmountID]
	                          ) SUB1 
                        LEFT JOIN [tm_Ext] T2 WITH (NOLOCK) ON SUB1.PlanAmountID = T2.ID
                        LEFT JOIN [tm_Ext] T3 WITH (NOLOCK) ON SUB1.PlanTypeID = T3.ID
                        ORDER BY SUB1.PlanTypeID
                                ,SUB1.PlanAmountID

                       "
                                    ;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@L_Year", filter.L_Year ?? "2025"));
                    cmd.Parameters.Add(new SqlParameter("@L_Quarter", filter.L_Quarter ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Month", filter.L_Month ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectID", filter.L_ProjectID ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_Bu", filter.L_Bu ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_PlanTypeID", filter.L_PlanTypeID ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new RollingPlanTotalModel
                            {
                                PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]),
                                PlanAmountName = Commond.FormatExtension.NullToString(reader["PlanAmountName"]),
                                TOTAL = Commond.FormatExtension.ConvertToShortUnit(reader["TOTAL"]),
                            });
                        }
                    }
                }
            }


            return result;
        }
    }
}
