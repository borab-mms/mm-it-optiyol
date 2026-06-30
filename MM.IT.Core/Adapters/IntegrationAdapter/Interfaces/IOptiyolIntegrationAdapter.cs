using MM.IT.Common.Models.Common;
using MM.Optiyol.Api.Models.Optiyol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.IntegrationAdapter.Interfaces
{
    public interface IOptiyolIntegrationAdapter
    {
        Task<ServiceResultModel<OptiyolBarcodeCancelResponseModel>> CancelBarcodeAsync(OptiyolBarcodeCancelRequestModel model);
    }
}
