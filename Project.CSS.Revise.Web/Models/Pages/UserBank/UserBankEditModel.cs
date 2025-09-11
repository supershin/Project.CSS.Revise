namespace Project.CSS.Revise.Web.Models.Pages.UserBank
{
    public class UserBankEditModel
    {
        public int ID { get; set; }
        public int? UserTypeID { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public bool? ConsentAccept { get; set; }
        public bool? FlagActive { get; set; }

        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }

        public bool IsLeadBank { get; set; }   // bit -> bool
        public int? ParentBankID { get; set; }

        public int? AreaID { get; set; }

        public int? BankID { get; set; }
        public string? BankCode { get; set; }
        public string? BankName { get; set; }

        public string? ParentTeam { get; set; }

        public int? IUDBy { get; set; }
        public List<ListProject> ProjectUserBank { get; set; } = new();
    }

    public class ListProject
    {
        public string? ProjectID { get; set; }
        public int? BankUserID { get; set; }
    }
}
