using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IQueueInspectRepo
    {
        public string RemoveRegisterLog(int id);
    }
    public class QueueInspectRepo : IQueueInspectRepo
    {
        private readonly CSSContext _context;
        private readonly IHttpContextAccessor _http;

        public QueueInspectRepo(CSSContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public string RemoveRegisterLog(int id)
        {
            var user = _http.HttpContext?.User;
            string? LoginID = user?.FindFirst("LoginID")?.Value;
            string UserID = SecurityManager.DecodeFrom64(LoginID);

            var item = _context.TR_RegisterLogs.FirstOrDefault(e => e.ID == id);

            if (item == null) 
            {
                return "NOT_FOUND";
            };            

            var projectId = item.ProjectID ?? "";

            item.FlagActive = false;
            item.UpdateDate = DateTime.Now;
            item.UpdateBy = Commond.FormatExtension.Nulltoint(UserID);
            _context.SaveChanges();

            return projectId;
        }


    }
}
