using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using HeroDatingApp.Entities;
using HeroDatingApp.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HeroDatingApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>()  // list of claims, for example username (password)..
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)

            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);   

            var tokenDescriptor = new SecurityTokenDescriptor  // Contains info for this token
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), // valid for a week
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor); // Creates token with relevant description

            return tokenHandler.WriteToken(token);
        }
    }
}