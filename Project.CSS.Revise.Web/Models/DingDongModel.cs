namespace Project.CSS.Revise.Web.Models
{
    public class DingDongModel
    {
        public class Filter
        {
            public string? ProjectID { get; set; }
            public DateTime? Day { get; set; } = null;

            // baseline from client
            public string? LastRegisterSignature { get; set; }
            public DateTime? LastBankUpdateDate { get; set; }
        }

        public class Result
        {
            public bool CanDingDong { get; set; }

            // ✅ always return latest values (client must store them for next time)
            public string ProjectID { get; set; } = "";
            public string RegisterSignature { get; set; } = "0";
            public DateTime? BankLatestUpdateDate { get; set; }

            // optional debug flags
            public bool RegisterChanged { get; set; }
            public bool BankChanged { get; set; }
        }
    }
}
