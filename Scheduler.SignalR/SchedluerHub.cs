using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Scheduler.SignalR
{
    public class SchedluerHub:Hub
    {
        private const string SocketGroup = "defaultUsers";

        private readonly SchedluerExtHub<SchedluerHub> _schedluerExtHub;

        public SchedluerHub(SchedluerExtHub<SchedluerHub> schedluerExtHub)
        {
            _schedluerExtHub = schedluerExtHub;
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
