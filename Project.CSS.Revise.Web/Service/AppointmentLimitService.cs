using Project.CSS.Revise.Web.Models.Pages.AppointmentLimit;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IAppointmentLimitService
    {
        public List<AppointmentLimitModel.ProjectAppointLimitPivotRow> GetlistAppointmentLimit(AppointmentLimitModel.ProjectAppointLimitPivotRow filter);
        public AppointmentLimitModel.ProjectAppointLimitIUD InsertOrUpdateProjectAppointLimit(IEnumerable<AppointmentLimitModel.ProjectAppointLimitIUD> items, int UserID);
    }
    public class AppointmentLimitService : IAppointmentLimitService
    {
        private readonly IAppointmentLimitRepo _appointmentLimitRepo;

        public AppointmentLimitService(IAppointmentLimitRepo appointmentLimitRepo)
        {
            _appointmentLimitRepo = appointmentLimitRepo;
        }

        public List<AppointmentLimitModel.ProjectAppointLimitPivotRow> GetlistAppointmentLimit(AppointmentLimitModel.ProjectAppointLimitPivotRow filter)
        {
            return _appointmentLimitRepo.GetlistAppointmentLimit(filter);
        }

        public AppointmentLimitModel.ProjectAppointLimitIUD InsertOrUpdateProjectAppointLimit(IEnumerable<AppointmentLimitModel.ProjectAppointLimitIUD> items , int UserID)
        {
            return _appointmentLimitRepo.InsertOrUpdateProjectAppointLimit(items , UserID);
        }
    }
}
