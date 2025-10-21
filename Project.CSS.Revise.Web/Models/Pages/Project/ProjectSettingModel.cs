namespace Project.CSS.Revise.Web.Models.Pages.Project
{
    public class ProjectSettingModel
    {
        public class ProjectFilter
        {
            public string? L_ProjectID { get; set; } = string.Empty;
            public string? L_BUID { get; set; } = string.Empty;
            public string? L_ProjectStatus { get; set; } = string.Empty;
            public string? L_ProjectPartner { get; set; } = string.Empty;
            public string? L_Company { get; set; } = string.Empty;
        }

        public class ListProjectItem
        {
            public string? ProjectID { get; set; }

            public string? CompanyID { get; set; }
            public string? CompanyName { get; set; }

            public string? BUID { get; set; }
            public string? BUName { get; set; }

            public string? PartnerID { get; set; }
            public string? PartnerName { get; set; }

            public string? ProjectName { get; set; }
            public string? ProjectName_Eng { get; set; }
            public string? ProjectType { get; set; }
            public string? ProjectStatusID { get; set; }
            public string? ProjectStatus { get; set; }

            public string? LandOfficeID { get; set; }      // (ถ้ามีหลายรายการจะได้ค่าเดี่ยวตาม SQL ปัจจุบัน)
            public string? LandOfficeName { get; set; }

            public string? ProjectZoneID { get; set; }     // เช่น "206,450"
            public string? ProjectZonename { get; set; }   // เช่น "Zone 2,All Zone"
        }

        public class DataProjectIUD
        {
            public string? ProjectID { get; set; }
            public int? CompanyID { get; set; }
            public int? BUID { get; set; }
            public int? PartnerID { get; set; }
            public string? ProjectName { get; set; }
            public string? ProjectName_Eng { get; set; }
            public string? ProjectType { get; set; }
            public int? ProjectStatus { get; set; }
            public int? LandOfficeID { get; set; }
            public int? ProjectZoneID { get; set; }
            public int? UserID { get; set; }
        }
        public class ReturnMessage
        {
            public bool? IsSuccess { get; set; }
            public string? Message { get; set; }
        }
    }
}
