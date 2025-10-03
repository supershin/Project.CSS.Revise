using Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture;
using Project.CSS.Revise.Web.Respositories;
using static Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture.FurnitureAndUnitFurnitureModel;

namespace Project.CSS.Revise.Web.Service
{
    public interface IFurnitureAndUnitFurnitureService
    {
        public List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem> GetlistUnitFurniture(FurnitureAndUnitFurnitureModel.UnitFurnitureFilter filter);
        Task<bool> SaveFurnitureProjectMappingAsync(SaveFurnitureProjectMappingRequest req, int userId, CancellationToken ct = default);
    }
    public class FurnitureAndUnitFurnitureService : IFurnitureAndUnitFurnitureService
    {
        private readonly IFurnitureAndUnitFurnitureRepo _furnitureAndUnitFurnitureRepo;

        public FurnitureAndUnitFurnitureService(IFurnitureAndUnitFurnitureRepo furnitureAndUnitFurnitureRepo)
        {
            _furnitureAndUnitFurnitureRepo = furnitureAndUnitFurnitureRepo;
        }

        public List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem> GetlistUnitFurniture(FurnitureAndUnitFurnitureModel.UnitFurnitureFilter filter)
        {
            return _furnitureAndUnitFurnitureRepo.GetlistUnitFurniture(filter);
        }

        public async Task<bool> SaveFurnitureProjectMappingAsync(SaveFurnitureProjectMappingRequest req,int userId,CancellationToken ct = default)
        {
            try
            {
                var result = await _furnitureAndUnitFurnitureRepo.SaveFurnitureProjectMappingAsync(req, userId, ct);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save furniture project mapping", ex);
            }
        }

    }
}
