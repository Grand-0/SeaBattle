using System.Security.Principal;
using System.Security.Claims;

namespace API.Helpers
{
    public static class TokenHelper
    {
        public static string GetClaim (IIdentity identity, string claimeName)
        {
            ClaimsIdentity claims = identity as ClaimsIdentity;
            return claims.FindFirst(claimeName).Value;
        }
    }
}
