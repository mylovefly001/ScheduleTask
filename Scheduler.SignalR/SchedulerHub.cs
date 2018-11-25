using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Scheduler.SignalR
{
    public class SchedulerHub : Hub
    {
        private const string SocketGroup = "defaultUsers";
        private readonly SchedulerExtHub<SchedulerHub> _schedulerExtHub;

        public SchedulerHub(SchedulerExtHub<SchedulerHub> schedulerExtHub)
        {
            _schedulerExtHub = schedulerExtHub;
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, SocketGroup);
            await base.OnConnectedAsync();
            //await Console.Out.WriteLineAsync($"服务端收到连接：{Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, SocketGroup);
            await base.OnDisconnectedAsync(exception);
            //await Console.Out.WriteLineAsync("服务端收到关闭连接");
        }
    }
}
