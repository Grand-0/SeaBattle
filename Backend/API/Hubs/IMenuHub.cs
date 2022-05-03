using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Hubs
{
    public interface IMenuHub
    {
        Task OnlineUsers(List<string> users);
    }
}
