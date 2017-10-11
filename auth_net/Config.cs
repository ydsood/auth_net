using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth_net
{
    public class TokenOptions
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expiration { get; set; }
    }
}
