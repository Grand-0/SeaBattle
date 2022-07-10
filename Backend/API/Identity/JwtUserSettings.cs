namespace API.Identity
{
    public static class JwtUserSettings
    {
        public static readonly string Issuer = "Sea_Battle_Authenticate_Service";
        public static readonly int LifeTime = 1;
        public static readonly string Audience = "Sea_Battle_API_Client";
    }
}
