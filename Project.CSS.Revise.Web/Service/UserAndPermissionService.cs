using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Project.CSS.Revise.Web.Models.Pages.UserAndPermission;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IUserAndPermissionService
    {
        List<UserAndPermissionModel.GetlistUser> GetlistUser(UserAndPermissionModel.FiltersGetlistUser filters);
    }
    public class UserAndPermissionService : IUserAndPermissionService
    {
        private readonly IUserAndPermissionRepo _userAndPermissionRepo;

        public UserAndPermissionService(IUserAndPermissionRepo IUserAndPermissionRepo)
        {
            _userAndPermissionRepo = IUserAndPermissionRepo;
        }
        public List<UserAndPermissionModel.GetlistUser> GetlistUser(UserAndPermissionModel.FiltersGetlistUser filters)
        {
            return _userAndPermissionRepo.GetlistUser(filters);
        }
    }
}
