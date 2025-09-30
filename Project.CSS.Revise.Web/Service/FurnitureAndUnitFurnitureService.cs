using Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IFurnitureAndUnitFurnitureService
    {
        public List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem> GetlistUnitFurniture(FurnitureAndUnitFurnitureModel.UnitFurnitureFilter filter);
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
    }
}
