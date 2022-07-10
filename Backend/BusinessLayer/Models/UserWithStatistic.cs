using System;

namespace BusinessLayer.Models
{
    public class UserWithStatistic
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Guid IndividualSalt { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public int Battles { get; set; }
        public int WinBattles { get; set; }
        public int WinRate { get; set; }
    }
}
