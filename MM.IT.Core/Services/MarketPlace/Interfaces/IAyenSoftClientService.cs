using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MarketPlace;
using MM.IT.Common.Models.MarketPlace.AyenSoft;
using MM.IT.Common.Models.OnlineOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MarketPlace.Interfaces
{
    public interface IAyenSoftClientService
    {
        Task<ServiceResultModel<IEnumerable<AyenSoftResponseModel>>> AnlikStokFiyatGuncelleAsync(string url, HttpContent contentPost);
        Task<ServiceResultModel<SiparisTicariSistemCariGuncelleResponseModel>> SiparisTicariSistemCariGuncelle(string url, HttpContent contentPost);
        Task<ServiceResultModel<PlatformKargoBilgisiniGirResponseModel>> PlatformKargoBilgisiniGirAsync(string url, HttpContent contentPost);
        Task<ServiceResultModel<PlatformKargoStatuGuncelleResponseModel>> PlatformKargoStatuGuncelleAsync(string url, HttpContent contentPost);
    }
}
