using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IMasterService
    {
        public List<BUModel> GetlistBU(BUModel model);
        public List<ProjectModel> GetlistPrject(ProjectModel model);
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

        public List<ProjectModel> GetlistPrject(ProjectModel model)
        {
            List<ProjectModel> resp = _MasterRepo.GetlistPrject(model);
            return resp;
        }
    }
}
