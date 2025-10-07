using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Respositories;
using static Project.CSS.Revise.Web.Models.Pages.Projectandunitfloorplan.ProjectandunitfloorplanModel;

namespace Project.CSS.Revise.Web.Service
{
    public interface IProjectandunitfloorplanService
    {
        public List<ListProjectFloorplan> GetlistProjectFloorPlan(ListProjectFloorplan model);
        public List<ListUnit> GetlistUnit(ListUnit model);
        Task<bool> SaveMappingAsync(SaveMappingFloorplanModel model, int userId, CancellationToken ct = default);
        public List<ListFloorPlanByUnit> GetFloorPlansByUnit(Guid unitId);
        bool DeactivateUnitFloorPlan(Guid id, int userId);
        Task<UploadResult> UploadFloorplansAsync(UploadFileProjectFloorPlan req, int userId, CancellationToken ct = default);
        Task<BasicResult> UpdateFloorplanAsync(UpdateFloorplanRequest req, int userId, CancellationToken ct = default);
        Task<BasicResult> DeleteFloorplanAsync(Guid floorPlanId, int userId, CancellationToken ct = default);
    }
    public class ProjectandunitfloorplanService : IProjectandunitfloorplanService
    {
        private readonly IProjectandunitfloorplanRepo _ProjectandunitfloorplanRepo;

        public ProjectandunitfloorplanService(IProjectandunitfloorplanRepo ProjectandunitfloorplanRepo)
        {
            _ProjectandunitfloorplanRepo = ProjectandunitfloorplanRepo;
        }

        public bool DeactivateUnitFloorPlan(Guid id, int userId)
        {
            var resp = _ProjectandunitfloorplanRepo.DeactivateUnitFloorPlan(id , userId);
            return resp;
        }

        public async Task<BasicResult> DeleteFloorplanAsync(Guid floorPlanId, int userId, CancellationToken ct = default)
        {
            return await _ProjectandunitfloorplanRepo.DeleteFloorplanAsync(floorPlanId, userId, ct);
        }

        public List<ListFloorPlanByUnit> GetFloorPlansByUnit(Guid unitId)
        {
            List<ListFloorPlanByUnit> resp = _ProjectandunitfloorplanRepo.GetFloorPlansByUnit(unitId);
            return resp;
        }

        public List<ListProjectFloorplan> GetlistProjectFloorPlan(ListProjectFloorplan model)
        {
            List<ListProjectFloorplan> resp = _ProjectandunitfloorplanRepo.GetlistProjectFloorPlan(model);
            return resp;
        }

        public List<ListUnit> GetlistUnit(ListUnit model)
        {
            List<ListUnit> resp = _ProjectandunitfloorplanRepo.GetlistUnit(model);
            return resp;
        }

        public async Task<bool> SaveMappingAsync(SaveMappingFloorplanModel model, int userId, CancellationToken ct = default)
        {
            return await _ProjectandunitfloorplanRepo.SaveMappingAsync(model, userId, ct);
        }

        public async Task<BasicResult> UpdateFloorplanAsync(UpdateFloorplanRequest req, int userId, CancellationToken ct = default)
        {
            return await _ProjectandunitfloorplanRepo.UpdateFloorplanAsync(req, userId, ct);
        }

        public async Task<UploadResult> UploadFloorplansAsync(UploadFileProjectFloorPlan req, int userId, CancellationToken ct = default)
        {
            return await _ProjectandunitfloorplanRepo.UploadFloorplansAsync(req, userId, ct);
        }

    }
}
