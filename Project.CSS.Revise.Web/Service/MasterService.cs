using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IMasterService
    {
        public List<BUModel> GetlistBU(BUModel model);
        public List<ProjectModel> GetlistPrject(ProjectModel model);
        public List<EventsModel> GetlistEvents(EventsModel model);
        public List<Monthevents> GetlistCountEventByMonth(Monthevents model);
    }
    public class MasterService : IMasterService
    {
        private readonly IMasterRepo _MasterRepo;

        public MasterService(IMasterRepo MasterRepo)
        {
            _MasterRepo = MasterRepo;
        }

        public List<BUModel> GetlistBU(BUModel model)
        {
            List<BUModel> resp = _MasterRepo.GetlistBU(model);
            return resp;
        }

        public List<EventsModel> GetlistEvents(EventsModel model)
        {
            List<EventsModel> resp = _MasterRepo.GetlistEvents(model);
            return resp;
        }

        public List<ProjectModel> GetlistPrject(ProjectModel model)
        {
            List<ProjectModel> resp = _MasterRepo.GetlistPrject(model);
            return resp;
        }

        public List<Monthevents> GetlistCountEventByMonth(Monthevents model)
        {
            List<Monthevents> resp = _MasterRepo.GetlistCountEventByMonth(model);
            return resp;
        }
    }
}
