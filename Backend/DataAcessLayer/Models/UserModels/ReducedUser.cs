using System;

namespace DataAcessLayer.Models.UserModels
{
    public class ReducedUser
    {
        public int? UserId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Guid PasswordSalt { get; set; }
        public string Email { get; set; }
        public string UserLogo { get; set; }
    }
}
