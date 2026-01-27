using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Models.Pages.QueueInspect;
using System.Data;

namespace Project.CSS.Revise.Web.Library.DAL
{
    public abstract class MasterManagementProviderQueueInspect : DataAccess
    {
        private static MasterManagementProviderQueueInspect _instance;

        protected static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration) => _configuration = configuration;

        public static MasterManagementProviderQueueInspect Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Project.CSS.Revise.Web.Library.DAL.SQL.SqlMasterManagementQueueInspect(_configuration);
                }
                return _instance;
            }
        }

        public MasterManagementProviderQueueInspect(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public abstract QueueInspectModel.ListModel sp_GetQueueInspect(QueueInspectModel.FiltersModel EN);


        #region __ Reader __
        public static List<QueueInspectModel.RegisterQueueInspectTableModel> sp_GetQueueInspect_RegisterQueueInspectTable_Reader(IDataReader reader)
        {
            List<QueueInspectModel.RegisterQueueInspectTableModel> list = new List<QueueInspectModel.RegisterQueueInspectTableModel>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetQueueInspect_RegisterQueueInspectTable_List_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }
        private static QueueInspectModel.RegisterQueueInspectTableModel sp_GetQueueInspect_RegisterQueueInspectTable_List_Reader(IDataReader reader, int index)
        {
            var Entity = new QueueInspectModel.RegisterQueueInspectTableModel();

            Entity.Index = index;
            Entity.ID = Commond.FormatExtension.NullToString(reader["ID"]);
            Entity.ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]);
            Entity.ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]);
            Entity.UnitID = Commond.FormatExtension.NullToString(reader["UnitID"]);
            Entity.UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]);
            Entity.CustomerName = Commond.FormatExtension.NullToString(reader["CustomerName"]);
            Entity.LineUserContract_Count = Commond.FormatExtension.NullToString(reader["LineUserContract_Count"]);

            Entity.AppointmentType = Commond.FormatExtension.NullToString(reader["AppointmentType"]);
            Entity.AppointDate = Commond.FormatExtension.ToStringFrom_DD_MM_YYYY_To_DD_MM_YYYY(reader["AppointDate"]);
            Entity.CSRespons = Commond.FormatExtension.NullToString(reader["CSRespons"]);
            Entity.Responsible = Commond.FormatExtension.NullToString(reader["Responsible"]);
            Entity.Status = Commond.FormatExtension.NullToString(reader["Status"]);
            Entity.RegisterDate = Commond.FormatExtension.ToStringFrom_DD_MM_YYYY_To_DD_MM_YYYY_HH_MM(reader["RegisterDate"]);
            Entity.Counter = Commond.FormatExtension.NullToString(reader["Counter"]);
            Entity.UnitStatus_CS = Commond.FormatExtension.NullToString(reader["UnitStatus_CS"]);
            Entity.TotalRecords = Commond.FormatExtension.NullToString(reader["TotalRecords"]);
            Entity.FilteredRecords = Commond.FormatExtension.NullToString(reader["FilteredRecords"]);
            return Entity;
        }


        public static List<QueueInspectModel.RegisterQueueInspectSummaryModel> sp_GetQueueInspect_RegisterQueueInspectSummary_Reader(IDataReader reader)
        {
            List<QueueInspectModel.RegisterQueueInspectSummaryModel> list = new List<QueueInspectModel.RegisterQueueInspectSummaryModel>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetQueueInspect_RegisterQueueInspectSummary_List_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }
        private static QueueInspectModel.RegisterQueueInspectSummaryModel sp_GetQueueInspect_RegisterQueueInspectSummary_List_Reader(IDataReader reader, int index)
        {
            var Entity = new QueueInspectModel.RegisterQueueInspectSummaryModel();

            Entity.Topic = Commond.FormatExtension.NullToString(reader["Topic"]);
            Entity.Unit = Commond.FormatExtension.ConvertToShortNameUnit(reader["Unit"]);
            Entity.Value = Commond.FormatExtension.ConvertToShortUnitV2(reader["Value"]);
            Entity.PercentUnit = Commond.FormatExtension.NullToString(reader["PercentUnit"]);
            Entity.Colorcode = Commond.FormatExtension.NullToString(reader["Colorcode"]);
            return Entity;
        }


        public static List<QueueInspectModel.RegisterQueueCheckingSummaryModel> sp_GetQueueInspect_RegisterQueueCheckingSummary_Reader(IDataReader reader)
        {
            List<QueueInspectModel.RegisterQueueCheckingSummaryModel> list = new List<QueueInspectModel.RegisterQueueCheckingSummaryModel>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetQueueInspect_RegisterQueueCheckingSummary_List_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }
        private static QueueInspectModel.RegisterQueueCheckingSummaryModel sp_GetQueueInspect_RegisterQueueCheckingSummary_List_Reader(IDataReader reader, int index)
        {
            var Entity = new QueueInspectModel.RegisterQueueCheckingSummaryModel();

            Entity.Checking_type = Commond.FormatExtension.NullToString(reader["Checking_type"]);
            Entity.Cnt_unit = Commond.FormatExtension.ConvertToShortNameUnit(reader["Cnt_unit"]);
            Entity.Sum_unit = Commond.FormatExtension.ConvertToShortUnitV2(reader["Sum_unit"]);
            Entity.Percent_Unit = Commond.FormatExtension.NullToString(reader["Percent_Unit"]);
            return Entity;
        }

        public static List<QueueInspectModel.RegisterQueueTransferTypeSummaryModel> sp_GetQueueInspect_RegisterQueueTransferTypeSummary_Reader(IDataReader reader)
        {
            List<QueueInspectModel.RegisterQueueTransferTypeSummaryModel> list = new List<QueueInspectModel.RegisterQueueTransferTypeSummaryModel>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetQueueInspect_RegisterQueueTransferTypeSummary_List_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }
        private static QueueInspectModel.RegisterQueueTransferTypeSummaryModel sp_GetQueueInspect_RegisterQueueTransferTypeSummary_List_Reader(IDataReader reader, int index)
        {
            var Entity = new QueueInspectModel.RegisterQueueTransferTypeSummaryModel();

            Entity.TransferType = Commond.FormatExtension.NullToString(reader["TransferType"]);
            Entity.Cnt_unit = Commond.FormatExtension.ConvertToShortNameUnit(reader["Cnt_unit"]);
            Entity.Sum_unit = Commond.FormatExtension.ConvertToShortUnitV2(reader["Sum_unit"]);
            Entity.Percent_Unit = Commond.FormatExtension.NullToString(reader["Percent_Unit"]);
            return Entity;
        }
        #endregion
    }
}
