using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MM.IT.Core.Services.Auth.Security.Encryption;
using MM.IT.Core.Services.Auth.Security.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Attributes
{

    public class CheckTokenAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var myToken = "";
            var token = authHeader.Replace("Bearer", "").Trim();
            if (!string.IsNullOrEmpty(token))
            {
            }
        }
    }
}
