using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Models.Pages.UserAndPermission;
using Project.CSS.Revise.Web.Respositories;
using System.Threading.Tasks;
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
        public bool IUDProjectUserMapping(UserAndPermissionModel.IUDProjectUserMapping model, int currentUserId);
        public List<UserAndPermissionModel.PermissionMatrixRow> GetPermissionMatrix(int qcTypeId = 10);
        bool SaveRolePermissions(UserAndPermissionModel.SaveRolePermissionRequest req, int currentUserId);
        List<UserAndPermissionModel.PermissionMatrixRow> GetPermissionMatrixFor(int qcTypeId, int departmentId, int roleId);
        int DepartmentCreate(string name, int currentUserId);
        bool DepartmentUpdate(int id, string name, int currentUserId);
        object DepartmentSoftDelete(int id, int currentUserId); // returns {success, message?}
        int RoleCreate(string name, int currentUserId, int qcTypeId = 10);
        bool RoleUpdate(int id, string name, int currentUserId, int qcTypeId = 10);
        object RoleSoftDelete(int id, int currentUserId, int qcTypeId = 10);
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

        public List<PermissionMatrixRow> GetPermissionMatrix(int qcTypeId = 10)
        {
            return _userAndPermissionRepo.GetPermissionMatrix(qcTypeId);
        }

        public int InsertUser(UserAndPermissionModel.CreateUserRequest model, int currentUserId)
        {
            return _userAndPermissionRepo.InsertUser(model, currentUserId);
        }

        public bool IUDProjectUserMapping(IUDProjectUserMapping model, int currentUserId)
        {
            return _userAndPermissionRepo.IUDProjectUserMapping(model, currentUserId);
        }

        public bool UpdateUser(UserAndPermissionModel.UpdateUserRequest model, int currentUserId)
        {
            return _userAndPermissionRepo.UpdateUser(model, currentUserId);
        }

        public bool SaveRolePermissions(UserAndPermissionModel.SaveRolePermissionRequest req, int currentUserId)
        {
            return _userAndPermissionRepo.SaveRolePermissions(req, currentUserId);
        }

        public List<PermissionMatrixRow> GetPermissionMatrixFor(int qcTypeId, int departmentId, int roleId)
        {
            return _userAndPermissionRepo.GetPermissionMatrixFor(qcTypeId, departmentId, roleId);
        }

        public int DepartmentCreate(string name, int currentUserId)
        {
            return _userAndPermissionRepo.DepartmentCreate(name, currentUserId);
        }

        public bool DepartmentUpdate(int id, string name, int currentUserId)
        {
            return _userAndPermissionRepo.DepartmentUpdate(id, name, currentUserId);
        }

        public object DepartmentSoftDelete(int id, int currentUserId)
        {
            return _userAndPermissionRepo.DepartmentSoftDelete(id, currentUserId);
        }

        public int RoleCreate(string name, int currentUserId, int qcTypeId = 10)
            => _userAndPermissionRepo.RoleCreate(name, currentUserId, qcTypeId);

        public bool RoleUpdate(int id, string name, int currentUserId, int qcTypeId = 10)
            => _userAndPermissionRepo.RoleUpdate(id, name, currentUserId, qcTypeId);

        public object RoleSoftDelete(int id, int currentUserId, int qcTypeId = 10)
            => _userAndPermissionRepo.RoleSoftDelete(id, currentUserId, qcTypeId);
    }
}
