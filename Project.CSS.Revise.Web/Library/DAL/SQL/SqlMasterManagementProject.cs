using Microsoft.Data.SqlClient;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using Serilog;
using System.Data;
using System.Data.SqlTypes;
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
                                if (en.IS_Export == true)
                                {
                                    return sp_GetProjecTargetRollingPlanList_Getlisttable_ForExport_ListReader(ExecuteReader(SqlCmd));
                                }
                                else
                                {
                                    return sp_GetProjecTargetRollingPlanList_Getlisttable_ListReader(ExecuteReader(SqlCmd));
                                }
                            }

                        case "GetListTargetRollingPlanCuttoltal":
                            {
                                if (en.IS_Export == true)
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

        public override SPGetDataCSResponse.ListData sp_GetDataCSResponse(SPGetDataCSResponse.FilterData en)
        {
            var result = new SPGetDataCSResponse.ListData();
            using var SqlCon = new SqlConnection(ConnectionString);
            using var SqlCmd = new SqlCommand("sp_GetDataCSResponse", SqlCon) { CommandType = CommandType.StoredProcedure };

            SqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.Act);
            SqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.ProjectID);
            SqlCmd.Parameters.Add(new SqlParameter("@BUID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.BUID);
            SqlCmd.Parameters.Add(new SqlParameter("@CsName", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(en.CsName);
            SqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = Commond.FormatExtension.Nulltoint(en.UserID);

            try
            {
                SqlCon.Open();
                using var reader = ExecuteReader(SqlCmd);
                switch (en.Act)
                {
                    case "GetListCSSummary":
                        result.CSSummary = sp_GetDataCSResponse_GetListCSSummary_ListReader(reader);
                        break;
                    case "GetListCountUnitStatus":
                        result.CountUnitStatus = sp_GetDataCSResponse_GetListCountUnitStatus_ListReader(reader);
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetDataCSResponse");
                Log.Error("SEND Act: {Act}", en.Act);
                Log.Error("SEND ProjectID: {ProjectID}", en.ProjectID);
                Log.Error("SEND BUID: {BUID}", en.BUID);
                Log.Error("SEND CsName: {CsName}", en.CsName);
                Log.Error("SEND UserID: {UserID}", en.UserID);
                Log.Error(ex, "Error executing sp_GetDataCSResponse");
                return new SPGetDataCSResponse.ListData();
            }
        }

        public override List<ListDataRegisterTable> sp_GetQueueBank_RegisterTable(GetQueueBankModel en)
        {
            var result = new List<ListDataRegisterTable>();

            using var sqlCon = new SqlConnection(ConnectionString);
            using var sqlCmd = new SqlCommand("sp_GetQueueBank", sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar, 50)).Value = Commond.FormatExtension.NullToString(en.L_Act);
            sqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateStart", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateStart);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateEnd", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateEnd);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar, -1)).Value = Commond.FormatExtension.NullToString(en.L_UnitID);
            sqlCmd.Parameters.Add(new SqlParameter("@CSResponse", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_CSResponse);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitCS", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_UnitCS);
            sqlCmd.Parameters.Add(new SqlParameter("@ExpectTransfer", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ExpectTransfer);
            sqlCmd.Parameters.Add(new SqlParameter("@QueueTypeID", SqlDbType.Int)).Value = en.L_QueueTypeID;
            // ===== paging params =====
            sqlCmd.Parameters.Add(new SqlParameter("@Start", SqlDbType.Int)).Value = en.start;
            sqlCmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int)).Value = en.length;
            sqlCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 200)).Value = Commond.FormatExtension.NullToString(en.SearchTerm);
            try
            {
                sqlCon.Open();
                using var reader = ExecuteReader(sqlCmd);

                switch (en.L_Act)
                {
                    case "RegisterTable":
                        result = sp_GetQueueBank_ListDataRegisterTable(reader);
                        break;

                    default:
                        result = new List<ListDataRegisterTable>();
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetQueueBank");
                Log.Error("SEND Act: {Act}", en.L_Act);
                Log.Error("SEND ProjectID: {ProjectID}", en.L_ProjectID);
                Log.Error("SEND RegisterDate: {RegisterDateStart}", en.L_RegisterDateStart);
                Log.Error("SEND RegisterDate: {RegisterDateEnd}", en.L_RegisterDateEnd);
                Log.Error("SEND UnitID: {UnitID}", en.L_UnitID);
                Log.Error("SEND CSResponse: {CSResponse}", en.L_CSResponse);
                Log.Error("SEND UnitCS: {UnitCS}", en.L_UnitCS);
                Log.Error("SEND ExpectTransfer: {ExpectTransfer}", en.L_ExpectTransfer);
                Log.Error(ex, "Error executing sp_GetQueueBank");

                return new List<ListDataRegisterTable>();
            }
        }

        public override List<ListSummeryRegister.ListSummeryRegisterType> sp_GetQueueBank_SummeryRegisterType(GetQueueBankModel en)
        {
            var result = new List<ListSummeryRegister.ListSummeryRegisterType>();

            using var sqlCon = new SqlConnection(ConnectionString);
            using var sqlCmd = new SqlCommand("sp_GetQueueBank", sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar, 50)).Value = Commond.FormatExtension.NullToString(en.L_Act);
            sqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateStart", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateStart);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateEnd", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateEnd);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar, -1)).Value = Commond.FormatExtension.NullToString(en.L_UnitID);
            sqlCmd.Parameters.Add(new SqlParameter("@CSResponse", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_CSResponse);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitCS", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_UnitCS);
            sqlCmd.Parameters.Add(new SqlParameter("@ExpectTransfer", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ExpectTransfer);
            sqlCmd.Parameters.Add(new SqlParameter("@QueueTypeID", SqlDbType.Int)).Value = en.L_QueueTypeID;
            // ===== paging params =====
            sqlCmd.Parameters.Add(new SqlParameter("@Start", SqlDbType.Int)).Value = en.start;
            sqlCmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int)).Value = en.length;
            sqlCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 200)).Value = Commond.FormatExtension.NullToString(en.SearchTerm);
            try
            {
                sqlCon.Open();
                using var reader = ExecuteReader(sqlCmd);

                switch (en.L_Act)
                {
                    case "SummeryRegisterType":
                        result = sp_GetQueueBank_LisSummeryRegisterType(reader);
                        break;

                    default:
                        result = new List<ListSummeryRegister.ListSummeryRegisterType>();
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetQueueBank");
                Log.Error("SEND Act: {Act}", en.L_Act);
                Log.Error("SEND ProjectID: {ProjectID}", en.L_ProjectID);
                Log.Error("SEND RegisterDate: {RegisterDateStart}", en.L_RegisterDateStart);
                Log.Error("SEND RegisterDate: {RegisterDateEnd}", en.L_RegisterDateEnd);
                Log.Error("SEND UnitID: {UnitID}", en.L_UnitID);
                Log.Error("SEND CSResponse: {CSResponse}", en.L_CSResponse);
                Log.Error("SEND UnitCS: {UnitCS}", en.L_UnitCS);
                Log.Error("SEND ExpectTransfer: {ExpectTransfer}", en.L_ExpectTransfer);
                Log.Error(ex, "Error executing sp_GetQueueBank");

                return new List<ListSummeryRegister.ListSummeryRegisterType>();
            }
        }

        public override List<ListSummeryRegister.ListSummeryRegisterLoanType> sp_GetQueueBank_SummeryRegisterLoanType(GetQueueBankModel en)
        {
            var result = new List<ListSummeryRegister.ListSummeryRegisterLoanType>();

            using var sqlCon = new SqlConnection(ConnectionString);
            using var sqlCmd = new SqlCommand("sp_GetQueueBank", sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar, 50)).Value = Commond.FormatExtension.NullToString(en.L_Act);
            sqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateStart", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateStart);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateEnd", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateEnd);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar, -1)).Value = Commond.FormatExtension.NullToString(en.L_UnitID);
            sqlCmd.Parameters.Add(new SqlParameter("@CSResponse", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_CSResponse);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitCS", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_UnitCS);
            sqlCmd.Parameters.Add(new SqlParameter("@ExpectTransfer", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ExpectTransfer);
            sqlCmd.Parameters.Add(new SqlParameter("@QueueTypeID", SqlDbType.Int)).Value = en.L_QueueTypeID;
            // ===== paging params =====
            sqlCmd.Parameters.Add(new SqlParameter("@Start", SqlDbType.Int)).Value = en.start;
            sqlCmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int)).Value = en.length;
            sqlCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 200)).Value = Commond.FormatExtension.NullToString(en.SearchTerm);
            try
            {
                sqlCon.Open();
                using var reader = ExecuteReader(sqlCmd);

                switch (en.L_Act)
                {
                    case "SummeryRegisterLoanType":
                        result = sp_GetQueueBank_ListSummeryRegisterLoanType(reader);
                        break;

                    default:
                        result = new List<ListSummeryRegister.ListSummeryRegisterLoanType>();
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetQueueBank");
                Log.Error("SEND Act: {Act}", en.L_Act);
                Log.Error("SEND ProjectID: {ProjectID}", en.L_ProjectID);
                Log.Error("SEND RegisterDate: {RegisterDateStart}", en.L_RegisterDateStart);
                Log.Error("SEND RegisterDate: {RegisterDateEnd}", en.L_RegisterDateEnd);
                Log.Error("SEND UnitID: {UnitID}", en.L_UnitID);
                Log.Error("SEND CSResponse: {CSResponse}", en.L_CSResponse);
                Log.Error("SEND UnitCS: {UnitCS}", en.L_UnitCS);
                Log.Error("SEND ExpectTransfer: {ExpectTransfer}", en.L_ExpectTransfer);
                Log.Error(ex, "Error executing sp_GetQueueBank");

                return new List<ListSummeryRegister.ListSummeryRegisterLoanType>();
            }
        }

        public override List<ListSummeryRegister.ListSummeryRegisterCareerType> sp_GetQueueBank_SummeryRegisterCareerType(GetQueueBankModel en)
        {
            var result = new List<ListSummeryRegister.ListSummeryRegisterCareerType>();

            using var sqlCon = new SqlConnection(ConnectionString);
            using var sqlCmd = new SqlCommand("sp_GetQueueBank", sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar, 50)).Value = Commond.FormatExtension.NullToString(en.L_Act);
            sqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateStart", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateStart);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateEnd", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateEnd);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar, -1)).Value = Commond.FormatExtension.NullToString(en.L_UnitID);
            sqlCmd.Parameters.Add(new SqlParameter("@CSResponse", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_CSResponse);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitCS", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_UnitCS);
            sqlCmd.Parameters.Add(new SqlParameter("@ExpectTransfer", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ExpectTransfer);
            sqlCmd.Parameters.Add(new SqlParameter("@QueueTypeID", SqlDbType.Int)).Value = en.L_QueueTypeID;
            // ===== paging params =====
            sqlCmd.Parameters.Add(new SqlParameter("@Start", SqlDbType.Int)).Value = en.start;
            sqlCmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int)).Value = en.length;
            sqlCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 200)).Value = Commond.FormatExtension.NullToString(en.SearchTerm);
            try
            {
                sqlCon.Open();
                using var reader = ExecuteReader(sqlCmd);

                switch (en.L_Act)
                {
                    case "SummeryRegisterCareerType":
                        result = sp_GetQueueBank_ListSummeryRegisterCareerType(reader);
                        break;

                    default:
                        result = new List<ListSummeryRegister.ListSummeryRegisterCareerType>();
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetQueueBank");
                Log.Error("SEND Act: {Act}", en.L_Act);
                Log.Error("SEND ProjectID: {ProjectID}", en.L_ProjectID);
                Log.Error("SEND RegisterDate: {RegisterDateStart}", en.L_RegisterDateStart);
                Log.Error("SEND RegisterDate: {RegisterDateEnd}", en.L_RegisterDateEnd);
                Log.Error("SEND UnitID: {UnitID}", en.L_UnitID);
                Log.Error("SEND CSResponse: {CSResponse}", en.L_CSResponse);
                Log.Error("SEND UnitCS: {UnitCS}", en.L_UnitCS);
                Log.Error("SEND ExpectTransfer: {ExpectTransfer}", en.L_ExpectTransfer);
                Log.Error(ex, "Error executing sp_GetQueueBank");

                return new List<ListSummeryRegister.ListSummeryRegisterCareerType>();
            }
        }

        public override List<ListSummeryRegister.ListSummeryRegisterBank> sp_GetQueueBank_SummeryRegisterBank(GetQueueBankModel en)
        {
            var result = new List<ListSummeryRegister.ListSummeryRegisterBank>();

            using var sqlCon = new SqlConnection(ConnectionString);
            using var sqlCmd = new SqlCommand("sp_GetQueueBank", sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar, 50)).Value = Commond.FormatExtension.NullToString(en.L_Act);
            sqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateStart", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateStart);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateEnd", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateEnd);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar, -1)).Value = Commond.FormatExtension.NullToString(en.L_UnitID);
            sqlCmd.Parameters.Add(new SqlParameter("@CSResponse", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_CSResponse);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitCS", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_UnitCS);
            sqlCmd.Parameters.Add(new SqlParameter("@ExpectTransfer", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ExpectTransfer);
            sqlCmd.Parameters.Add(new SqlParameter("@QueueTypeID", SqlDbType.Int)).Value = en.L_QueueTypeID;
            // ===== paging params =====
            sqlCmd.Parameters.Add(new SqlParameter("@Start", SqlDbType.Int)).Value = en.start;
            sqlCmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int)).Value = en.length;
            sqlCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 200)).Value = Commond.FormatExtension.NullToString(en.SearchTerm);
            try
            {
                sqlCon.Open();
                using var reader = ExecuteReader(sqlCmd);

                switch (en.L_Act)
                {
                    case "SummeryRegisterBank":
                        result = sp_GetQueueBank_ListSummeryRegisterBank(reader);
                        break;

                    default:
                        result = new List<ListSummeryRegister.ListSummeryRegisterBank>();
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetQueueBank");
                Log.Error("SEND Act: {Act}", en.L_Act);
                Log.Error("SEND ProjectID: {ProjectID}", en.L_ProjectID);
                Log.Error("SEND RegisterDate: {RegisterDateStart}", en.L_RegisterDateStart);
                Log.Error("SEND RegisterDate: {RegisterDateEnd}", en.L_RegisterDateEnd);
                Log.Error("SEND UnitID: {UnitID}", en.L_UnitID);
                Log.Error("SEND CSResponse: {CSResponse}", en.L_CSResponse);
                Log.Error("SEND UnitCS: {UnitCS}", en.L_UnitCS);
                Log.Error("SEND ExpectTransfer: {ExpectTransfer}", en.L_ExpectTransfer);
                Log.Error(ex, "Error executing sp_GetQueueBank");

                return new List<ListSummeryRegister.ListSummeryRegisterBank>();
            }
        }

        public override List<ListCreateRegisterTableModel> sp_GetQueueBank_CreateRegisterTable(GetQueueBankModel en)
        {
            var result = new List<ListCreateRegisterTableModel>();

            using var sqlCon = new SqlConnection(ConnectionString);
            using var sqlCmd = new SqlCommand("sp_GetQueueBank", sqlCon)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar, 50)).Value = Commond.FormatExtension.NullToString(en.L_Act);
            sqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ProjectID);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateStart", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateStart);
            sqlCmd.Parameters.Add(new SqlParameter("@RegisterDateEnd", SqlDbType.NVarChar, 10)).Value = Commond.FormatExtension.NullToString(en.L_RegisterDateEnd);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar, -1)).Value = Commond.FormatExtension.NullToString(en.L_UnitID);
            sqlCmd.Parameters.Add(new SqlParameter("@CSResponse", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_CSResponse);
            sqlCmd.Parameters.Add(new SqlParameter("@UnitCS", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_UnitCS);
            sqlCmd.Parameters.Add(new SqlParameter("@ExpectTransfer", SqlDbType.NVarChar, 100)).Value = Commond.FormatExtension.NullToString(en.L_ExpectTransfer);
            sqlCmd.Parameters.Add(new SqlParameter("@QueueTypeID", SqlDbType.Int)).Value = en.L_QueueTypeID;
            // ===== paging params =====
            sqlCmd.Parameters.Add(new SqlParameter("@Start", SqlDbType.Int)).Value = en.start;
            sqlCmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int)).Value = en.length;
            sqlCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 200)).Value = Commond.FormatExtension.NullToString(en.SearchTerm);
            try
            {
                sqlCon.Open();
                using var reader = ExecuteReader(sqlCmd);

                switch (en.L_Act)
                {
                    case "CreateRegisterTable":
                        result = sp_GetQueueBank_ListCreateRegisterTable(reader);
                        break;

                    default:
                        result = new List<ListCreateRegisterTableModel>();
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetQueueBank");
                Log.Error("SEND Act: {Act}", en.L_Act);
                Log.Error("SEND ProjectID: {ProjectID}", en.L_ProjectID);
                Log.Error("SEND RegisterDate: {RegisterDateStart}", en.L_RegisterDateStart);
                Log.Error("SEND RegisterDate: {RegisterDateEnd}", en.L_RegisterDateEnd);
                Log.Error("SEND UnitID: {UnitID}", en.L_UnitID);
                Log.Error("SEND CSResponse: {CSResponse}", en.L_CSResponse);
                Log.Error("SEND UnitCS: {UnitCS}", en.L_UnitCS);
                Log.Error("SEND ExpectTransfer: {ExpectTransfer}", en.L_ExpectTransfer);
                Log.Error(ex, "Error executing sp_GetQueueBank");

                return new List<ListCreateRegisterTableModel>();
            }
        }
    }


}
