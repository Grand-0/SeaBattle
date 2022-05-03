using DataAcessLayer.Repositories.UserRepository;
using UserRepo = DataAcessLayer.Repositories.UserRepository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private string _connectionString;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepo(_connectionString);
                }

                return _userRepository;
            }
        }
    }
}
