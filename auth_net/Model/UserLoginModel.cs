using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace auth_net.Model
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
