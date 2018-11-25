using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Scheduler.SignalR
{
    public class SchedulerExtHub<THub> where THub : Hub
    {
        public readonly IHubContext<THub> HubContext;

        public SchedulerExtHub(IHubContext<THub> hubContext)
        {
            HubContext = hubContext;
        }

        public async Task SendMessage(string message)
        {
            await HubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
