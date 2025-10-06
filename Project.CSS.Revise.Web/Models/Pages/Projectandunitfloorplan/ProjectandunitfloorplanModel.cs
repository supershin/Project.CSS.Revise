namespace Project.CSS.Revise.Web.Models.Pages.Projectandunitfloorplan
{
    public class ProjectandunitfloorplanModel
    {
        public class ListProjectFloorplan
        {
            public Guid? ID { get; set; }
            public string ProjectID { get; set; } = "";
            public string FileName { get; set; } = "";
            public string FilePath { get; set; } = "";
            public string MimeType { get; set; } = "";
        }
        public class ListUnit
        {
            public Guid? ID { get; set; }
            public string ProjectID { get; set; } = "";
            public string UnitCode { get; set; } = "";
            public string UnitType { get; set; } = "";

        }
    }
}
