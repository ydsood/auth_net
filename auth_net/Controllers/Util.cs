﻿using auth_net.Model;
using auth_net.DAO.Common;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace auth_net.Controllers
{
    public class Util
    {
        //TODO : Injecting IConfigurationRoot v/s passing it to the method that needs it.
        //I'm confused here.

        /*private readonly IConfigurationRoot _config;

        public Util(IConfigurationRoot config)
        {
            _config = config;
        }*/

        public static object GenerateJWT(User user, TokenOptions tokenOptions)
        {
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(
              issuer: tokenOptions.Issuer,
              audience: tokenOptions.Audience,
              claims: claims,
              expires: DateTime.Now.AddMinutes(tokenOptions.Expiration),
              signingCredentials: creds);

            return new { Token = new JwtSecurityTokenHandler().WriteToken(token), Expires = token.ValidTo };
        }

        public static bool PasswordsMatch(User retrievedUser, string password)
        {
            return MongoHelper<User>.PasswordsMatch(retrievedUser.Password, password);
        }
    }
}
