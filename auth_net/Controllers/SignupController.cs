using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using auth_net.Model;
using auth_net.DAO;

namespace auth_net.Controllers
{
    [Produces("application/json")]
    [Route("api/Signup")]
    public class SignupController : Controller
    {
        [HttpPost]
        public IActionResult SignUp([FromBody]User user)
        {
            if (null == user ||string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.UserRole) || string.IsNullOrEmpty(user.FullName))
                return Json(new { Error = "Missing parameters" });
            var repo = UserRepository.Get();
            try
            {
                repo.GetUser(user.UserName);
            }
            catch(KeyNotFoundException)
            {
                User newUser = repo.AddUser(user);
                return Ok(new { Token = Util.GenerateJWT(newUser) });
            }

            return StatusCode(StatusCodes.Status409Conflict, "User already exists"); 
        }

        [HttpGet]
        public IActionResult SignupForm()
        {
            return Json(new { Username = "Type Username here", Password = "Type Password here", FullName = "What's your fullname", UserRole = "What's your role" });
        }
    }
}