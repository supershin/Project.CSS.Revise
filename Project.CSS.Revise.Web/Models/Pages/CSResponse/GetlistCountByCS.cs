using static Project.CSS.Revise.Web.Models.Pages.ProjectCounter.CreateCounterRequest;

namespace Project.CSS.Revise.Web.Models.Pages.CSResponse
{
    public class GetlistCountByCS
    {
        public int ID { get; set; }
        public string? FullnameTH { get; set; }
        public string? FullnameEN { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public List<ListProjectAndCountUnit> Project { get; set; } = new();
    }
    public sealed class ListProjectAndCountUnit
    {
        public string ProjectID { get; set; } = "";
        public string ProjectName { get; set; } = "";
        public int CountUnit { get; set; }
        public List<ListUnitCoutstatus> Unit { get; set; } = new();
    }
    public sealed class ListUnitCoutstatus
    {
        public int UnitStatus { get; set; } = 0;
        public string UnitStatusName { get; set; } = "";
        public int TotalUnit { get; set; } = 0;
    }

}
