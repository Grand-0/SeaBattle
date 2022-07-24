using DataAcessLayer.Models.UserModels;
using System;
using System.Collections.Generic;

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
        void UpdateLoginAndEmail(int id, string login, string email);
        void UpdateLogin(int Id, string login);
        void UpdateEmail(int Id, string email);
        void UpdateLogo(int Id, string pathToLogo);
        List<UserProfile> GetUserProfiles(List<int> ids);
    }
}
