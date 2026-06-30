using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Integration.VCR;
using MM.IT.Common.Models.MediaMarkt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MediaMarkt.Interfaces
{
    /// <summary>
    /// MMIT Servis Interface Tanımı
    /// </summary>

    public interface IMMITService
    {
        
        /// <summary>
        /// MMIT servisindeki gelen yeni faturaları kaydeder.
        /// </summary>
        /// <returns></returns>
        Task<ServiceResultModel<CreateInvoiceByCustomerOrderNumberResponseModel>> SendDataToFOMByCustomerOrderNumbersAsync(CreateInvoiceByCustomerOrderNumberRequestModel model);


    }
}
