namespace Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling
{
    public class TargetRollingPlanInsertModel
    {
        public string? ProjectID { get; set; }
        public int PlanTypeID { get; set; }
        public int PlanAmountID { get; set; } // 183 = Unit, 184 = Value
        public DateTime MonthlyDate { get; set; }
        public decimal Amount { get; set; }
        public bool FlagActive { get; set; } = true;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int CreateBy { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public int UpdateBy { get; set; }


        public int Inserted { get; set; }
        public int Updated { get; set; }
        public int Skipped { get; set; }
        public int Total => Inserted + Updated + Skipped;
        public string Message { get; set; } = "";
    }
}
