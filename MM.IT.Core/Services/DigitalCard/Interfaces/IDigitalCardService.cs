using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.DigitalCard;

namespace MM.IT.Core.Services.DigitalCard.Interfaces
{
    public interface IDigitalCardService
    {
        Task<ServiceResultModel<List<CreataSerialCodeInEpayResponseModel>>> CreataSerialCodeInEpay(CreataSerialCodeInEpayRequestModel model);
    }
}
