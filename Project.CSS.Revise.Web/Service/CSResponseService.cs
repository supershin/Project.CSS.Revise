using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface ICSResponseService
    {
        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter);
        public UpdateInsertCsmapping UpdateorInsertCsmapping(UpdateInsertCsmapping model);
        Task<List<GetlistCountByCS>> GetListCountByCSAsync();
    }
    public class CSResponseService : ICSResponseService
    {
        private readonly ICSResponseRepo _cSResponseRepo;
        public CSResponseService(ICSResponseRepo csResponseRepo)
        {
            _cSResponseRepo = csResponseRepo;
        }

        public Task<List<GetlistCountByCS>> GetListCountByCSAsync()
        {
            return _cSResponseRepo.GetListCountByCSAsync();
        }

        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter)
        {
            return _cSResponseRepo.GetlistUnitCSResponse(filter);
        }

        public UpdateInsertCsmapping UpdateorInsertCsmapping(UpdateInsertCsmapping model)
        {
            return _cSResponseRepo.UpdateorInsertCsmapping(model);
        }
    }
}
