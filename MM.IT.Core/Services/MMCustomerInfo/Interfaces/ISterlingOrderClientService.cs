using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Common.Models.Sterling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MMCustomerInfo.Interfaces
{

    public interface ISterlingOrderClientService
    {
        Task<ServiceResultModel<List<CustomerInfoResponseModel>>> GetCustomerInfoAsync(string url);
        Task<ServiceResultModel<CustomerOrderModel>> GetCustomerOrdersAsync(string url);
    }
}
