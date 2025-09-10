using Project.CSS.Revise.Web.Library.DAL;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;

public class MasterManagementConfigProject
{
    private readonly MasterManagementProviderProject _provider;

    public MasterManagementConfigProject(MasterManagementProviderProject provider)
    {
        _provider = provider;
    }

    public List<RollingPlanSummaryModel> sp_GetProjecTargetRollingPlanList_GetListTargetRollingPlan(RollingPlanSummaryModel en)
    {
        return _provider.sp_GetProjecTargetRollingPlanList_GetListTargetRollingPlan(en);
    }
}
