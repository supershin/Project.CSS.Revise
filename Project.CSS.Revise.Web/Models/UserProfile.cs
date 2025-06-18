namespace Project.CSS.Revise.Web.Models
{
    public class UserProfile
    {
        public int ID { get; set; }
        public int? DepartmentID { get; set; }
        public int? RoleID { get; set; }
        public int? BUID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
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
