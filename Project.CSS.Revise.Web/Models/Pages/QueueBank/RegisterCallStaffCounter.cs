using Project.CSS.Revise.Web.Data;

namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class RegisterCallStaffCounter : TR_Register_CallStaffCounter
    {
        public string ProjectID { get; set; }
        public int QueueTypeID { get; set; }
        public int? Counter { get; set; }

        private List<RegisterLogList> _RegisterLogList;
        public List<RegisterLogList> RegisterLogList
        {
            get
            {
                return (_RegisterLogList) ?? new List<RegisterLogList>();
            }
            set { _RegisterLogList = value; }
        }
    }
    public class RegisterLogList
    {
        public int RegisterLogID { get; set; }
    }
}
