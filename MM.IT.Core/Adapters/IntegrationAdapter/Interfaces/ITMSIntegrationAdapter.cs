using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Sterling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.IntegrationAdapter.Interfaces
{
    public interface ITMSIntegrationAdapter
    {
        Task<ServiceResultModel<CreateInvoiceResponseModel>> SendFOMInvoiceToTMSAsync(CreateInvoiceRequestModel input);
    }
}
