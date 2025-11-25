namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class RegisterCounter
    {
        public int RegisterLogID { get; set; }
        public string ProjectID { get; set; }
        public int QueueTypeID { get; set; }
        public string UnitCode { get; set; }
        public int? Counter { get; set; }
        public bool FlagCancel { get; set; }
        public string CounterView { get; set; }
        public int FixedDuration { get; set; }
        public int ReasonID { get; set; }
        public bool PlaySound { get; set; }
        public bool FlagFastFixFinish { get; set; }
    }
}
