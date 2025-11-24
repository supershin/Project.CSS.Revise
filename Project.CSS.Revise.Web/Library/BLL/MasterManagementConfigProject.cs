using Project.CSS.Revise.Web.Library.DAL;
using Project.CSS.Revise.Web.Models.Pages.CSResponse;
using Project.CSS.Revise.Web.Models.Pages.ProjectAndTargetRolling;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;

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

    public List<RollingPlanTotalModel> sp_GetProjecTargetRollingPlanList_GetDataTotalTargetRollingPlan(RollingPlanTotalModel en)
    {
        return _provider.sp_GetProjecTargetRollingPlanList_GetDataTotalTargetRollingPlan(en);
    }

    public SPGetDataCSResponse.ListData sp_GetDataCSResponse(SPGetDataCSResponse.FilterData en)
    {
        return _provider.sp_GetDataCSResponse(en);
    }

    public List<ListDataRegisterTable> sp_GetQueueBank_RegisterTable(GetQueueBankModel en)
    {
        return _provider.sp_GetQueueBank_RegisterTable(en);
    }

    public List<ListSummeryRegister.ListSummeryRegisterType> sp_GetQueueBank_SummeryRegisterType(GetQueueBankModel en)
    {
        return _provider.sp_GetQueueBank_SummeryRegisterType(en);
    }

    public List<ListSummeryRegister.ListSummeryRegisterLoanType> sp_GetQueueBank_SummeryRegisterLoanType(GetQueueBankModel en)
    {
        return _provider.sp_GetQueueBank_SummeryRegisterLoanType(en);
    }

    public List<ListSummeryRegister.ListSummeryRegisterCareerType> sp_GetQueueBank_SummeryRegisterCareerType(GetQueueBankModel en)
    {
        return _provider.sp_GetQueueBank_SummeryRegisterCareerType(en);
    }

    public List<ListSummeryRegister.ListSummeryRegisterBank> sp_GetQueueBank_SummeryRegisterBank(GetQueueBankModel en)
    {
        return _provider.sp_GetQueueBank_SummeryRegisterBank(en);
    }
}
