using Project.CSS.Revise.Web.Models.Pages.UserBank;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IUserBankService
    {
        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter);
    }
    public class UserBankService : IUserBankService
    {
        private readonly IUserBankRepo _userBankRepo;

        public UserBankService(IUserBankRepo userBankRepo)
        {
            _userBankRepo = userBankRepo;
        }

        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter)
        {
            List<GetlistUserBank.ListData> resp = _userBankRepo.GetListUserBank(filter);
            return resp;
        }
    }
}
