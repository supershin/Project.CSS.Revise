namespace Project.CSS.Revise.Web.Models.Pages.UserBank
{
    public class GetlistUserBank
    {
        public class FilterData
        {
            public string? L_BankIDs { get; set; }
            public string? L_Name { get; set; }
        }

        public class ListData
        {
            public int? ID { get; set; }
            public string? FullName { get; set; }
            public string? BankCode { get; set; }
            public string? BankName { get; set; }
            public int? Role { get; set; }
        }
    }
}
