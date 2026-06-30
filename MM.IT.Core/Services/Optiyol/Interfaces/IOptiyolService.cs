using MM.IT.Common.Models.Common;
using MM.Optiyol.Api.Models.Optiyol;
namespace MM.Optiyol.Api.Services.Optiyol.Interfaces
{
    public interface IOptiyolService
    {
        Task<ServiceResultModel> WebhookAsync(OptiyolRequestModel input, string createdDate);
        Task<ServiceResultModel> CreateBarcodeAsync(OptiyolBarcodeCreateRequestModel model);
        Task<ServiceResultModel> CancelBarcodeAsync(OptiyolBarcodeCancelRequestModel model);
    }
}
