using Microsoft.Data.SqlClient;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using System.Data;
using Serilog;
using Log = Serilog.Log;

namespace Project.CSS.Revise.Web.Library.DAL.SQL
{
    public class SqlMasterManagementProject : MasterManagementProviderProject
    {
        public SqlMasterManagementProject(IConfiguration configuration) : base(configuration) { }

        public override List<RollingPlanSummaryModel> sp_GetProjecTargetRollingPlanList_GetListTargetRollingPlan(RollingPlanSummaryModel en)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCommand SqlCmd = new SqlCommand("sp_GetProjecTargetRollingPlanList", SqlCon);
                try
                {
                    SqlCon.Open();
                    SqlTransaction Trans = SqlCon.BeginTransaction();
                    SqlCmd.Transaction = Trans;
                    SqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Act);
                    SqlCmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Year);
                    SqlCmd.Parameters.Add(new SqlParameter("@Quarter", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Quarter);
                    SqlCmd.Parameters.Add(new SqlParameter("@Month", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Month);
                    SqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
                    SqlCmd.Parameters.Add(new SqlParameter("@Bu", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Bu);
                    SqlCmd.Parameters.Add(new SqlParameter("@PlanTypeID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_PlanTypeID);
                    SqlCmd.Parameters.Add(new SqlParameter("@ProjectStatus", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_ProjectStatus);
                    SqlCmd.Parameters.Add(new SqlParameter("@ProjectPartner", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_ProjectPartner);

                    switch (en.L_Act)
                    {
                        case "GetListTargetRollingPlan":
                        {
                            if (en.IS_Export == true )
                            {
                                    return sp_GetProjecTargetRollingPlanList_Getlisttable_ForExport_ListReader(ExecuteReader(SqlCmd));
                            }
                            else
                            {
                                return sp_GetProjecTargetRollingPlanList_Getlisttable_ListReader(ExecuteReader(SqlCmd));
                            }
                        }
                            

                        default:
                            return new List<RollingPlanSummaryModel>();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Stored name : sp_GetProjecTargetRollingPlanList");

                    Log.Error("SEND pram1 Act (nvarchar) : {Act}", en.L_Act);
                    Log.Error("SEND pram2 Year (nvarchar) : " + en.L_Year);
                    Log.Error("SEND pram3 Quarter (nvarchar) : " + en.L_Quarter);
                    Log.Error("SEND pram4 Month (nvarchar) : " + en.L_Month);
                    Log.Error("SEND pram5 ProjectID (nvarchar) : " + en.L_ProjectID);
                    Log.Error("SEND pram6 Bu (nvarchar) : " + en.L_Bu);
                    Log.Error("SEND pram7 PlanTypeID (nvarchar) : " + en.L_PlanTypeID);
                    Log.Error("SEND pram8 PlanTypeName (nvarchar) : " + en.L_PlanTypeName);
                    Log.Error("SEND pram9 ProjectStatus (nvarchar) : " + en.L_ProjectStatus);
                    Log.Error("SEND pram10 ProjectPartner (nvarchar) : " + en.L_ProjectPartner);

                    Log.Error(ex.ToString());
                    Log.Error("=========== END ===========");

                    return new List<RollingPlanSummaryModel>();
                }
                finally
                {
                    SqlCmd.Dispose();
                    SqlCon.Close();
                    SqlCon.Dispose();
                }
            }
        }

        public override List<RollingPlanTotalModel> sp_GetProjecTargetRollingPlanList_GetDataTotalTargetRollingPlan(RollingPlanTotalModel en)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCommand SqlCmd = new SqlCommand("sp_GetProjecTargetRollingPlanList", SqlCon);
                try
                {
                    SqlCon.Open();
                    SqlTransaction Trans = SqlCon.BeginTransaction();
                    SqlCmd.Transaction = Trans;
                    SqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Act);
                    SqlCmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Year);
                    SqlCmd.Parameters.Add(new SqlParameter("@Quarter", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Quarter);
                    SqlCmd.Parameters.Add(new SqlParameter("@Month", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Month);
                    SqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
                    SqlCmd.Parameters.Add(new SqlParameter("@Bu", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_Bu);
                    SqlCmd.Parameters.Add(new SqlParameter("@PlanTypeID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_PlanTypeID);
                    SqlCmd.Parameters.Add(new SqlParameter("@ProjectStatus", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_ProjectStatus);
                    SqlCmd.Parameters.Add(new SqlParameter("@ProjectPartner", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.L_ProjectPartner);

                    switch (en.L_Act)
                    {
                        case "GetDataTotalTargetRollingPlan":
                            return sp_GetProjecTargetRollingPlanList_GetlistTotal_ListReader(ExecuteReader(SqlCmd));

                        default:
                            return new List<RollingPlanTotalModel>();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Stored name : sp_GetProjecTargetRollingPlanList");

                    Log.Error("SEND pram1 Act (nvarchar) : {Act}", en.L_Act);
                    Log.Error("SEND pram2 Year (nvarchar) : " + en.L_Year);
                    Log.Error("SEND pram3 Quarter (nvarchar) : " + en.L_Quarter);
                    Log.Error("SEND pram4 Month (nvarchar) : " + en.L_Month);
                    Log.Error("SEND pram5 ProjectID (nvarchar) : " + en.L_ProjectID);
                    Log.Error("SEND pram6 Bu (nvarchar) : " + en.L_Bu);
                    Log.Error("SEND pram7 PlanTypeID (nvarchar) : " + en.L_PlanTypeID);
                    Log.Error("SEND pram8 ProjectStatus (nvarchar) : " + en.L_ProjectStatus);
                    Log.Error("SEND pram9 ProjectPartner (nvarchar) : " + en.L_ProjectPartner);

                    Log.Error(ex.ToString());
                    Log.Error("=========== END ===========");

                    return new List<RollingPlanTotalModel>();
                }
                finally
                {
                    SqlCmd.Dispose();
                    SqlCon.Close();
                    SqlCon.Dispose();
                }
            }
        }
    }
}
