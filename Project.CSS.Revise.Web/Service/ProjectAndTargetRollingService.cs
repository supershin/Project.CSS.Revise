using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IProjectAndTargetRollingService
    {
        public List<RollingPlanSummaryModel> GetListTargetRollingPlan(RollingPlanSummaryModel filter);
        public List<RollingPlanTotalModel> GetDataTotalTargetRollingPlan(RollingPlanTotalModel filter);
        public TargetRollingPlanInsertModel UpsertTargetRollingPlans(List<TargetRollingPlanInsertModel> plans);
    }
    public class ProjectAndTargetRollingService : IProjectAndTargetRollingService
    {
        private readonly IProjectAndTargetRollingRepo _ProjectAndTargetRollingRepoRepo;

        public ProjectAndTargetRollingService(IProjectAndTargetRollingRepo ProjectAndTargetRollingRepoRepo)
        {
            _ProjectAndTargetRollingRepoRepo = ProjectAndTargetRollingRepoRepo;
        }

        public List<RollingPlanSummaryModel> GetListTargetRollingPlan(RollingPlanSummaryModel filter)
        {
            List<RollingPlanSummaryModel> resp = _ProjectAndTargetRollingRepoRepo.GetListTargetRollingPlan(filter);
            return resp;
        }
        public List<RollingPlanTotalModel> GetDataTotalTargetRollingPlan(RollingPlanTotalModel filter)
        {
            List<RollingPlanTotalModel> resp = _ProjectAndTargetRollingRepoRepo.GetDataTotalTargetRollingPlan(filter);
            return resp;
        }

        public TargetRollingPlanInsertModel UpsertTargetRollingPlans(List<TargetRollingPlanInsertModel> plans)
        {
            return _ProjectAndTargetRollingRepoRepo.UpsertTargetRollingPlans(plans);
        }
    }
}
