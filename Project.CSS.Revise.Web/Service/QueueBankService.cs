using Project.CSS.Revise.Web.Models.Pages.QueueBank;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IQueueBankService
    {
        public List<ListUnitForRegisterBankModel> GetListUnitForRegisterBank(string ProjectID);
        public string GetMessageAppointmentInspect(RegisterLog criteria);
        public RegisterLog SaveRegisterLog(RegisterLog model);
        public RegisterLog GetRegisterLogInfo(RegisterLog criteria, string UserID, string Password);
        public void SaveCustomerSubmit_FINPlus(LoanModel model);
        public string RemoveRegisterLog(int id);
        public string GetProjectIDRegisterLog(int id);
        public void SaveDeleteLoanBank_FINPlus(LoanBankModel model);
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

        public string GetMessageAppointmentInspect(RegisterLog criteria)
        {
            return _QueueBankRepo.GetMessageAppointmentInspect(criteria);
        }

        public RegisterLog SaveRegisterLog(RegisterLog model)
        {
            return _QueueBankRepo.SaveRegisterLog(model);
        }

        public RegisterLog GetRegisterLogInfo(RegisterLog criteria, string UserID, string Password)
        {
            return _QueueBankRepo.GetRegisterLogInfo(criteria , UserID , Password);
        }

        public void SaveCustomerSubmit_FINPlus(LoanModel model)
        {
             _QueueBankRepo.SaveCustomerSubmit_FINPlus(model);
        }

        public string RemoveRegisterLog(int id)
        {
            return _QueueBankRepo.RemoveRegisterLog(id);
        }

        public string GetProjectIDRegisterLog(int id)
        {
            return _QueueBankRepo.GetProjectIDRegisterLog(id);
        }

        public void SaveDeleteLoanBank_FINPlus(LoanBankModel model)
        {
            _QueueBankRepo.SaveDeleteLoanBank_FINPlus(model);
        }
    }
}
