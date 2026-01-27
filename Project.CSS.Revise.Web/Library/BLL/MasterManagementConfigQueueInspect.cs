using Project.CSS.Revise.Web.Library.DAL;
using Project.CSS.Revise.Web.Models.Pages.QueueInspect;

namespace Project.CSS.Revise.Web.Library.BLL
{
    public class MasterManagementConfigQueueInspect
    {
        private readonly MasterManagementProviderQueueInspect _provider;

        public MasterManagementConfigQueueInspect(MasterManagementProviderQueueInspect provider)
        {
            _provider = provider;
        }

        public QueueInspectModel.ListModel sp_GetQueueInspect(QueueInspectModel.FiltersModel EN)
        {
            return _provider.sp_GetQueueInspect(EN);
        }
    }
}
