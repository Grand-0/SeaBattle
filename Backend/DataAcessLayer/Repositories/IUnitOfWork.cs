using DataAcessLayer.Repositories.NationRepository;
using DataAcessLayer.Repositories.UserRepository;

namespace DataAcessLayer.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        INationRepository NationRepository { get; }
    }
}
