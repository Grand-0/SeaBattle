using BusinessLayer.Models;
using System;

namespace BusinessLayer.Services.UserService
{
    public interface IUserService
    {
        bool IsUserExist(string login, string email);
        ReducedUser GetUserById(int id);
        UserWithStatistic GetUserWithStatisticById(int id);
        void ChangePassword(int id, byte[] passwordHash, Guid individualSalt);
    }
}
