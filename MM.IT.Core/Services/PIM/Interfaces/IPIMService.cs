using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.PIM;
using MM.IT.Common.Models.Sms;
using MM.IT.Core.Services.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.PIM.Interfaces
{

    /// <summary>
    /// IPIMService Servis Interface Tanımı
    /// </summary>
    public interface IPIMService : IService
    {
        /// <summary>
        /// MasterDataPIM
        /// </summary>
        /// <returns></returns>
        Task<ServiceResultModel<List<ProductSummaryModel>>> MasterDataPIM();
    }
}
