using API.Models;

namespace API.Identity
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
