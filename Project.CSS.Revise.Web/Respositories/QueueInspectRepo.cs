using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IQueueInspectRepo
    {
        public string RemoveRegisterLog(int id);
    }
    public class QueueInspectRepo : IQueueInspectRepo
    {
        private readonly CSSContext _context;

        public QueueInspectRepo(CSSContext context)
        {
            _context = context;
        }

        public string RemoveRegisterLog(int id)
        {
            string? dep64 = User.FindFirst("DepartmentID")?.Value;
            string? rol64 = User.FindFirst("RoleID")?.Value;

            var item = _context.TR_RegisterLogs.FirstOrDefault(e => e.ID == id);

            if (item == null) 
            {
                return "NOT_FOUND";
            };            

            var projectId = item.ProjectID ?? "";

            item.FlagActive = false;
            item.UpdateDate = DateTime.Now;

            _context.SaveChanges();

            return projectId;
        }


    }
}
