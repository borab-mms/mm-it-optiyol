using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Core.Services.Auth.Interfaces;
using MM.IT.Core.Services.Auth.Security.Encryption;
using MM.IT.Core.Services.Auth.Security.Jwt;
using MM.IT.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.Auth
{
    public class JwtHelper : BaseService, ITokenHelper
    {
        private readonly IOptions<ApplicationSettingModel> _applicationSettingModel;
        DateTime _accessTokenExpiration;
        public JwtHelper(IServiceProvider serviceProvider,IOptions<ApplicationSettingModel> applicationSettingModel):base(serviceProvider) 
        {
            _applicationSettingModel = applicationSettingModel;
        }
        public async Task<ServiceResultModel<AccessToken>> CreateToken(JwtUser user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_applicationSettingModel.Value.TokenOptions.AccessTokenExpiration));
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_applicationSettingModel.Value.TokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            var accessToken = new  AccessToken { Token = token, Expiration = _accessTokenExpiration };

            return Result<AccessToken>(accessToken);
        }
        public JwtSecurityToken CreateJwtSecurityToken(JwtUser user, SigningCredentials signingCredentials)
        {
            var jwtToken = new JwtSecurityToken(
                issuer: _applicationSettingModel.Value.TokenOptions.Issuer,
                audience: _applicationSettingModel.Value.TokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
                );
            return jwtToken;
        }
        private IEnumerable<Claim> SetClaims(JwtUser user)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(Guid.NewGuid().ToString());
            claims.AddName($"{user.UserName}");
            return claims;
        }

    }
}
