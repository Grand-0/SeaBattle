using DataAcessLayer.Models.UserModels;
using System;

namespace DataAcessLayer.Repositories.UserRepository
{
    public interface IUserRepository
    {
        bool isUserExistLogin(string login);
        bool isUserExist(string email);
        ReducedUser GetUserById(int id);
        UserWithStatistic GetUserWithStatisticById(int id);
        void ChangePassword(int id, byte[] passwordHash, Guid individualSalt);
    }
}
