using auth_net.Model;
using auth_net.DAO.Common;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace auth_net.Controllers
{
    public class Util
    {
        public static string GenerateJWT(User user)
        {
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //TODO : Get this stuff out from here into config or certificates
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JustPuttingInSomethingForNow"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

           
            var token = new JwtSecurityToken(
              issuer: "http://localhost:50919",
              audience: "http://localhost:50919",
              claims: claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);
            //TODO : Start returning token.ValidTo as expiration time
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool PasswordsMatch(User retrievedUser, string password)
        {
            return MongoHelper<User>.PasswordsMatch(retrievedUser.Password, password);
        }
    }
}
