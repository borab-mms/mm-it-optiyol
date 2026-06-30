using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.Auth.Security.Jwt
{
    public class TokenOptionsModel
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
        public string JwtUserName { get; set; }
        public string JwtPassword { get; set; }
    }
}
