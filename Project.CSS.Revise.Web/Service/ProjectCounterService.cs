using Project.CSS.Revise.Web.Models.Pages.ProjectCounter;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IProjectCounterService
    {
        public List<ProjectCounterMappingModel.ListData> GetListsProjectCounterMapping(ProjectCounterMappingModel.FilterData filter);
        public CreateCounterRequest.Response CreateEventsAndShops(CreateCounterRequest model);
        Task<GetdataEditProjectCounter.ProjectCounterDetailVm?> GetProjectCounterDetailAsync(int id);
        public UpdateCounterBankRequest.Response UpdateCounterBank(UpdateCounterBankRequest dto);
        Task<BasicResponse> UpdateCounterInspectAsync(UpdateCounterInspectDto dto);
    }
    public class ProjectCounterService : IProjectCounterService
    {
        private readonly IProjectCounterRepo _projectCounterRepo;

        public ProjectCounterService(IProjectCounterRepo ProjectCounterRepo)
        {
            _projectCounterRepo = ProjectCounterRepo;
        }

        public CreateCounterRequest.Response CreateEventsAndShops(CreateCounterRequest model)
        {
            return _projectCounterRepo.CreateEventsAndShops(model);
        }

        public List<ProjectCounterMappingModel.ListData> GetListsProjectCounterMapping(ProjectCounterMappingModel.FilterData filter)
        {
            List<ProjectCounterMappingModel.ListData> resp = _projectCounterRepo.GetListsProjectCounterMapping(filter);
            return resp;
        }

        public async Task<GetdataEditProjectCounter.ProjectCounterDetailVm?> GetProjectCounterDetailAsync(int id)
        {
            var resp = await _projectCounterRepo.GetProjectCounterDetailAsync(id);
            return resp;
        }

        public UpdateCounterBankRequest.Response UpdateCounterBank(UpdateCounterBankRequest dto)
        {
            return _projectCounterRepo.UpdateCounterBank(dto);
        }

        public Task<BasicResponse> UpdateCounterInspectAsync(UpdateCounterInspectDto dto) => _projectCounterRepo.UpdateCounterInspectAsync(dto);
    }
}
