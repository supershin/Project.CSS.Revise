
namespace Project.CSS.Revise.Web.Models.Pages.QueueInspect
{
    public class QueueInspectModel
    {
        public class FiltersModel
        {
            public int Draw { get; set; } = 0;   // ✅ DataTables draw
            public string Act { get; set; } = string.Empty;
            public string Bu { get; set; } = string.Empty;
            public string ProjectID { get; set; } = string.Empty;

            public string RegisterDateStart { get; set; } = string.Empty;
            public string RegisterDateEnd { get; set; } = string.Empty;

            public string UnitID { get; set; } = string.Empty;
            public string Inspect_Round { get; set; } = string.Empty;
            public string CSResponse { get; set; } = string.Empty;
            public string UnitCS { get; set; } = string.Empty;
            public string ExpectTransfer { get; set; } = string.Empty;

            public int Start { get; set; } = 0;
            public int Length { get; set; } = 10;
            public int QueueTypeID { get; set; } = 49;

            public string SearchText { get; set; } = string.Empty;
        }


        public class ListModel
        {
            public List<RegisterQueueInspectTableModel> ListRegisterQueueInspectTable { get; set; } = new();
            public List<RegisterQueueInspectSummaryModel> ListRegisterQueueInspectSummary { get; set; } = new();
            public List<RegisterQueueCheckingSummaryModel> ListRegisterQueueCheckingSummary { get; set; } = new();
            public List<RegisterQueueTransferTypeSummaryModel> ListRegisterQueueTransferTypeSummary { get; set; } = new();
        }

        public class RegisterQueueInspectTableModel
        {
            public int? Index { get; set; }
            public string? ID { get; set; }
            public string? ProjectID { get; set; }
            public string? ProjectName { get; set; }

            public string? UnitID { get; set; }
            public string? UnitCode { get; set; }
            public string? CustomerName { get; set; }

            public string? LineUserContract_Count { get; set; }

            public string? AppointmentType { get; set; }
            public string? AppointDate { get; set; }

            public string? CSRespons { get; set; }
            public string? Responsible { get; set; }

            public string? Status { get; set; }

            public string? RegisterDate { get; set; }
            public string? FixedDuration { get; set; }
            public string? Counter { get; set; }

            public string? UnitStatus_CS { get; set; }

            public string? TotalRecords { get; set; }
            public string? FilteredRecords { get; set; }
        }

        public class RegisterQueueInspectSummaryModel
        {
            public string? Topic { get; set; }
            public string? Unit { get; set; }
            public string? Value { get; set; }
            public string? PercentUnit { get; set; }
            public string? Colorcode { get; set; }
        }

        public class RegisterQueueCheckingSummaryModel
        {
            public string? Checking_type { get; set; }
            public string? Cnt_unit { get; set; }
            public string? Sum_unit { get; set; }
            public string? Percent_Unit { get; set; }
        }

        public class RegisterQueueTransferTypeSummaryModel
        {
            public string? TransferType { get; set; }
            public string? Cnt_unit { get; set; }
            public string? Sum_unit { get; set; }
            public string? Percent_Unit { get; set; }
        }
    }
}
