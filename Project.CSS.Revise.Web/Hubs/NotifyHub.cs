using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Project.CSS.Revise.Web.Models.Pages.QueueBank;

namespace Project.CSS.Revise.Web.Hubs
{
    [Authorize] // ใช้ cookie auth ได้
    public class NotifyHub : Hub
    {
        public async Task NotifyCounter()
        {
            await Clients.All.SendAsync("notifyCounter");
        }

        //public async Task SendCheckIn()
        //{
        //    await Clients.All.SendCoreAsync("SendCheckIn", Array.Empty<object>());
        //}

        //public async Task SendCallStaff(RegisterCallStaffCounter data)
        //{
        //    await Clients.All.SendCoreAsync("sendCallStaff", new object[] { data });
        //}
    }
}
