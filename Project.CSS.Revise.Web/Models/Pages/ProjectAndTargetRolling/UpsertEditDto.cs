namespace Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling
{
    public class UpsertEditDto
    {
        public string ProjectID { get; set; } = "";
        public int PlanTypeID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }         // 1..12
        public int PlanAmountID { get; set; }  // 183 = Unit, 184 = Value
        public decimal? OldValue { get; set; }
        public decimal? NewValue { get; set; }
    }
}
