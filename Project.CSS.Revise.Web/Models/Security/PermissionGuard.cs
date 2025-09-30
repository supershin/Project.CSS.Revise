namespace Project.CSS.Revise.Web.Models.Security
{
    public class PermissionGuard
    {
        public enum PermissionAction { View, Add, Update, Delete, Download }

        public class PermissionResult
        {
            public bool View { get; set; }
            public bool Add { get; set; }
            public bool Update { get; set; }
            public bool Delete { get; set; }
            public bool Download { get; set; }
            public bool HasAny => View || Add || Update || Delete || Download;
        }

        public interface IPermissionGuard
        {
            PermissionResult GetPermissions(int qcTypeId, int menuId, int departmentId, int roleId);
            bool Has(int qcTypeId, int menuId, int departmentId, int roleId, PermissionAction action);
        }
    }
}
