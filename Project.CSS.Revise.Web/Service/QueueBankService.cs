using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IQueueBankService
    {
        public List<ListUnitForRegisterBankModel> GetListUnitForRegisterBank(string ProjectID);
    }

    public class QueueBankService : IQueueBankService
    {
        private readonly IQueueBankRepo _QueueBankRepo;

        public QueueBankService(IQueueBankRepo QueueBankRepo)
        {
            _QueueBankRepo = QueueBankRepo;
        }

        public List<ListUnitForRegisterBankModel> GetListUnitForRegisterBank(string ProjectID)
        {
            return _QueueBankRepo.GetListUnitForRegisterBank(ProjectID);
        }
    }
}
