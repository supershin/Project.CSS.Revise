using Microsoft.Data.SqlClient;
using Project.CSS.Revise.Web.Models.Pages.QueueInspect;
using Serilog;
using System.Data;

namespace Project.CSS.Revise.Web.Library.DAL.SQL
{
    public class SqlMasterManagementQueueInspect : MasterManagementProviderQueueInspect
    {
        public SqlMasterManagementQueueInspect(IConfiguration configuration) : base(configuration) { }

        public override QueueInspectModel.ListModel sp_GetQueueInspect(QueueInspectModel.FiltersModel EN)
        {
            var result = new QueueInspectModel.ListModel();
            using var SqlCon = new SqlConnection(ConnectionString);
            using var SqlCmd = new SqlCommand("sp_GetQueueInspect", SqlCon) { CommandType = CommandType.StoredProcedure };

            SqlCmd.Parameters.Add(new SqlParameter("@Act", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.Act);
            SqlCmd.Parameters.Add(new SqlParameter("@Bu", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.Bu);
            SqlCmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.ProjectID);
            SqlCmd.Parameters.Add(new SqlParameter("@RegisterDateStart", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.RegisterDateStart);
            SqlCmd.Parameters.Add(new SqlParameter("@RegisterDateEnd", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.RegisterDateEnd);
            SqlCmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.UnitID);
            SqlCmd.Parameters.Add(new SqlParameter("@Inspect_Round", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.Inspect_Round);
            SqlCmd.Parameters.Add(new SqlParameter("@CSResponse", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.CSResponse);
            SqlCmd.Parameters.Add(new SqlParameter("@UnitCS", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.UnitCS);
            SqlCmd.Parameters.Add(new SqlParameter("@ExpectTransfer", SqlDbType.NVarChar)).Value = Commond.FormatExtension.NullToString(EN.ExpectTransfer);
            SqlCmd.Parameters.Add(new SqlParameter("@QueueTypeID", SqlDbType.Int)).Value = EN.QueueTypeID;
            // ===== paging params =====
            SqlCmd.Parameters.Add(new SqlParameter("@Start", SqlDbType.Int)).Value = EN.Start;
            SqlCmd.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int)).Value = EN.Length;
            SqlCmd.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar, 200)).Value = Commond.FormatExtension.NullToString(EN.SearchText);
            try
            {
                SqlCon.Open();
                using var reader = ExecuteReader(SqlCmd);
                switch (EN.Act)
                {
                    case "RegisterQueueInspectTable":
                        result.ListRegisterQueueInspectTable = sp_GetQueueInspect_RegisterQueueInspectTable_Reader(reader);
                        break;
                    case "RegisterQueueInspectSummary":
                        result.ListRegisterQueueInspectSummary = sp_GetQueueInspect_RegisterQueueInspectSummary_Reader(reader);
                        break;
                    case "RegisterQueueCheckingSummary":
                        result.ListRegisterQueueCheckingSummary = sp_GetQueueInspect_RegisterQueueCheckingSummary_Reader(reader);
                        break;
                    case "RegisterQueueTransferTypeSummary":
                        result.ListRegisterQueueTransferTypeSummary = sp_GetQueueInspect_RegisterQueueTransferTypeSummary_Reader(reader);
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Stored name : sp_GetQueueInspect");

                Log.Error("SEND Act: {Act}", EN.Act);
                Log.Error("SEND Bu: {Bu}", EN.Bu);
                Log.Error("SEND ProjectID: {ProjectID}", EN.ProjectID);
                Log.Error("SEND RegisterDateStart: {RegisterDateStart}", EN.RegisterDateStart);
                Log.Error("SEND RegisterDateEnd: {RegisterDateEnd}", EN.RegisterDateEnd);
                Log.Error("SEND UnitID: {UnitID}", EN.UnitID);
                Log.Error("SEND Inspect_Round: {Inspect_Round}", EN.Inspect_Round);
                Log.Error("SEND CSResponse: {CSResponse}", EN.CSResponse);
                Log.Error("SEND UnitCS: {UnitCS}", EN.UnitCS);
                Log.Error("SEND ExpectTransfer: {ExpectTransfer}", EN.ExpectTransfer);
                Log.Error("SEND QueueTypeID: {QueueTypeID}", EN.QueueTypeID);
                Log.Error("SEND Start: {Start}", EN.Start);
                Log.Error("SEND Length: {Length}", EN.Length);
                Log.Error("SEND SearchText: {SearchText}", EN.SearchText);

                Log.Error(ex, "Error executing sp_GetQueueInspect");
                return new QueueInspectModel.ListModel();
            }

        }

    }
}
