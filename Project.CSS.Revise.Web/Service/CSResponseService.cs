using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface ICSResponseService
    {
        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter);
    }
    public class CSResponseService : ICSResponseService
    {
        private readonly ICSResponseRepo _cSResponseRepo;
        public CSResponseService(ICSResponseRepo csResponseRepo)
        {
            _cSResponseRepo = csResponseRepo;
        }
        public List<GetlistUnitCSResponseModel.ListData> GetlistUnitCSResponse(GetlistUnitCSResponseModel.FilterData filter)
        {
            return _cSResponseRepo.GetlistUnitCSResponse(filter);
        }
    }
}
