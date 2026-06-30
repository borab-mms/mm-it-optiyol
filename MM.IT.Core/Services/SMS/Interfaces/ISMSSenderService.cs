using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Common.Models.Sms;
using MM.IT.Core.Services.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.SMS.Interfaces
{
    /// <summary>
    /// ISMSSenderService Servis Interface Tanımı
    /// </summary>
    public interface ISMSSenderService : IService
    {
        /// <summary>
        /// Tekli sms ekler.
        /// </summary>
        /// <returns></returns>
        Task<ServiceResultModel<SMSSenderResponse>> SMSSender(SMSSenderRequest model);

        /// <summary>
        /// Çoklu sms ekler.
        /// </summary>
        /// <returns></returns>
        Task<ServiceResultModel<SMSMultiSenderResponse>> SMSMultiSender(SMSMultiSenderRequest model);

        /// <summary>
        /// OTP sms ekler.
        /// </summary>
        /// <returns></returns>
        Task<ServiceResultModel<OTPSMSSenderResponse>> OTPSMSSender(OTPSMSSenderRequest model);
    }
}
