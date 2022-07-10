using API.Models.Users;

namespace API.Identity
{
    public interface IJwtGenerator
    {
        string CreateToken(UserBase user);
    }
}
