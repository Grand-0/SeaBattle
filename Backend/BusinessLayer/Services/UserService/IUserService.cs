using BusinessLayer.Models;
using System;

namespace BusinessLayer.Services.UserService
{
    public interface IUserService
    {
        bool IsUserExist(string login, string email);
        ReducedUser GetUserById(int id);
        UserWithStatistic GetUserWithStatisticById(int id);
        void ChangePassword(int id, string passwordHash, Guid individualSalt);
        void CreateUser(ReducedUser user);
        ReducedUser GetUserByLogin(string login);
        int GetUserIdByLogin(string login);
        void UpdateUser(UpdatedUser user);
        void UpdateUserEmail(int id, string email);
        void UpdateUserLogin(int id, string login);
        void UpdateUserLogo(int id, string pathToLogo);
        void UpdateUserLoginAndEmail(int id, string login, string email);
        void UpdateUserLoginAndLogo(int id, string login, string pathToLogo);
        void UpdateUserEmailAndLogo(int id, string email, string pathToLogo);
    }
}
