using MM.IT.Common.Models.Common;
using MM.IT.Core.Services.Auth.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResultModel<bool>> Login(JwtUser userForLoginDto);
        Task<ServiceResultModel<AccessToken>> CreateAccessToken(JwtUser user);
    }
}
