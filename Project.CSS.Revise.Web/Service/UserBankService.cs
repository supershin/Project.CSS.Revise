using Microsoft.Extensions.Logging;
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
        public List<GetlistUserBankInTeam> GetlistUserBankInTeam(GetlistUserBankInTeam model);
        Task<int> InsertUserBankAsync(UserBankEditModel model);
        public bool MoveUserbankToTeam(int UserBankID, int ParrentID, string UserID);
        public bool LeavUserbankFromTeam(int UserBankID, string UserID);
        Task<int> UpdateUserBankAsync(UserBankEditModel model);
        Task<bool> SoftDeleteUserBankAsync(int id, string updatedBy);

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

        public List<GetlistUserBankInTeam> GetlistUserBankInTeam(GetlistUserBankInTeam model)
        {
            List<GetlistUserBankInTeam> resp = _userBankRepo.GetlistUserBankInTeam(model);
            return resp;
        }

        public Task<int> InsertUserBankAsync(UserBankEditModel model) => _userBankRepo.InsertUserBankAsync(model);

        public bool MoveUserbankToTeam(int UserBankID, int ParrentID, string UserID)
        {
            return _userBankRepo.MoveUserbankToTeam(UserBankID, ParrentID, UserID);
        }

        public bool LeavUserbankFromTeam(int UserBankID, string UserID)
        {
            return _userBankRepo.LeavUserbankFromTeam(UserBankID, UserID);
        }

        public Task<int> UpdateUserBankAsync(UserBankEditModel model) => _userBankRepo.UpdateUserBankAsync(model);

        public async Task<bool> SoftDeleteUserBankAsync(int id, string updatedBy)
        {
            return await _userBankRepo.SoftDeleteUserBankAsync(id, updatedBy);
        }

    }
}
