using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Core.Services.Auth.Interfaces;
using MM.IT.Core.Services.Auth.Security.Jwt;
using MM.IT.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.Auth
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IOptions<List<Users>> _users;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(IServiceProvider serviceProvider
            , ILogger<AuthService> logger
            , IOptions<List<Users>> users
            , ITokenHelper tokenHelper) : base(serviceProvider)
        {
            _logger = logger;
            _users = users;
            _tokenHelper = tokenHelper;
        }
        public async Task<ServiceResultModel<AccessToken>> CreateAccessToken(JwtUser user)
        {
            var result = await _tokenHelper.CreateToken(user);
            return result;
        }
        public async Task<ServiceResultModel<bool>> Login(JwtUser userForLoginDto)
        {
            var result = _users.Value.Exists(x => x.UserName == userForLoginDto.UserName && x.Password == userForLoginDto.Password);
            if (result)
            {
                return Result<bool>(result);
            }

            return Result<bool>(result, "Kullanıcı Bulunamadı", StatusCodes.Status404NotFound);
        }
    }
}
