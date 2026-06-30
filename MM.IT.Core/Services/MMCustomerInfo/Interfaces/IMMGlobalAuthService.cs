using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MMCustomerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MMCustomerInfo.Interfaces
{
    public interface IMMGlobalAuthService
    {
        Task<ServiceResultModel<AuthResponseModel>> GetToken(AuthRequestModel model);
    }
}
