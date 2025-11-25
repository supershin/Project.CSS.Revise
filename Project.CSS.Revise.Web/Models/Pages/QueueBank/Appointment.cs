using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;

namespace Project.CSS.Revise.Web.Models.Pages.QueueBank
{
    public class Appointment : TR_Appointment
    {
        public DateTime? StartDate { get; set; }

        private DateTime? _EndDate;
        public DateTime? EndDate
        {
            get
            {
                if (_EndDate != null)
                {
                    return (DateTime?)_EndDate.AsDate().Add(new TimeSpan(23, 59, 59));
                }
                return _EndDate;

            }
            set { _EndDate = value; }
        }
    }
}
