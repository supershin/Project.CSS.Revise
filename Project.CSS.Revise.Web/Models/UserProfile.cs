namespace Project.CSS.Revise.Web.Models
{
    public class UserProfile
    {
        public int ID { get; set; }
        public int? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public int? RoleID { get; set; }
        public int? BUID { get; set; }
        public string? TitleTH { get; set; }
        public string? TitleEN { get; set; }
        public string? FirstNameTH { get; set; }
        public string? LastNameTH { get; set; }
        public string? FirstNameEN { get; set; }
        public string? LastNameEN { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? FlagActive { get; set; }

        //For return answer
        public int? Status { get; set; }
        public string? Message { get; set; }
    }
}
