using MM.IT.Common.Models.DigitalCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.DigitalCard.Interfaces
{
    public interface IEpayClientService
    {
        Task<SerialCodeInEpayResponseModel> PostAsync(string url, HttpContent contentPost);

    }
}
