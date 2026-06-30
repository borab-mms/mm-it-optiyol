using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.OnlineOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.IntegrationAdapter.Interfaces
{
    public interface IAyenSoftIntegrationAdapter
    {
        Task<ServiceResultModel<PlatformKargoBilgisiniGirResponseModel>> PlatformKargoBilgisiniGirAsync(PlatformKargoBilgisiniGirRequestModel input);

    }
}
