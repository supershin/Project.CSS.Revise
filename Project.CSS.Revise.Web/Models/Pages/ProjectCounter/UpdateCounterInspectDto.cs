namespace Project.CSS.Revise.Web.Models.Pages.ProjectCounter
{
    public class UpdateCounterInspectDto
    {
        public int Id { get; set; }              // TR_ProjectCounter_Mapping.ID
        public int QueueTypeId { get; set; } = 49;
        public int CounterQty { get; set; }      // EndCounter
        public int? UserID { get; set; }      // optional; set server-side if you prefer
    }

    public class BasicResponse
    {
        public bool Ok { get; set; }
        public string? Message { get; set; }
    }
}
