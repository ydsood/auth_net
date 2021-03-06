﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using auth_net.DAO;
using auth_net.Model;
using System.Security.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using auth_net.Filters;

namespace auth_net.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    [ValidateModel]
    public class LoginController : Controller
    {
        private TokenOptions _tokenOptions;

        public LoginController(IOptions<TokenOptions> tokenOptionsAccessor)
        {
            _tokenOptions = tokenOptionsAccessor.Value;
        }


        [HttpPost]
        public IActionResult Login([FromBody]UserLoginModel userLogin)
        {
            User user = UserRepository.Get().GetUser(userLogin.UserName);
            if (user == null)
            {
                NotFound($"Could not find username {userLogin.UserName}");
            }

            if(!Util.PasswordsMatch(user, userLogin.Password))
            {
                throw new AuthenticationException("Passwords don't match");
            }
            return Ok(Util.GenerateJWT(user, _tokenOptions));
        }

        [HttpGet]
        public IActionResult LoginForm()
        {
            return Json(new { Username = "Type Username here", Password = "Type Password here" });
        }
    }
}