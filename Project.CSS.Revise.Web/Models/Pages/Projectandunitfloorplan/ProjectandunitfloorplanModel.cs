using Microsoft.AspNetCore.Mvc;

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

        public class UploadFileProjectFloorPlan
        {
            public string ProjectID { get; set; } = string.Empty;
            public string? ApplicationPath { get; set; }

            public List<string> FileName { get; set; } = new();     // no FromForm(Name=...)
            public List<IFormFile> Images { get; set; } = new();    // non-nullable list
        }

        public class UploadResult
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public int SavedCount { get; set; }
            public List<(Guid Id, string FileName, string FilePath)> Items { get; set; } = new();
        }

        public class UpdateFloorplanRequest
        {
            [FromForm] public Guid FloorPlanID { get; set; }
            [FromForm] public string ProjectID { get; set; } = string.Empty;

            // rename-only or used with NewImage
            [FromForm] public string? NewFileName { get; set; }

            // optional replacement
            [FromForm] public IFormFile? NewImage { get; set; }
        }

        public class BasicResult
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
        }

    }
}
