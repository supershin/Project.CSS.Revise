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
            public int Cnt_FloorPlan { get; set; } = 0;
        }
        public class SaveMappingFloorplanModel
        {
            public string ProjectID { get; set; } = string.Empty;
            public List<Guid> FloorPlanIDs { get; set; } = new();
            public List<Guid> UnitIDs { get; set; } = new();
        }
        public class ListFloorPlanByUnit
        {
            public Guid ID { get; set; }
            public Guid? FloorPlanID { get; set; }
            public string FileName { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;
            public string MimeType { get; set; } = string.Empty;
        }
    }
}
