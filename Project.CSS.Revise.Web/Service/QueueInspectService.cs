using Microsoft.AspNetCore.Mvc.Rendering;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IQueueInspectService
    {
        public string RemoveRegisterLog(int id);
        public List<SelectListItem> GetListUnitForRegisterInspect(string ProjectID);
    }
    public class QueueInspectService : IQueueInspectService
    {
        private readonly IQueueInspectRepo _QueueInspectRepo;

        public QueueInspectService(IQueueInspectRepo QueueInspectRepo)
        {
            _QueueInspectRepo = QueueInspectRepo;
        }

        public string RemoveRegisterLog(int id)
        {
            return _QueueInspectRepo.RemoveRegisterLog(id);
        }

        public List<SelectListItem> GetListUnitForRegisterInspect(string ProjectID)
        {
            return _QueueInspectRepo.GetListUnitForRegisterInspect(ProjectID);
        }

    }
}
