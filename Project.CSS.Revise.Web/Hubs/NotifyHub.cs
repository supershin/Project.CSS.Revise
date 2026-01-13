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

        // ✅ เพิ่มเมธอดนี้
        public async Task StopCallStaff(RegisterCallStaffCounter payload)
        {
            // ส่งให้ทุก client ไปซ่อนปุ่ม + sync UI
            await Clients.All.SendAsync("stopCallStaff", payload);

            // (ถ้าต้องการ) สั่ง refresh event ด้วย
            await Clients.All.SendAsync("notifyCounter");
        }
    }
}
