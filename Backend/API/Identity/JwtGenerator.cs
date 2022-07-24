using API.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Identity
{
    public class JwtGenerator : IJwtGenerator
    {
		private readonly SymmetricSecurityKey _key;

		public JwtGenerator(IConfiguration config)
		{
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

		public string CreateToken(UserBase user)
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var claims = new ClaimsIdentity();

			claims.AddClaim(new Claim("UniqueId", user.Id.ToString()));
			claims.AddClaim(new Claim("Name", user.Login));
			claims.AddClaim(new Claim("Email", user.Email));
			
			var time = DateTime.UtcNow;

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				NotBefore = time,
				Expires = time.Add(TimeSpan.FromHours(JwtUserSettings.LifeTime)),
				SigningCredentials = credentials,
				Issuer = JwtUserSettings.Issuer,
				Audience = JwtUserSettings.Audience,
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
        }
    }
}
