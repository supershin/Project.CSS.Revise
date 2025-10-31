using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IProjectAndTargetRollingRepo
    {
        public List<RollingPlanSummaryModel> GetListTargetRollingPlan(RollingPlanSummaryModel filter);
        public List<RollingPlanTotalModel> GetDataTotalTargetRollingPlan(RollingPlanTotalModel filter);
        public TargetRollingPlanInsertModel UpsertTargetRollingPlans(List<TargetRollingPlanInsertModel> plans);
        public List<RollingPlanSummaryModel> GetListDataExportTargetRollingPlan(RollingPlanSummaryModel filter);
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
						--DECLARE @L_ProjectStatus NVARCHAR(100) = '';
						--DECLARE @L_ProjectPartner NVARCHAR(100) = '';
                        -- ===== TEST CASE =====

                            SELECT
                                 T1.ProjectID
                                ,P.ProjectName
								,BU.[Name] AS BUName
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
							LEFT JOIN [tm_BU] BU WITH (NOLOCK) ON BPM.BUID = BU.ID
							LEFT JOIN TR_ProjectStatus PST WITH (NOLOCK) ON P.ProjectID = PST.ProjectID
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
                              -- ===== PROJECT STATUS FILTER =====
                              AND (
                                    @L_ProjectStatus = ''
                                    OR (',' + @L_ProjectStatus + ',' LIKE '%,' + CONVERT(VARCHAR, PST.StatusID) + ',%')
                                  )
                              -- ===== PROJECT PARTNER FILTER =====
							   AND (
									@L_ProjectPartner = ''  -- case 1: no filter, show all
									OR (
										@L_ProjectPartner = '372' AND PartnerID IS NULL -- case 2: 372 means NULL
									)
									OR (
										@L_ProjectPartner <> '' AND @L_ProjectPartner <> '372' AND P.PartnerID = @L_ProjectPartner -- case 3: normal compare
									)
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
								,BU.[Name]
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
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectStatus", filter.L_ProjectStatus ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectPartner", filter.L_ProjectPartner ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new RollingPlanSummaryModel
                            {
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]),
                                BuName = Commond.FormatExtension.NullToString(reader["BUName"]),
                                PlanTypeID = Commond.FormatExtension.NullToString(reader["PlanTypeID"]),
                                PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]),
                                PlanYear = Commond.FormatExtension.NullToString(reader["PlanYear"]),

                                Jan_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jan_Unit"]),
                                Jan_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Jan_Unit"]),
                                Jan_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Jan_Value"]),
                                Jan_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Jan_Value"]),

                                Feb_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Feb_Unit"]),
                                Feb_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Feb_Unit"]),
                                Feb_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Feb_Value"]),
                                Feb_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Feb_Value"]),

                                Mar_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Mar_Unit"]),
                                Mar_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Mar_Unit"]),
                                Mar_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Mar_Value"]),
                                Mar_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Mar_Value"]),

                                Apr_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Apr_Unit"]),
                                Apr_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Apr_Unit"]),
                                Apr_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Apr_Value"]),
                                Apr_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Apr_Value"]),

                                May_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["May_Unit"]),
                                May_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["May_Unit"]),
                                May_Value = Commond.FormatExtension.ConvertToShortUnit(reader["May_Value"]),
                                May_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["May_Value"]),

                                Jun_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jun_Unit"]),
                                Jun_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Jun_Unit"]),
                                Jun_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Jun_Value"]),
                                Jun_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Jun_Value"]),

                                Jul_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jul_Unit"]),
                                Jul_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Jul_Unit"]),
                                Jul_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Jul_Value"]),
                                Jul_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Jul_Value"]),

                                Aug_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Aug_Unit"]),
                                Aug_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Aug_Unit"]),
                                Aug_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Aug_Value"]),
                                Aug_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Aug_Value"]),

                                Sep_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Sep_Unit"]),
                                Sep_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Sep_Unit"]),
                                Sep_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Sep_Value"]),
                                Sep_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Sep_Value"]),

                                Oct_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Oct_Unit"]),
                                Oct_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Oct_Unit"]),
                                Oct_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Oct_Value"]),
                                Oct_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Oct_Value"]),

                                Nov_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Nov_Unit"]),
                                Nov_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Nov_Unit"]),
                                Nov_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Nov_Value"]),
                                Nov_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Nov_Value"]),

                                Dec_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Dec_Unit"]),
                                Dec_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Dec_Unit"]),
                                Dec_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Dec_Value"]),
                                Dec_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Dec_Value"]),

                                Total_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Total_Unit"]),
                                Total_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Total_Unit"]),
                                Total_Value = Commond.FormatExtension.ConvertToShortUnit(reader["Total_Value"]),
                                Total_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Total_Value"]),
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
						--DECLARE @L_ProjectStatus NVARCHAR(100) = '';
						--DECLARE @L_ProjectPartner NVARCHAR(100) = '';
                        -- ===== TEST CASE =====

                        SELECT
                              T3.[Name] AS PlanTypeName 
	                         ,T2.[Name] AS PlanAmountName 
	                         ,SUB1.TOTAL
                             ,CASE 
                                WHEN T3.[Name] LIKE '%Target%' THEN '#0d6efd'            -- Bootstrap primary
                                WHEN T3.[Name] LIKE '%Rolling%' THEN '#dc3545'           -- Bootstrap danger
                                WHEN T3.[Name] LIKE '%Actual%' THEN '#ffc107'            -- Bootstrap warning
                                WHEN T3.[Name] LIKE '%Working Target%' THEN '#0d6efd'
                                WHEN T3.[Name] LIKE '%Working Rolling%' THEN '#dc3545'
                                WHEN T3.[Name] LIKE '%MLL%' THEN '#0d6efd'
                                ELSE '#6c757d'                                          -- default = text-secondary
                              END AS COLORS
                        FROM (
		                        SELECT T1.[PlanTypeID]
			                          ,T1.[PlanAmountID]
			                          ,SUM(T1.[Amount]) AS TOTAL
		                        FROM [TR_TargetRollingPlan] T1 WITH (NOLOCK)
                                LEFT JOIN tm_Project P WITH (NOLOCK) ON T1.ProjectID = P.ProjectID
		                        LEFT JOIN tm_BUProject_Mapping BPM WITH (NOLOCK) ON T1.ProjectID = BPM.ProjectID
							    LEFT JOIN [tm_BU] BU WITH (NOLOCK) ON BPM.BUID = BU.ID
							    LEFT JOIN TR_ProjectStatus PST WITH (NOLOCK) ON P.ProjectID = PST.ProjectID
		                        WHERE T1.FlagActive = 1
		                          AND T1.PlanAmountID IN (183, 184)
		                          -- ===== YEAR FILTER =====
		                          AND (
				                        @L_Year = ''
				                        OR (',' + @L_Year + ',' LIKE '%,' + CAST(YEAR(T1.MonthlyDate) AS NVARCHAR) + ',%')
			                          )
                                  -- ===== PROJECT STATUS FILTER =====
                                  AND (
                                        @L_ProjectStatus = ''
                                        OR (',' + @L_ProjectStatus + ',' LIKE '%,' + CONVERT(VARCHAR, PST.StatusID) + ',%')
                                      )
                                  -- ===== PROJECT PARTNER FILTER =====
							       AND (
									    @L_ProjectPartner = ''  -- case 1: no filter, show all
									    OR (
										    @L_ProjectPartner = '372' AND PartnerID IS NULL -- case 2: 372 means NULL
									    )
									    OR (
										    @L_ProjectPartner <> '' AND @L_ProjectPartner <> '372' AND P.PartnerID = @L_ProjectPartner -- case 3: normal compare
									    )
								    )
		        -- ===== PLAN TYPE FILTER (Not use now ^_^)=====
		        -- AND (
				-- @L_PlanTypeID = ''
				-- OR (',' + @L_PlanTypeID + ',' LIKE '%,' + CONVERT(VARCHAR, T1.PlanTypeID) + ',%')
			    -- )
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
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectStatus", filter.L_ProjectStatus ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectPartner", filter.L_ProjectPartner ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new RollingPlanTotalModel
                            {
                                PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]),
                                PlanAmountName = Commond.FormatExtension.NullToString(reader["PlanAmountName"]),
                                TOTAL = Commond.FormatExtension.ConvertToShortNameUnit(reader["TOTAL"]),
                                COLORS = Commond.FormatExtension.NullToString(reader["COLORS"]),
                            });
                        }
                    }
                }
            }


            return result;
        }

        public TargetRollingPlanInsertModel UpsertTargetRollingPlans_Old_Version_Not_use_now(List<TargetRollingPlanInsertModel> plans)
        {
            var result = new TargetRollingPlanInsertModel();

            if (plans == null || plans.Count == 0)
            {
                result.Message = "No data to import.";
                return result;
            }

            using (var tx = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var p in plans)
                    {
                        // guard bad rows
                        if (string.IsNullOrWhiteSpace(p.ProjectID) || p.PlanTypeID <= 0 || p.MonthlyDate == default)
                        {
                            result.Skipped++;
                            continue;
                        }

                        var projectId = p.ProjectID.Trim();
                        if (projectId.Length > 50) projectId = projectId.Substring(0, 50); // adjust to your column length

                        var monthDate = p.MonthlyDate.Date;
                        var amount = Math.Round(p.Amount, 2); // if column is DECIMAL(18,2)

                        // 🔎 WHERE: find existing row by key
                        var row = _context.TR_TargetRollingPlans.FirstOrDefault(x =>
                            x.ProjectID == projectId &&
                            x.PlanTypeID == p.PlanTypeID &&
                            x.PlanAmountID == p.PlanAmountID &&
                            x.MonthlyDate == monthDate);

                        if (row != null)
                        {
                            // UPDATE
                            row.Amount = amount;
                            row.FlagActive = p.FlagActive;
                            row.UpdateBy = p.UpdateBy;
                            row.UpdateDate = DateTime.Now;
                            result.Updated++;
                        }
                        else
                        {
                            // INSERT
                            var newRow = new TR_TargetRollingPlan
                            {
                                ProjectID = projectId,
                                PlanTypeID = p.PlanTypeID,
                                PlanAmountID = p.PlanAmountID,
                                MonthlyDate = monthDate,
                                Amount = amount,
                                FlagActive = p.FlagActive,
                                CreateBy = p.CreateBy,
                                CreateDate = DateTime.Now,
                                UpdateBy = p.UpdateBy,
                                UpdateDate = DateTime.Now
                            };

                            _context.TR_TargetRollingPlans.Add(newRow);
                            result.Inserted++;
                        }
                    }

                    _context.SaveChanges();
                    tx.Commit();

                    result.Message = $"Upsert OK. Inserted: {result.Inserted}, Updated: {result.Updated}, Skipped: {result.Skipped}";
                }
                catch (DbUpdateException ex)
                {
                    tx.Rollback();

                    var root = ex.InnerException?.Message
                               ?? ex.GetBaseException().Message
                               ?? ex.Message;

                    result.Message = $"Upsert failed: {root}";
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    result.Message = $"Upsert failed: {ex.Message}";
                }
            }

            return result;
        }

        public TargetRollingPlanInsertModel UpsertTargetRollingPlans(List<TargetRollingPlanInsertModel> plans)
        {
            var result = new TargetRollingPlanInsertModel();

            if (plans == null || plans.Count == 0)
            {
                result.Message = "No data to import.";
                return result;
            }

            using (var tx = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var p in plans)
                    {
                        // ======= 1. ตรวจสอบเบื้องต้น =======
                        if (string.IsNullOrWhiteSpace(p.ProjectID)
                            || p.PlanTypeID <= 0
                            || (p.PlanAmountID != 183 && p.PlanAmountID != 184)
                            || p.MonthlyDate == default)
                        {
                            result.Skipped++;
                            continue;
                        }

                        var projectId = p.ProjectID.Trim();
                        if (projectId.Length > 50) projectId = projectId.Substring(0, 50);

                        // ======= 2. ตรวจสอบ BUID =======
                        int? buid = _context.tm_BUProject_Mappings
                            .AsNoTracking()
                            .Where(m => m.ProjectID == projectId)
                            .Select(m => (int?)m.BUID)
                            .FirstOrDefault();

                        // ======= 3. ถ้าเป็น Actual (185) ต้อง BU=6 เท่านั้น =======
                        if (p.PlanTypeID == 185)
                        {
                            if (buid == null || buid != 6)
                            {
                                result.Skipped++;
                                continue;
                            }
                        }

                        var monthDate = p.MonthlyDate.Date;
                        var amount = Math.Round(p.Amount, 2);
                        var flagActive = p.FlagActive;

                        // ======= 4. ตรวจว่ามีข้อมูลเดิมหรือไม่ =======
                        var row = _context.TR_TargetRollingPlans.FirstOrDefault(x =>
                            x.ProjectID == projectId &&
                            x.PlanTypeID == p.PlanTypeID &&
                            x.PlanAmountID == p.PlanAmountID &&
                            x.MonthlyDate == monthDate);

                        if (row != null)
                        {
                            // UPDATE
                            row.Amount = amount;
                            row.FlagActive = flagActive;
                            row.UpdateBy = p.UpdateBy;
                            row.UpdateDate = DateTime.Now;
                            result.Updated++;
                        }
                        else
                        {
                            // INSERT
                            var newRow = new TR_TargetRollingPlan
                            {
                                ProjectID = projectId,
                                PlanTypeID = p.PlanTypeID,
                                PlanAmountID = p.PlanAmountID,
                                MonthlyDate = monthDate,
                                Amount = amount,
                                FlagActive = flagActive,
                                CreateBy = p.CreateBy,
                                CreateDate = DateTime.Now,
                                UpdateBy = p.UpdateBy,
                                UpdateDate = DateTime.Now
                            };

                            _context.TR_TargetRollingPlans.Add(newRow);
                            result.Inserted++;
                        }
                    }

                    _context.SaveChanges();
                    tx.Commit();
                    result.Message = $"Upsert OK. Inserted: {result.Inserted}, Updated: {result.Updated}, Skipped: {result.Skipped}";
                }
                catch (DbUpdateException ex)
                {
                    tx.Rollback();
                    var root = ex.InnerException?.Message
                               ?? ex.GetBaseException().Message
                               ?? ex.Message;
                    result.Message = $"Upsert failed: {root}";
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    result.Message = $"Upsert failed: {ex.Message}";
                }
            }

            return result;
        }

        public List<RollingPlanSummaryModel> GetListDataExportTargetRollingPlan(RollingPlanSummaryModel filter)
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
						--DECLARE @L_ProjectStatus NVARCHAR(100) = '';
						--DECLARE @L_ProjectPartner NVARCHAR(100) = '';
                        -- ===== TEST CASE =====

                            SELECT
                                 T1.ProjectID
                                ,P.ProjectName
								,BU.[Name] AS BUName
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
							LEFT JOIN [tm_BU] BU WITH (NOLOCK) ON BPM.BUID = BU.ID
							LEFT JOIN TR_ProjectStatus PST WITH (NOLOCK) ON P.ProjectID = PST.ProjectID
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
                              -- ===== PROJECT STATUS FILTER =====
                              AND (
                                    @L_ProjectStatus = ''
                                    OR (',' + @L_ProjectStatus + ',' LIKE '%,' + CONVERT(VARCHAR, PST.StatusID) + ',%')
                                  )
                              -- ===== PROJECT PARTNER FILTER =====
							   AND (
									@L_ProjectPartner = ''  -- case 1: no filter, show all
									OR (
										@L_ProjectPartner = '372' AND PartnerID IS NULL -- case 2: 372 means NULL
									)
									OR (
										@L_ProjectPartner <> '' AND @L_ProjectPartner <> '372' AND P.PartnerID = @L_ProjectPartner -- case 3: normal compare
									)
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
								,BU.[Name]
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
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectStatus", filter.L_ProjectStatus ?? ""));
                    cmd.Parameters.Add(new SqlParameter("@L_ProjectPartner", filter.L_ProjectPartner ?? ""));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new RollingPlanSummaryModel
                            {
                                ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]),
                                ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]),
                                BuName = Commond.FormatExtension.NullToString(reader["BUName"]),
                                PlanTypeID = Commond.FormatExtension.NullToString(reader["PlanTypeID"]),
                                PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]),
                                PlanYear = Commond.FormatExtension.NullToString(reader["PlanYear"]),

                                Jan_Unit = Commond.FormatExtension.NullToString(reader["Jan_Unit"]),
                                Jan_Value = Commond.FormatExtension.NullToString(reader["Jan_Value"]),
                                Feb_Unit = Commond.FormatExtension.NullToString(reader["Feb_Unit"]),
                                Feb_Value = Commond.FormatExtension.NullToString(reader["Feb_Value"]),
                                Mar_Unit = Commond.FormatExtension.NullToString(reader["Mar_Unit"]),
                                Mar_Value = Commond.FormatExtension.NullToString(reader["Mar_Value"]),
                                Apr_Unit = Commond.FormatExtension.NullToString(reader["Apr_Unit"]),
                                Apr_Value = Commond.FormatExtension.NullToString(reader["Apr_Value"]),
                                May_Unit = Commond.FormatExtension.NullToString(reader["May_Unit"]),
                                May_Value = Commond.FormatExtension.NullToString(reader["May_Value"]),
                                Jun_Unit = Commond.FormatExtension.NullToString(reader["Jun_Unit"]),
                                Jun_Value = Commond.FormatExtension.NullToString(reader["Jun_Value"]),
                                Jul_Unit = Commond.FormatExtension.NullToString(reader["Jul_Unit"]),
                                Jul_Value = Commond.FormatExtension.NullToString(reader["Jul_Value"]),
                                Aug_Unit = Commond.FormatExtension.NullToString(reader["Aug_Unit"]),
                                Aug_Value = Commond.FormatExtension.NullToString(reader["Aug_Value"]),
                                Sep_Unit = Commond.FormatExtension.NullToString(reader["Sep_Unit"]),
                                Sep_Value = Commond.FormatExtension.NullToString(reader["Sep_Value"]),
                                Oct_Unit = Commond.FormatExtension.NullToString(reader["Oct_Unit"]),
                                Oct_Value = Commond.FormatExtension.NullToString(reader["Oct_Value"]),
                                Nov_Unit = Commond.FormatExtension.NullToString(reader["Nov_Unit"]),
                                Nov_Value = Commond.FormatExtension.NullToString(reader["Nov_Value"]),
                                Dec_Unit = Commond.FormatExtension.NullToString(reader["Dec_Unit"]),
                                Dec_Value = Commond.FormatExtension.NullToString(reader["Dec_Value"]),

                                Total_Unit = Commond.FormatExtension.NullToString(reader["Total_Unit"]),
                                Total_Value = Commond.FormatExtension.NullToString(reader["Total_Value"]),
                            });
                        }
                    }
                }
            }


            return result;
        }

    }
}
