using BlogSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogSystem.Enums
{
    public static class JWT
    {
        public static string generateToken(UserType role,string key,string issuer,int liveTime,string userName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role.ToString()),
            };

            var Sectoken = new JwtSecurityToken(issuer,
              issuer,
              claims,
              expires: DateTime.Now.AddMinutes(liveTime),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return token;
        }
    }
}
