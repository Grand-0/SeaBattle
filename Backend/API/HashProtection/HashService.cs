using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;
using API.Models;

namespace API.HashProtection
{
    public class HashService : IHashService
    {
        private IConfiguration _config { get; }
        public HashService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Hash HashPassword(string password)
        {
            var individualSalt = Guid.NewGuid();
            var hash = MakeHashUserPassword(password, individualSalt);

            return new Hash { UniqueGuid = individualSalt, HashResult = hash };
        }

        public bool isHashEquals(string password, Guid individualSalt, byte[] userHashPassword)
        {
            var tempHash = MakeHashUserPassword(password, individualSalt);

            bool bEqual = false;

            if (tempHash.Length == userHashPassword.Length)
            {
                int i = 0;
                while ((i < tempHash.Length) && (tempHash[i] == userHashPassword[i]))
                {
                    i += 1;
                }
                if (i == tempHash.Length)
                {
                    bEqual = true;
                }
            }

            return bEqual;
        }

        private byte[] MakeHashUserPassword(string password, Guid individualSalt)
        {
            string globalSalt = _config["GlobalPasswordSalt"];
            string fullPassword = password + individualSalt.ToString() + globalSalt;

            var passwordToHash = ASCIIEncoding.ASCII.GetBytes(fullPassword);
            var hash = new MD5CryptoServiceProvider().ComputeHash(passwordToHash);
            return hash;
        }
    }
}
