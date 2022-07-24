using DataAcessLayer.Repositories.UserRepository;
using UserRepo = DataAcessLayer.Repositories.UserRepository.UserRepository;
using DataAcessLayer.Repositories.NationRepository;
using NationRepo = DataAcessLayer.Repositories.NationRepository.NationRepository;

namespace DataAcessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private INationRepository _nationRepository;

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

        public INationRepository NationRepository
        {
            get
            {
                if(_nationRepository == null)
                {
                    _nationRepository = new NationRepo(_connectionString);
                }

                return _nationRepository;
            }
        }
    }
}
