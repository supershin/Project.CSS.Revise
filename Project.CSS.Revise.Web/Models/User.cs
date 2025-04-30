namespace Project.CSS.Revise.Web.Models
{
    public class User
    {
        public string? ID { get; set; }
        public string? QCTypeID { get; set; }
        public string? UserID { get; set; }
        public string? TitleID { get; set; }
        public string? FirstName { get; set; }
        public string? FirstName_Eng { get; set; }
        public string? TitleID_Eng { get; set; }
        public string? LastName { get; set; }
        public string? LastName_Eng { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public string? RoleID { get; set; }
        public bool? FlagAdmin { get; set; }
        public bool? FlagReadonly { get; set; }
        public bool? FlagActive { get; set; }
        public bool? IsQCFinishPlan { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
    }
}
