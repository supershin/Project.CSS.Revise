using Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IQueueBankCounterViewService
    {
        public List<ListCounterModel.ListCounterItem> GetListsCounterQueueBank(ListCounterModel.Filter filter);
        public List<ListCounterDetailsModel.ListCounterItem> GetListsCounterDetailsQueueBank(ListCounterDetailsModel.Filter filter);
        public List<dynamic> GetUnitDropdown(string projectId);
        public UpdateUnitRegisterModel.Message UpdateUnitRegister(UpdateUnitRegisterModel.Entity input);
        public UpdateUnitRegisterModel.Message RemoveUnitFromCounter(UpdateUnitRegisterModel.Entity input);
        public UpdateUnitRegisterModel.Message CheckoutBankCounter(BankCheckoutRequest input);
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

        public List<ListCounterDetailsModel.ListCounterItem> GetListsCounterDetailsQueueBank(ListCounterDetailsModel.Filter filter)
        {
            return _QueueBankCounterViewRepo.GetListsCounterDetailsQueueBank(filter);
        }

        public List<dynamic> GetUnitDropdown(string projectId)
        {
            return _QueueBankCounterViewRepo.GetUnitDropdown(projectId);
        }
        public UpdateUnitRegisterModel.Message UpdateUnitRegister(UpdateUnitRegisterModel.Entity input)
        {
            return _QueueBankCounterViewRepo.UpdateUnitRegister(input);
        }

        public UpdateUnitRegisterModel.Message RemoveUnitFromCounter(UpdateUnitRegisterModel.Entity input)
        {
            return _QueueBankCounterViewRepo.RemoveUnitFromCounter(input);
        }

        public UpdateUnitRegisterModel.Message CheckoutBankCounter(BankCheckoutRequest input)
        {
            return _QueueBankCounterViewRepo.CheckoutBankCounter(input);
        }
    }
}
