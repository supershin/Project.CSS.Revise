namespace Project.CSS.Revise.Web.Models.Pages.ProjectCounter
{
    public class ProjectCounterMappingModel
    {
        public class FilterData
        {
            public string? L_Bu { get; set; } = "";
            public string? L_ProjectID { get; set; } = "";
            public int? L_QueueType { get; set; } = -1;
            public string? L_ProjectStatus { get; set; } = "";
        }
        public class ListData
        {
            public int ID { get; set; }
            public string? ProjectID { get; set; } = "";
            public string? ProjectName { get; set; } = "";
            public int QueueTypeID { get; set; }
            public string? QueueTypeName { get; set; } = "";
            public int StartCounter { get; set; }
            public int EndCounter { get; set; }
            public int COUNT_Bank { get; set; }
            public int COUNT_Staff { get; set; }
        }
    }
}
