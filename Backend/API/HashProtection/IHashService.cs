using API.Models;
using System;

namespace API.HashProtection
{
    public interface IHashService
    {
        Hash HashPassword(string password);
        bool isHashEquals(string password, Guid individualSalt, byte[] userHashPassword);
    }
}
