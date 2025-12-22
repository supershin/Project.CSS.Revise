using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Project.CSS.Revise.Web.Hubs
{
    [Authorize] // ใช้ cookie auth ได้
    public class QueueBankHub : Hub
    {
        public Task JoinProject(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId)) return Task.CompletedTask;
            return Groups.AddToGroupAsync(Context.ConnectionId, $"queuebank:project:{projectId}");
        }

        public Task LeaveProject(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId)) return Task.CompletedTask;
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"queuebank:project:{projectId}");
        }
    }
}
