using System;

namespace BusinessLayer.Models
{
    public class ReducedUser
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Guid IndividualSalt { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
    }
}
