using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Integration.VCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.IntegrationAdapter.Interfaces
{
    public interface IVCRIntegrationAdapter
    {
        Task<ServiceResultModel<VCRInvoiceResponseModel>> CreateInvoiceAsync(VCRInvoiceRequestModel input);
    }
}
