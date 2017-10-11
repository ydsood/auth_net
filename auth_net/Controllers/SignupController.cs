using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using auth_net.Model;
using auth_net.DAO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using auth_net.Filters;

namespace auth_net.Controllers
{
    [Produces("application/json")]
    [Route("api/Signup")]
    [ValidateModel]
    public class SignupController : Controller
    {
        private TokenOptions _tokenOptions;
        public SignupController(IOptions<TokenOptions> tokenOptionsAccessor)
        {
            _tokenOptions = tokenOptionsAccessor.Value;
        }

        [HttpPost]
        public IActionResult SignUp([FromBody]User user)
        {
            var repo = UserRepository.Get();
            try
            {
                repo.GetUser(user.UserName);
            }
            catch(KeyNotFoundException)
            {
                User newUser = repo.AddUser(user);
                return Ok(Util.GenerateJWT(newUser, _tokenOptions));
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