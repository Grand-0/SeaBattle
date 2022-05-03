using BusinessLayer.Models;
using DataAcessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.UserService
{
    public class UserService : IUserService
    {
        private IUnitOfWork _databaseContext;
        public UserService(IUnitOfWork unitOfWork)
        {
            _databaseContext = unitOfWork;
        }

        public void ChangePassword(int id, byte[] passwordHash, Guid individualSalt)
        {
            try
            {
                _databaseContext.UserRepository.ChangePassword(id, passwordHash, individualSalt);
            }
            catch (Exception)
            {

            }
        }

        public ReducedUser GetUserById(int id)
        {
            try
            {
                var t = _databaseContext.UserRepository.GetUserById(id);
            }
            catch(Exception)
            {

            }
        }

        public UserWithStatistic GetUserWithStatisticById(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExist(string login, string email)
        {
            throw new NotImplementedException();
        }
    }
}
