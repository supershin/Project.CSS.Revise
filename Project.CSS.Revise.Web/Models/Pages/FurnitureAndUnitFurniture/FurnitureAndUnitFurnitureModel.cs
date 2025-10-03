namespace Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture
{
    public class FurnitureAndUnitFurnitureModel
    {
        public class UnitFurnitureFilter
        {
            public string? L_BUG { get; set; }
            public string? L_ProjectID { get; set; }
            public string? L_UnitType { get; set; }
            public string? Src_UnitCode { get; set; }
        }
        public class UnitFurnitureListItem
        {
            public string? ID { get; set; }
            public bool? ISCheck { get; set; }
            public string? UnitCode { get; set; }
            public string? ProjectID { get; set; }
            public string? UnitType { get; set; }
            public string? QTYFurnitureUnit { get; set; }
            public string? CheckStatusID { get; set; }
            public string? CheckStatusName { get; set; }
            public string? FullnameTH { get; set; }
            public string? UpdateDate { get; set; }
        }
        public class SaveFurnitureProjectMappingRequest
        {
            public string ProjectID { get; set; } = "";
            public List<FurnitureItem> Furnitures { get; set; } = new();
            public List<UnitItem> Units { get; set; } = new();
        }

        public class FurnitureItem
        {
            public string id { get; set; } = "";
            public string name { get; set; } = "";
            public int qty { get; set; }
        }

        public class UnitItem
        {
            public string id { get; set; } = "";
            public string code { get; set; } = "";
            public string type { get; set; } = "";
        }

    }
}
