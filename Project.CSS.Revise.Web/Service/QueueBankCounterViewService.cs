using Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IQueueBankCounterViewService
    {
        public List<ListCounterModel.ListCounterItem> GetListsCounterQueueBank(ListCounterModel.Filter filter);
    }
    public class QueueBankCounterViewService : IQueueBankCounterViewService
    {
        private readonly IQueueBankCounterViewRepo _QueueBankCounterViewRepo;

        public QueueBankCounterViewService(IQueueBankCounterViewRepo QueueBankCounterViewRepo)
        {
            _QueueBankCounterViewRepo = QueueBankCounterViewRepo;
        }

        public List<ListCounterModel.ListCounterItem> GetListsCounterQueueBank(ListCounterModel.Filter filter)
        {
            return _QueueBankCounterViewRepo.GetListsCounterQueueBank(filter);
        }
    }
}
