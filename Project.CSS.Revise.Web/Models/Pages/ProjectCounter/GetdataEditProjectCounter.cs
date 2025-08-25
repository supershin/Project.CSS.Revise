namespace Project.CSS.Revise.Web.Models.Pages.ProjectCounter
{
    public class GetdataEditProjectCounter
    {
        public class ProjectCounterDetailVm
        {
            public string? ID { get; set; }
            public string? ProjectName { get; set; }
            public string? QueueType { get; set; }
            public string? EndCounter { get; set; }
            public string? UpdateDate { get; set; }     // "dd/MM/yyyy HH:mm"
            public string? UpdateName { get; set; }
            public List<BankStaffVm> Banks { get; set; } = new();  // แนบเฉพาะเมื่อ QueueTypeID = 48
        }

        public class BankStaffVm
        {
            public string? BankID { get; set; }
            public string? BankCode { get; set; }
            public string? BankName { get; set; }
            public string? Staff { get; set; }          // จำนวนพนักงานต่อธนาคาร
            public bool? FlagActive { get; set; }     // จาก T1.FlagActive ตาม SQL ตัวอย่าง
            public string? UpdateDate { get; set; }     // จาก T1.UpdateDate ตาม SQL ตัวอย่าง
        }
    }
}
