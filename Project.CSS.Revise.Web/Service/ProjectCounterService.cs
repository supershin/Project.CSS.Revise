using Project.CSS.Revise.Web.Models.Pages.ProjectCounter;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IProjectCounterService
    {
        public List<ProjectCounterMappingModel.ListData> GetListsProjectCounterMapping(ProjectCounterMappingModel.FilterData filter);
    }
    public class ProjectCounterService : IProjectCounterService
    {
        private readonly IProjectCounterRepo _projectCounterRepo;

        public ProjectCounterService(IProjectCounterRepo ProjectCounterRepo)
        {
            _projectCounterRepo = ProjectCounterRepo;
        }

        public List<ProjectCounterMappingModel.ListData> GetListsProjectCounterMapping(ProjectCounterMappingModel.FilterData filter)
        {
            List<ProjectCounterMappingModel.ListData> resp = _projectCounterRepo.GetListsProjectCounterMapping(filter);
            return resp;
        }
    }
}
