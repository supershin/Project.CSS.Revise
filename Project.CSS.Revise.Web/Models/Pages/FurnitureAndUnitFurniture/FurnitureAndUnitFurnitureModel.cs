namespace Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture
{
    public class FurnitureAndUnitFurnitureModel
    {
        public class UnitFurnitureFilter
        {
            public string? L_ProjectID { get; set; }
        }
        public class UnitFurnitureListItem
        {
            public string? ID { get; set; }
            public string? ISCheck { get; set; }
            public string? UnitCode { get; set; }
            public string? ProjectID { get; set; }
            public string? UnitType { get; set; }
            public string? QTYFurnitureUnit { get; set; }
            public string? CheckStatusID { get; set; }
            public string? CheckStatusName { get; set; }
            public string? FullnameTH { get; set; }
            public string? UpdateDate { get; set; }
        }

    }
}
