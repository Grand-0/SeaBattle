namespace API.Models.Users
{
    public class FullUser : UserBase
    {
        public string LogoPath { get; set; }
        public int Battles { get; set; }
        public int WinBattles { get; set; }
        public int WinRate { get; set; }
    }
}
