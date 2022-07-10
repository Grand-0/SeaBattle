using API.Helpers;
using API.Resources;
using BusinessLayer.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MenuHub : Hub<IMenuHub>
    {
        private UserStore<string> _store { get; }
        private IUserService _userService { get; }
        private ILogger<MenuHub> _logger { get; set; }

        public MenuHub(UserStore<string> store, IUserService userService, ILogger<MenuHub> logger)
        {
            _store = store;
            _userService = userService;
            _logger = logger;
        }

        [HubMethodName("GetOnline")]
        public async Task GetOnlineUsers()
        {
            var allOnline = _store.GetOnlineUsers();
            string login = TokenHelper.GetClaim(Context.User.Identity, "Name");
            var online = allOnline.FindAll(l => l != login);
            await Clients.Caller.ShowOnlineUsers(online);
        }

        public async Task MakeRequest(string sender, string recipient)
        {
            var records = _store.GetUserRecords(recipient);
            var connectionId = records["ConnectionId"];

            await Clients.Clients(connectionId).MakeRequestWindow(sender, recipient);
        }

        public async Task MoveToSession(string firstUserLogin, string secondUserLogin, string sessionId)
        {
            var firstUserConnectionId = _store.GetUserRecords(firstUserLogin)["ConnectionId"];
            var secondUserConnectionId = _store.GetUserRecords(secondUserLogin)["ConnectionId"];

            await Clients.Users(firstUserConnectionId, secondUserConnectionId).MoveToSession(sessionId);
        }

        public override Task OnConnectedAsync()
        {
            string login = TokenHelper.GetClaim(Context.User.Identity, "Name");
            //after change ConnectionId - UserIdenfication
            _store.AddRecord(login, "ConnectionId", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string login = TokenHelper.GetClaim(Context.User.Identity, "Name");

            _store.RemoveRecord(login);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
