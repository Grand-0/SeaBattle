using API.Models;
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

		public string CreateToken(User user)
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var claims = new ClaimsIdentity();

			claims.AddClaim(new Claim("Name", user.Login));
			claims.AddClaim(new Claim("Identificator", user.Id.ToString()));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = credentials,
				Issuer = JwtUserSettings.Issuer
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
        }
    }
}
