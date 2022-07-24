using API.Helpers;
using API.Models.Users;
using API.Resources;
using AutoMapper;
using BusinessLayer.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MenuHub : Hub<IMenuHub>
    {
        private UserStore<string> _store { get; }
        private IUserService _userService { get; }
        private IMapper _mapper { get; }
        private ILogger<MenuHub> _logger { get; set; }

        public MenuHub(UserStore<string> store, IUserService userService, IMapper mapper, ILogger<MenuHub> logger)
        {
            _store = store;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HubMethodName("GetOnline")]
        public async Task GetOnlineUsers()
        {
            string login = TokenHelper.GetClaim(Context.User.Identity, "Name");
            var allOnline = _store.GetOnlineUsersID(login);
            var profiles = _mapper.Map<List<UserProfile>>(_userService.GetUserProfiles(allOnline));
            await Clients.Caller.ShowOnlineUsers(profiles);
        }

        [HubMethodName("ShowOnline")]
        public async Task ShowOnlineUsers()
        {
            var allOnline = _store.GetOnlineUsersID();
            var profiles = _mapper.Map<List<UserProfile>>(_userService.GetUserProfiles(allOnline));
            await Clients.All.ShowOnlineUsers(profiles);
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
            string id = TokenHelper.GetClaim(Context.User.Identity, "UniqueId");
            //after change ConnectionId - UserIdenfication
            _store.AddRecord(login, "ConnectionId", Context.ConnectionId);
            _store.AddRecord(login, "UniqueId", id);

            ShowOnlineUsers().Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string login = TokenHelper.GetClaim(Context.User.Identity, "Name");
            _store.RemoveRecord(login);

            ShowOnlineUsers().Wait();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
