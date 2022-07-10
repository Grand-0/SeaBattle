using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using UpdatedFullUserDAL = DataAcessLayer.Models.UserModels.UpdatedFullUser;
using DataAcessLayer.Repositories;
using System;
using ReducedUserDAL = DataAcessLayer.Models.UserModels.ReducedUser;

namespace BusinessLayer.Services.UserService
{
    public class UserService : IUserService
    {
        private IUnitOfWork _databaseContext;
        private IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _databaseContext = unitOfWork;
            _mapper = mapper;
        }

        public void ChangePassword(int id, string passwordHash, Guid individualSalt)
        {
            _databaseContext.UserRepository.ChangePassword(id, passwordHash, individualSalt);
        }

        public void CreateUser(ReducedUser user)
        {
            _databaseContext.UserRepository.CreateUser(_mapper.Map<ReducedUserDAL>(user));
        }

        public ReducedUser GetUserById(int id)
        {
            ReducedUser user = _mapper.Map<ReducedUser>(_databaseContext.UserRepository.GetUserById(id));
            return user;
        }

        public ReducedUser GetUserByLogin(string login)
        {
            ReducedUser user = _mapper.Map<ReducedUser>(_databaseContext.UserRepository.GetUserByLogin(login));

            if (user == null)
            {
                throw new NotFoundException("Not found User");
            }

            return user;
        }

        public UserWithStatistic GetUserWithStatisticById(int id)
        {
            UserWithStatistic user = _mapper.Map<UserWithStatistic>(_databaseContext.UserRepository.GetUserWithStatisticById(id));

            if (user == null)
            {
                throw new NotFoundException("Not found User");
            }

            return user;
        }

        public bool IsUserExist(string login, string email)
        {
            bool isUserExist = _databaseContext.UserRepository.isUserExist(email);

            if (isUserExist)
            {
                throw new Exception("This user exist!");
            }

            bool isLoginExist = _databaseContext.UserRepository.isUserExistLogin(login);

            if (isLoginExist)
            {
                throw new Exception("User with this login exist!");
            }

            return false;
        }

        public int GetUserIdByLogin(string login)
        {
            int? id = _databaseContext.UserRepository.GetUserIdByLogin(login);

            if (id == null)
            {
                throw new NotFoundException("User not exist!");
            }

            return (int)id;
        }

        public void UpdateUser(UpdatedUser user)
        {
            _databaseContext.UserRepository.UpdateFullUser(_mapper.Map<UpdatedFullUserDAL>(user));
        }

        public void UpdateUserEmail(int id, string email)
        {
            _databaseContext.UserRepository.UpdateEmail(id, email);
        }

        public void UpdateUserLogin(int id, string login)
        {
            _databaseContext.UserRepository.UpdateLogin(id, login);
        }

        public void UpdateUserLogo(int id, string pathToLogo)
        {
            _databaseContext.UserRepository.UpdateLogo(id, pathToLogo);
        }

        public void UpdateUserLoginAndEmail(int id, string login, string email)
        {
            _databaseContext.UserRepository.UpdateLoginAndEmail(id, login, email);
        }

        public void UpdateUserLoginAndLogo(int id, string login, string pathToLogo)
        {
            _databaseContext.UserRepository.UpdateLoginAndLogo(id, login, pathToLogo);
        }

        public void UpdateUserEmailAndLogo(int id, string email, string pathToLogo)
        {
            _databaseContext.UserRepository.UpdateEmailAndLogo(id, email, pathToLogo);
        }
    }
}
