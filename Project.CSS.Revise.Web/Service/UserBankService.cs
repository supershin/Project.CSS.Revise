using Project.CSS.Revise.Web.Models.Pages.ProjectCounter;
using Project.CSS.Revise.Web.Models.Pages.UserBank;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IUserBankService
    {
        public List<CountUserByBankModel.ListData> GetListCountUserByBank();
        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter);
        Task<UserBankEditModel?> GetUserBankByIdAsync(int id);
    }
    public class UserBankService : IUserBankService
    {
        private readonly IUserBankRepo _userBankRepo;

        public UserBankService(IUserBankRepo userBankRepo)
        {
            _userBankRepo = userBankRepo;
        }

        public List<CountUserByBankModel.ListData> GetListCountUserByBank()
        {
            List<CountUserByBankModel.ListData> resp = _userBankRepo.GetListCountUserByBank();
            return resp;
        }

        public List<GetlistUserBank.ListData> GetListUserBank(GetlistUserBank.FilterData filter)
        {
            List<GetlistUserBank.ListData> resp = _userBankRepo.GetListUserBank(filter);
            return resp;
        }
        public async Task<UserBankEditModel?> GetUserBankByIdAsync(int id)
        {
            var resp = await _userBankRepo.GetUserBankByIdAsync(id);
            return resp;
        }


    }
}
