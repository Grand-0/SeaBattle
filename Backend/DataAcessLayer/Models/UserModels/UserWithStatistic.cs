using System;

namespace DataAcessLayer.Models.UserModels
{
    public class UserWithStatistic
    {
        public int? UserId { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public Guid PasswordSalt { get; set; }
        public string Email { get; set; }
        public string UserLogo { get; set; }
        public int Battles { get; set; }
        public int WinBattles { get; set; }
        public float WinRate { get; set; }
    }
}
