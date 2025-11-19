using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using System.Data;

namespace Project.CSS.Revise.Web.Library.DAL
{
    public abstract class MasterManagementProviderProject : DataAccess
    {
        private static MasterManagementProviderProject _instance;

        protected static IConfiguration _configuration;

        // ✅ เพิ่มเมธอดสำหรับตั้งค่า config ให้ static field
        public static void Initialize(IConfiguration configuration) => _configuration = configuration;

        public static MasterManagementProviderProject Instance
        {
            get
            {
                if (_instance == null)
                {
                    // ✅ ตอนนี้ _configuration ไม่ null แล้ว (ตั้งค่าจาก Program.cs)
                    _instance = new Project.CSS.Revise.Web.Library.DAL.SQL.SqlMasterManagementProject(_configuration);
                }
                return _instance;
            }
        }



        public MasterManagementProviderProject(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public abstract List<RollingPlanSummaryModel> sp_GetProjecTargetRollingPlanList_GetListTargetRollingPlan(RollingPlanSummaryModel en);

        public abstract List<RollingPlanTotalModel> sp_GetProjecTargetRollingPlanList_GetDataTotalTargetRollingPlan(RollingPlanTotalModel en);

        public abstract SPGetDataCSResponse.ListData sp_GetDataCSResponse(SPGetDataCSResponse.FilterData en);

        public abstract List<ListDataRegisterTable> sp_GetQueueBank_RegisterTable(GetQueueBankModel en);

        #region __ Reader __

        public static List<RollingPlanSummaryModel> sp_GetProjecTargetRollingPlanList_Getlisttable_ListReader(IDataReader reader)
        {
            List<RollingPlanSummaryModel> list = new List<RollingPlanSummaryModel>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetProjecTargetRollingPlanList_Getlisttable_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }

        private static RollingPlanSummaryModel sp_GetProjecTargetRollingPlanList_Getlisttable_Reader(IDataReader reader, int index)
        {
            var Entity = new RollingPlanSummaryModel();

            Entity.index = index;
            Entity.ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]);
            Entity.ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]);
            Entity.BuName = Commond.FormatExtension.NullToString(reader["BUName"]);
            Entity.PlanTypeID = Commond.FormatExtension.NullToString(reader["PlanTypeID"]);
            Entity.PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]);
            Entity.PlanYear = Commond.FormatExtension.NullToString(reader["PlanYear"]);

            // ===== JAN =====
            Entity.Jan_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jan_Unit"]);
            Entity.Jan_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Jan_Unit"]);
            Entity.Jan_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Jan_Value"]);
            Entity.Jan_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Jan_Value"]);

            // ===== FEB =====
            Entity.Feb_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Feb_Unit"]);
            Entity.Feb_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Feb_Unit"]);
            Entity.Feb_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Feb_Value"]);
            Entity.Feb_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Feb_Value"]);

            // ===== MAR =====
            Entity.Mar_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Mar_Unit"]);
            Entity.Mar_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Mar_Unit"]);
            Entity.Mar_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Mar_Value"]);
            Entity.Mar_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Mar_Value"]);

            // ===== APR =====
            Entity.Apr_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Apr_Unit"]);
            Entity.Apr_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Apr_Unit"]);
            Entity.Apr_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Apr_Value"]);
            Entity.Apr_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Apr_Value"]);

            // ===== MAY =====
            Entity.May_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["May_Unit"]);
            Entity.May_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["May_Unit"]);
            Entity.May_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["May_Value"]);
            Entity.May_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["May_Value"]);

            // ===== JUN =====
            Entity.Jun_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jun_Unit"]);
            Entity.Jun_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Jun_Unit"]);
            Entity.Jun_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Jun_Value"]);
            Entity.Jun_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Jun_Value"]);

            // ===== JUL =====
            Entity.Jul_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Jul_Unit"]);
            Entity.Jul_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Jul_Unit"]);
            Entity.Jul_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Jul_Value"]);
            Entity.Jul_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Jul_Value"]);

            // ===== AUG =====
            Entity.Aug_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Aug_Unit"]);
            Entity.Aug_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Aug_Unit"]);
            Entity.Aug_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Aug_Value"]);
            Entity.Aug_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Aug_Value"]);

            // ===== SEP =====
            Entity.Sep_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Sep_Unit"]);
            Entity.Sep_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Sep_Unit"]);
            Entity.Sep_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Sep_Value"]);
            Entity.Sep_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Sep_Value"]);

            // ===== OCT =====
            Entity.Oct_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Oct_Unit"]);
            Entity.Oct_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Oct_Unit"]);
            Entity.Oct_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Oct_Value"]);
            Entity.Oct_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Oct_Value"]);

            // ===== NOV =====
            Entity.Nov_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Nov_Unit"]);
            Entity.Nov_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Nov_Unit"]);
            Entity.Nov_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Nov_Value"]);
            Entity.Nov_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Nov_Value"]);

            // ===== DEC =====
            Entity.Dec_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Dec_Unit"]);
            Entity.Dec_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Dec_Unit"]);
            Entity.Dec_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Dec_Value"]);
            Entity.Dec_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Dec_Value"]);

            // ===== TOTALS =====
            Entity.Total_Unit = Commond.FormatExtension.ConvertToShortUnit(reader["Total_Unit"]);
            Entity.Total_Unit_comma = Commond.FormatExtension.ConvertToMoney(reader["Total_Unit"]);
            Entity.Total_Value = Commond.FormatExtension.ConvertToShortUnithaveZero(reader["Total_Value"]);
            Entity.Total_Value_comma = Commond.FormatExtension.ConvertToMoney(reader["Total_Value"]);

            return Entity;
        }


        public static List<RollingPlanSummaryModel> sp_GetProjecTargetRollingPlanList_Getlisttable_ForExport_ListReader(IDataReader reader)
        {
            List<RollingPlanSummaryModel> list = new List<RollingPlanSummaryModel>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetProjecTargetRollingPlanList_Getlisttable_ForExport_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }

        private static RollingPlanSummaryModel sp_GetProjecTargetRollingPlanList_Getlisttable_ForExport_Reader(IDataReader reader, int index)
        {
            var Entity = new RollingPlanSummaryModel();

            Entity.index = index;
            Entity.ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]);
            Entity.ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]);
            Entity.BuName = Commond.FormatExtension.NullToString(reader["BUName"]);
            Entity.PlanTypeID = Commond.FormatExtension.NullToString(reader["PlanTypeID"]);
            Entity.PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]);
            Entity.PlanYear = Commond.FormatExtension.NullToString(reader["PlanYear"]);

            // ===== JAN =====
            Entity.Jan_Unit = Commond.FormatExtension.NullToString(reader["Jan_Unit"]);
            Entity.Jan_Unit_comma = Commond.FormatExtension.NullToString(reader["Jan_Unit"]);
            Entity.Jan_Value = Commond.FormatExtension.NullToString(reader["Jan_Value"]);
            Entity.Jan_Value_comma = Commond.FormatExtension.NullToString(reader["Jan_Value"]);

            // ===== FEB =====
            Entity.Feb_Unit = Commond.FormatExtension.NullToString(reader["Feb_Unit"]);
            Entity.Feb_Unit_comma = Commond.FormatExtension.NullToString(reader["Feb_Unit"]);
            Entity.Feb_Value = Commond.FormatExtension.NullToString(reader["Feb_Value"]);
            Entity.Feb_Value_comma = Commond.FormatExtension.NullToString(reader["Feb_Value"]);

            // ===== MAR =====
            Entity.Mar_Unit = Commond.FormatExtension.NullToString(reader["Mar_Unit"]);
            Entity.Mar_Unit_comma = Commond.FormatExtension.NullToString(reader["Mar_Unit"]);
            Entity.Mar_Value = Commond.FormatExtension.NullToString(reader["Mar_Value"]);
            Entity.Mar_Value_comma = Commond.FormatExtension.NullToString(reader["Mar_Value"]);

            // ===== APR =====
            Entity.Apr_Unit = Commond.FormatExtension.NullToString(reader["Apr_Unit"]);
            Entity.Apr_Unit_comma = Commond.FormatExtension.NullToString(reader["Apr_Unit"]);
            Entity.Apr_Value = Commond.FormatExtension.NullToString(reader["Apr_Value"]);
            Entity.Apr_Value_comma = Commond.FormatExtension.NullToString(reader["Apr_Value"]);

            // ===== MAY =====
            Entity.May_Unit = Commond.FormatExtension.NullToString(reader["May_Unit"]);
            Entity.May_Unit_comma = Commond.FormatExtension.NullToString(reader["May_Unit"]);
            Entity.May_Value = Commond.FormatExtension.NullToString(reader["May_Value"]);
            Entity.May_Value_comma = Commond.FormatExtension.NullToString(reader["May_Value"]);

            // ===== JUN =====
            Entity.Jun_Unit = Commond.FormatExtension.NullToString(reader["Jun_Unit"]);
            Entity.Jun_Unit_comma = Commond.FormatExtension.NullToString(reader["Jun_Unit"]);
            Entity.Jun_Value = Commond.FormatExtension.NullToString(reader["Jun_Value"]);
            Entity.Jun_Value_comma = Commond.FormatExtension.NullToString(reader["Jun_Value"]);

            // ===== JUL =====
            Entity.Jul_Unit = Commond.FormatExtension.NullToString(reader["Jul_Unit"]);
            Entity.Jul_Unit_comma = Commond.FormatExtension.NullToString(reader["Jul_Unit"]);
            Entity.Jul_Value = Commond.FormatExtension.NullToString(reader["Jul_Value"]);
            Entity.Jul_Value_comma = Commond.FormatExtension.NullToString(reader["Jul_Value"]);

            // ===== AUG =====
            Entity.Aug_Unit = Commond.FormatExtension.NullToString(reader["Aug_Unit"]);
            Entity.Aug_Unit_comma = Commond.FormatExtension.NullToString(reader["Aug_Unit"]);
            Entity.Aug_Value = Commond.FormatExtension.NullToString(reader["Aug_Value"]);
            Entity.Aug_Value_comma = Commond.FormatExtension.NullToString(reader["Aug_Value"]);

            // ===== SEP =====
            Entity.Sep_Unit = Commond.FormatExtension.NullToString(reader["Sep_Unit"]);
            Entity.Sep_Unit_comma = Commond.FormatExtension.NullToString(reader["Sep_Unit"]);
            Entity.Sep_Value = Commond.FormatExtension.NullToString(reader["Sep_Value"]);
            Entity.Sep_Value_comma = Commond.FormatExtension.NullToString(reader["Sep_Value"]);

            // ===== OCT =====
            Entity.Oct_Unit = Commond.FormatExtension.NullToString(reader["Oct_Unit"]);
            Entity.Oct_Unit_comma = Commond.FormatExtension.NullToString(reader["Oct_Unit"]);
            Entity.Oct_Value = Commond.FormatExtension.NullToString(reader["Oct_Value"]);
            Entity.Oct_Value_comma = Commond.FormatExtension.NullToString(reader["Oct_Value"]);

            // ===== NOV =====
            Entity.Nov_Unit = Commond.FormatExtension.NullToString(reader["Nov_Unit"]);
            Entity.Nov_Unit_comma = Commond.FormatExtension.NullToString(reader["Nov_Unit"]);
            Entity.Nov_Value = Commond.FormatExtension.NullToString(reader["Nov_Value"]);
            Entity.Nov_Value_comma = Commond.FormatExtension.NullToString(reader["Nov_Value"]);

            // ===== DEC =====
            Entity.Dec_Unit = Commond.FormatExtension.NullToString(reader["Dec_Unit"]);
            Entity.Dec_Unit_comma = Commond.FormatExtension.NullToString(reader["Dec_Unit"]);
            Entity.Dec_Value = Commond.FormatExtension.NullToString(reader["Dec_Value"]);
            Entity.Dec_Value_comma = Commond.FormatExtension.NullToString(reader["Dec_Value"]);

            // ===== TOTALS =====
            Entity.Total_Unit = Commond.FormatExtension.NullToString(reader["Total_Unit"]);
            Entity.Total_Unit_comma = Commond.FormatExtension.NullToString(reader["Total_Unit"]);
            Entity.Total_Value = Commond.FormatExtension.NullToString(reader["Total_Value"]);
            Entity.Total_Value_comma = Commond.FormatExtension.NullToString(reader["Total_Value"]);

            return Entity;
        }


        public static List<RollingPlanTotalModel> sp_GetProjecTargetRollingPlanList_GetlistTotal_ListReader(IDataReader reader)
        {
            List<RollingPlanTotalModel> list = new List<RollingPlanTotalModel>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetProjecTargetRollingPlanList_GetlistTotal_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }

        private static RollingPlanTotalModel sp_GetProjecTargetRollingPlanList_GetlistTotal_Reader(IDataReader reader, int index)
        {
            var Entity = new RollingPlanTotalModel();

            Entity.index = index;
            Entity.PlanTypeName = Commond.FormatExtension.NullToString(reader["PlanTypeName"]);
            Entity.PlanAmountName = Commond.FormatExtension.NullToString(reader["PlanAmountName"]);
            Entity.TOTAL = Commond.FormatExtension.ConvertToShortUnitV2(reader["TOTAL"],Entity.PlanAmountName);
            Entity.COLORS = Commond.FormatExtension.NullToString(reader["COLORS"]);

            return Entity;
        }


        public static List<SPGetDataCSResponse.ListCSSummary> sp_GetDataCSResponse_GetListCSSummary_ListReader(IDataReader reader)
        {
            List<SPGetDataCSResponse.ListCSSummary> list = new List<SPGetDataCSResponse.ListCSSummary>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetDataCSResponse_GetListCSSummary_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }

        private static SPGetDataCSResponse.ListCSSummary sp_GetDataCSResponse_GetListCSSummary_Reader(IDataReader reader, int index)
        {
            var Entity = new SPGetDataCSResponse.ListCSSummary();

            Entity.index = index;
            Entity.ID = Commond.FormatExtension.Nulltoint(reader["ID"]);
            Entity.FullnameTH = Commond.FormatExtension.NullToString(reader["FullnameTH"]);
            Entity.FullnameEN = Commond.FormatExtension.NullToString(reader["FullnameEN"]);
            Entity.Email = Commond.FormatExtension.NullToString(reader["Email"]);
            Entity.Mobile = Commond.FormatExtension.NullToString(reader["Mobile"]);
            Entity.Cnt_Project = Commond.FormatExtension.Nulltoint(reader["Cnt_Project"]);
            Entity.Cnt_UnitCode = Commond.FormatExtension.Nulltoint(reader["Cnt_UnitCode"]);

            return Entity;
        }


        public static List<SPGetDataCSResponse.ListCountUnitStatus> sp_GetDataCSResponse_GetListCountUnitStatus_ListReader(IDataReader reader)
        {
            List<SPGetDataCSResponse.ListCountUnitStatus> list = new List<SPGetDataCSResponse.ListCountUnitStatus>();
            int index = 1;
            while ((reader.Read()))
            {
                list.Add(sp_GetDataCSResponse_GetListCountUnitStatus_Reader(reader, index));
                index++;
            }
            reader.Close();
            return list;
        }

        private static SPGetDataCSResponse.ListCountUnitStatus sp_GetDataCSResponse_GetListCountUnitStatus_Reader(IDataReader reader, int index)
        {
            var Entity = new SPGetDataCSResponse.ListCountUnitStatus();

            Entity.index = index;
            Entity.ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]);
            Entity.ProjectName = Commond.FormatExtension.NullToString(reader["ProjectName"]);
            Entity.Total = Commond.FormatExtension.Nulltoint(reader["Total"]);
            Entity.ID_62 = Commond.FormatExtension.Nulltoint(reader["ID_62"]);
            Entity.ID_63 = Commond.FormatExtension.Nulltoint(reader["ID_63"]);
            Entity.ID_64 = Commond.FormatExtension.Nulltoint(reader["ID_64"]);
            Entity.ID_65 = Commond.FormatExtension.Nulltoint(reader["ID_65"]);
            Entity.ID_67 = Commond.FormatExtension.Nulltoint(reader["ID_67"]);
            Entity.ID_68 = Commond.FormatExtension.Nulltoint(reader["ID_68"]);
            Entity.ID_69 = Commond.FormatExtension.Nulltoint(reader["ID_69"]);
            Entity.ID_70 = Commond.FormatExtension.Nulltoint(reader["ID_70"]);
            Entity.ID_71 = Commond.FormatExtension.Nulltoint(reader["ID_71"]);
            Entity.ID_373 = Commond.FormatExtension.Nulltoint(reader["ID_373"]);
            Entity.ID_408 = Commond.FormatExtension.Nulltoint(reader["ID_408"]);
            Entity.ID_435 = Commond.FormatExtension.Nulltoint(reader["ID_435"]);
            Entity.ID_436 = Commond.FormatExtension.Nulltoint(reader["ID_436"]);
            Entity.ID_438 = Commond.FormatExtension.Nulltoint(reader["ID_438"]);
            return Entity;
        }


        public static List<ListDataRegisterTable> sp_GetQueueBank_ListDataRegisterTable(IDataReader reader)
        {
            var list = new List<ListDataRegisterTable>();
            int index = 1;

            while (reader.Read())
            {
                list.Add(sp_GetQueueBank_ListDataRegisterTable_Reader(reader, index));
                index++;
            }

            reader.Close();
            return list;
        }

        private static ListDataRegisterTable sp_GetQueueBank_ListDataRegisterTable_Reader(IDataReader reader, int index)
        {
            var entity = new ListDataRegisterTable();

            entity.index = index;
            entity.ID = Commond.FormatExtension.NullToString(reader["ID"]);
            entity.ProjectID = Commond.FormatExtension.NullToString(reader["ProjectID"]);
            entity.UnitID = Commond.FormatExtension.NullToString(reader["UnitID"]);
            entity.UnitCode = Commond.FormatExtension.NullToString(reader["UnitCode"]);
            entity.CustomerName = Commond.FormatExtension.NullToString(reader["CustomerName"]);

            entity.LineUserContract_Count = Commond.FormatExtension.NullToString(reader["LineUserContract_Count"]);
            entity.LoanDraftDate = Commond.FormatExtension.NullToString(reader["LoanDraftDate"]);
            entity.LoanSubmitDate = Commond.FormatExtension.NullToString(reader["LoanSubmitDate"]);

            entity.Appointment = Commond.FormatExtension.NullToString(reader["Appointment"]);
            entity.Status = Commond.FormatExtension.NullToString(reader["Status"]);
            entity.StatusTime = Commond.FormatExtension.NullToString(reader["StatusTime"]);

            entity.Counter = Commond.FormatExtension.NullToString(reader["Counter"]);
            entity.Unitstatus_CS = Commond.FormatExtension.NullToString(reader["Unitstatus_CS"]);
            entity.ResponsibleID = Commond.FormatExtension.NullToString(reader["ResponsibleID"]);
            entity.CSResponse = Commond.FormatExtension.NullToString(reader["CSResponse"]);

            return entity;
        }


        #endregion
    }
}
