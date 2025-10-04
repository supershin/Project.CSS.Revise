using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture;
using Project.CSS.Revise.Web.Respositories;
using static Project.CSS.Revise.Web.Models.Pages.FurnitureAndUnitFurniture.FurnitureAndUnitFurnitureModel;

namespace Project.CSS.Revise.Web.Service
{
    public interface IFurnitureAndUnitFurnitureService
    {
        public List<FurnitureAndUnitFurnitureModel.UnitFurnitureListItem> GetlistUnitFurniture(FurnitureAndUnitFurnitureModel.UnitFurnitureFilter filter);
        Task<bool> SaveFurnitureProjectMappingAsync(SaveFurnitureProjectMappingRequest req, int userId, CancellationToken ct = default);
        public UnitFurnitureModel? GetUnitFurniture(Guid unitId);
        Task<bool> UpdateFurnitureProjectMappingAsync(UpdateFurnitureProjectMappingRequest req, int userId, CancellationToken ct = default);
        Task<bool> CreateAsync(string name, int userId, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, string name, int userId, CancellationToken ct = default);
        Task<(bool ok, string message)> DeleteAsync(int id, int userId, CancellationToken ct = default);
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

        public UnitFurnitureModel? GetUnitFurniture(Guid unitId)
        {
            return _furnitureAndUnitFurnitureRepo.GetUnitFurniture(unitId);
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

        public async Task<bool> UpdateFurnitureProjectMappingAsync(UpdateFurnitureProjectMappingRequest req,int userId, CancellationToken ct = default)
        {
            try
            {
                return await _furnitureAndUnitFurnitureRepo.UpdateFurnitureProjectMappingAsync(req, userId, ct);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update furniture project mapping", ex);
            }
        }

        public async Task<bool> CreateAsync(string name, int userId, CancellationToken ct = default)
        {
            return await _furnitureAndUnitFurnitureRepo.CreateAsync(name, userId, ct);
        }

        public async Task<bool> UpdateAsync(int id, string name, int userId, CancellationToken ct = default)
        {
            return await _furnitureAndUnitFurnitureRepo.UpdateAsync(id, name, userId, ct);
        }

        public async Task<(bool ok, string message)> DeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            return await _furnitureAndUnitFurnitureRepo.DeleteAsync(id, userId, ct);
        }
    }
}
