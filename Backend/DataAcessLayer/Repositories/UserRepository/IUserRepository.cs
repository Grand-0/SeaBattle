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
        void ChangePassword(int id, string passwordHash, Guid individualSalt);
        void CreateUser(ReducedUser user);
        ReducedUser GetUserByLogin(string login);
        int? GetUserIdByLogin(string login);
        void UpdateFullUser(UpdatedFullUser user);
        void UpdateLoginAndEmail(int id, string login, string email);
        void UpdateLoginAndLogo(int id, string login, string pathToLogo);
        void UpdateEmailAndLogo(int id, string email, string pathToLogo);
        void UpdateLogin(int Id, string login);
        void UpdateEmail(int Id, string email);
        void UpdateLogo(int Id, string pathToLogo);
    }
}
