namespace API.Models.Users
{
    public class UserResponse : UserBase
    {
        public string Logo { get; set; }
        public int Battles { get; set; }
        public int WinBattles { get; set; }
        public int WinRate { get; set; }
    }
}
