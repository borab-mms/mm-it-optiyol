using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Common.Models.Sterling.CosV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MMCustomerInfo.Interfaces
{
    public interface ISterlingCosV1OrderClientService
    {
        Task<ServiceResultModel<List<CustomerInfoResponseModel>>> GetCustomerInfoAsync(string url);
        Task<ServiceResultModel<CosV1CustomerOrderRawDataModel>> GetCustomerOrdersAsync(string url);

    }
}
