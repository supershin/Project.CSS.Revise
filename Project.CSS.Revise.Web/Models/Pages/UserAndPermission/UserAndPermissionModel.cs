using System.ComponentModel.DataAnnotations;

namespace Project.CSS.Revise.Web.Models.Pages.UserAndPermission
{
    public class UserAndPermissionModel
    {
        public class FiltersGetlistUser
        {
            public string? @L_Name { get; set; }
            public string? @L_DepartmentID { get; set; }
            public string? @L_RoleID { get; set; }
            public string? @L_UserID { get; set; }
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

        public class CreateUserRequest
        {
            [Required] public int? TitleID { get; set; }
            [Required] public string? UserID { get; set; }
            [Required] public string Password { get; set; }
            public int? QCTypeID { get; set; }
            [Required] public string? FirstName { get; set; }
            [Required] public string? LastName { get; set; }

            public string? FirstName_Eng { get; set; }
            public string? LastName_Eng { get; set; }
            public string? Email { get; set; }
            public string? Mobile { get; set; }


            public int? DepartmentID { get; set; }
            public int? RoleID { get; set; }


            public bool? FlagAdmin { get; set; }
            public bool? FlagReadonly { get; set; }
            public bool? FlagActive { get; set; }

            public List<int> BUIds { get; set; } = new List<int>();
        }

        public class UpdateUserRequest
        {
            [Required] public int? ID { get; set; }
            public int? TitleID { get; set; }
            [Required] public string? UserID { get; set; }
            [Required] public string? FirstName { get; set; }
            [Required] public string? LastName { get; set; }

            public string? FirstName_Eng { get; set; }
            public string? LastName_Eng { get; set; }
            public string? Email { get; set; }
            public string? Mobile { get; set; }
            public string? Password { get; set; }

            public int? DepartmentID { get; set; }
            public int? RoleID { get; set; }

            public bool? FlagAdmin { get; set; }
            public bool? FlagReadonly { get; set; }
            public bool? FlagActive { get; set; }

            // ✅ เพิ่ม List BU สำหรับอัปเดต Mapping
            public List<int> BUIds { get; set; } = new List<int>();
        }

        public class DuplicateCheckResult
        {
            public bool EmailExists { get; set; }
            public bool UserIdExists { get; set; }
            public bool FullNameThExists { get; set; }
            public bool FullNameEnExists { get; set; }

            public int? EmailConflictId { get; set; }
            public int? UserIdConflictId { get; set; }
            public int? FullNameThConflictId { get; set; }
            public int? FullNameEnConflictId { get; set; }

            public bool HasAnyConflict => EmailExists || UserIdExists || FullNameThExists || FullNameEnExists;
        }

        public class UserDetail
        {
            public int? ID { get; set; }
            public string? UserID { get; set; }
            public int? TitleID { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }

            public string? FirstName_Eng { get; set; }
            public string? LastName_Eng { get; set; }

            public string? Password { get; set; }
            public string? Email { get; set; }
            public string? Mobile { get; set; }
            public int? DepartmentID { get; set; }
            public string? BUMaping { get; set; }
            public int? RoleID { get; set; }
            public bool? FlagAdmin { get; set; }
            public bool? FlagReadonly { get; set; }
            public bool? FlagActive { get; set; }
        }

        public class GetlistProjects
        {

            public int? index { get; set; }
            public string? BUName { get; set; }
            public string? ProjectID { get; set; }
            public string? ProjectName { get; set; }
            public string? ProjectName_Eng { get; set; }
            public bool? ISCheck { get; set; }

        }

        public class IUDProjectUserMapping
        {

            public int? UserID { get; set; }
            public List<string> ProjectID { get; set; } = new List<string>();
        }
    }
}
