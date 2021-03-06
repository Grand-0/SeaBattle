using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Hubs
{
    public interface IMenuHub
    {
        Task ShowOnlineUsers(List<string> users);
        Task MakeRequestWindow(string sender, string recipient);
        Task MoveToSession(string sessionId);
    }
}
