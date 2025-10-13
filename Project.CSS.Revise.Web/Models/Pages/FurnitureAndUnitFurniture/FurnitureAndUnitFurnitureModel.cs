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
            public string? L_CheckStatus { get; set; }
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
        // Unit + Furniture info
        public class UnitFurnitureModel
        {
            public Guid UnitID { get; set; }
            public string ProjectID { get; set; } = string.Empty;
            public string UnitCode { get; set; } = string.Empty;
            public int CheckStatusID { get; set; }

            public Guid? UnitFurnitureID { get; set; }
            public string? CheckRemark { get; set; }

            // รายการรายละเอียดเฟอร์นิเจอร์
            public List<UnitFurnitureDetailModel> Details { get; set; } = new();
        }

        // Furniture detail
        public class UnitFurnitureDetailModel
        {
            public int ID { get; set; }
            public Guid? UnitFurnitureID { get; set; }
            public int FurnitureID { get; set; }
            public string? FurnitureName { get; set; }
            public int Amount { get; set; }
            public bool FlagActive { get; set; }
        }

        public class UpdateFurnitureProjectMappingRequest
        {
            public string ProjectID { get; set; } = "";
            public Guid UnitID { get; set; }
            public string Remark { get; set; } = "";
            public List<UpdateFurnitureItem> Furnitures { get; set; } = new();
        }
        public class UpdateFurnitureItem
        {
            public string id { get; set; } = "";
            public string name { get; set; } = "";
            public int qty { get; set; }
        }
    }
}
