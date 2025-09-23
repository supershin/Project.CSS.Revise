using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.UserAndPermission;
using Project.CSS.Revise.Web.Respositories;
using static Project.CSS.Revise.Web.Models.Pages.UserAndPermission.UserAndPermissionModel;

namespace Project.CSS.Revise.Web.Service
{
    public interface IUserAndPermissionService
    {
        List<UserAndPermissionModel.GetlistUser> GetlistUser(UserAndPermissionModel.FiltersGetlistUser filters);
        public int InsertUser(UserAndPermissionModel.CreateUserRequest model, int currentUserId);
        public bool UpdateUser(UserAndPermissionModel.UpdateUserRequest model, int currentUserId);
        public DuplicateCheckResult CheckDuplicate(string? email, string? userId, string? firstNameTh, string? lastNameTh, string? firstNameEn, string? lastNameEn, int? excludeId = null);
        public UserAndPermissionModel.UserDetail? GetDetailsUser(UserAndPermissionModel.FiltersGetlistUser filters);
        public List<UserAndPermissionModel.GetlistProjects> GetlistProjects(UserAndPermissionModel.FiltersGetlistUser filters);
    }
    public class UserAndPermissionService : IUserAndPermissionService
    {
        private readonly IUserAndPermissionRepo _userAndPermissionRepo;

        public UserAndPermissionService(IUserAndPermissionRepo IUserAndPermissionRepo)
        {
            _userAndPermissionRepo = IUserAndPermissionRepo;
        }

        public DuplicateCheckResult CheckDuplicate(string? email, string? userId, string? firstNameTh, string? lastNameTh, string? firstNameEn, string? lastNameEn, int? excludeId = null)
        {
            return _userAndPermissionRepo.CheckDuplicate(email, userId, firstNameTh, lastNameTh, firstNameEn, lastNameEn, excludeId);
        }

        public UserDetail? GetDetailsUser(FiltersGetlistUser filters)
        {
            return _userAndPermissionRepo.GetDetailsUser(filters);
        }

        public List<GetlistProjects> GetlistProjects(FiltersGetlistUser filters)
        {
            return _userAndPermissionRepo.GetlistProjects(filters);
        }

        public List<UserAndPermissionModel.GetlistUser> GetlistUser(UserAndPermissionModel.FiltersGetlistUser filters)
        {
            return _userAndPermissionRepo.GetlistUser(filters);
        }

        public int InsertUser(UserAndPermissionModel.CreateUserRequest model, int currentUserId)
        {
            return _userAndPermissionRepo.InsertUser(model, currentUserId);
        }

        public bool UpdateUser(UserAndPermissionModel.UpdateUserRequest model, int currentUserId)
        {
            return _userAndPermissionRepo.UpdateUser(model, currentUserId);
        }
    }
}
