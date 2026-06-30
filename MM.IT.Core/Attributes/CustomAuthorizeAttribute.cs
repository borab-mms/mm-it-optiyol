using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.Configuration;

namespace MM.IT.Core.Attributes
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
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

            var token = authHeader.Replace("Bearer", "").Trim();

            //if (isExpired(token))
            //{
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}

            var claims = GetPrincipal(token, context);

            if (claims == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string username = claims.Identity.Name;


            var _configurationSection = _config.GetSection("ApplicationSettings:TokenOptions");
            var _users = _config.GetSection("ApplicationSettings:Users").Get<List<Users>>();

            if (string.IsNullOrEmpty(username) || !_users.Exists(x => x.UserName == username))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //context.HttpContext.User = new GenericPrincipal(new GenericIdentity(username), new string[] { });

        }

        ClaimsPrincipal GetPrincipal(string token, AuthorizationFilterContext context)
        {
            try
            {
                var _config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json")
                    .Build();

                var tokenHandler = new JwtSecurityTokenHandler();

                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null || jwtToken.ValidTo > DateTime.Now) return null;

                var tokenOptions = _config.GetSection("ApplicationSettings:TokenOptions").Get<TokenOptionsModel>();

                var validationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
