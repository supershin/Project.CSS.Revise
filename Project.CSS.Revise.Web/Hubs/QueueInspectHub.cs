using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Project.CSS.Revise.Web.Hubs
{
    [Authorize]
    public class QueueInspectHub : Hub
    {
        public Task JoinProject(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId)) return Task.CompletedTask;
            return Groups.AddToGroupAsync(Context.ConnectionId, $"queueinspect:project:{projectId}");
        }

        public Task LeaveProject(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId)) return Task.CompletedTask;
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"queueinspect:project:{projectId}");
        }
    }
}
