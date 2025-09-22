namespace Project.CSS.Revise.Web.Models.Pages.UserAndPermission
{
    public class UserAndPermissionModel
    {
        public class FiltersGetlistUser
        {
            public string? @L_Name { get; set; }
            public string? @L_DepartmentID { get; set; }
            public string? @L_RoleID { get; set; }
        }
        public class GetlistUser
        {
            public int ID { get; set; }
            public int index { get; set; }
            public string? FullnameTH { get; set; }
            public string? FullnameEN { get; set; }
            public string? Email { get; set; }
            public string? Mobile { get; set; }
            public int DepartmentID { get; set; }
            public string? DepartmentName { get; set; }
            public int RoleID { get; set; }
            public string? RoleName { get; set; }
        }
    }
}
