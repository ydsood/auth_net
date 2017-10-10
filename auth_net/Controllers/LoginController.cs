using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using auth_net.DAO;
using auth_net.Model;
using System.Security.Authentication;

namespace auth_net.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult Login([FromBody]UserLoginModel userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.UserName) || string.IsNullOrEmpty(userLogin.Password))
                return Json(new { Error = "Missing parameters" });
            User user = UserRepository.Get().GetUser(userLogin.UserName);
            if (user == null)
            {
                NotFound($"Could not find username {userLogin.UserName}");
            }
            return Ok(Util.GenerateJWT(user));
        }

        [HttpGet]
        public IActionResult LoginForm()
        {
            return Json(new { Username = "Type Username here", Password = "Type Password here" });
        }

        public class UserLoginModel
        {
            public string UserName;
            public string Password;
        }
    }
}