using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Hubs
{
    [Authorize]
    public class MenuHub : Hub<IMenuHub>
    {
        public async Task GetOnlineUsers()
        {
            await Clients.Caller.OnlineUsers(new List<string>());
        }
    }
}
