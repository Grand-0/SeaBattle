using API.Models;
using Microsoft.Extensions.Configuration;
using System;
using Cryptographer = BCrypt.Net.BCrypt;

namespace API.HashProtection
{
    public class BCryptHashService : IHashService
    {
        private IConfiguration _config { get; }

        public BCryptHashService(IConfiguration config)
        {
            _config = config;
        }

        public Hash HashPassword(string password)
        {
            var individualSalt = Guid.NewGuid();
            string globalSalt = _config["GlobalPasswordSalt"];
            string fullPassword = password + globalSalt + individualSalt.ToString();
            string hashPassword = Cryptographer.HashPassword(fullPassword, 15);

            return new Hash { HashResult = hashPassword, UniqueGuid = individualSalt };
        }

        public bool isHashEquals(string password, Guid individualSalt, string userHashPassword)
        {
            string globalSalt = _config["GlobalPasswordSalt"];
            string fullPassword = password + globalSalt + individualSalt.ToString();

            if (!Cryptographer.Verify(fullPassword, userHashPassword))
            {
                throw new UnauthorizedAccessException("Неверный пароль!");
            }

            return true;
        }
    }
}
