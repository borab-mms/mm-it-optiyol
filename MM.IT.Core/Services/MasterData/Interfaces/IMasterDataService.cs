using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MasterData;
using MM.IT.Core.Services.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MasterData.Interfaces
{
    /// <summary>
    /// Master Data DB Servis Interface Tanımı
    /// </summary>
    public partial interface IMasterDataService : IService
    {
        #region Articles
        Task<ServiceResultModel<IEnumerable<ArtikelInfoModel>>> GetArticleInfoByArtikelIdAsync(List<int> model);
        Task<ServiceResultModel<IEnumerable<KeyValueModel<string>>>> GetArticlesBySearchAsync(string search);
        #endregion
        Task<ServiceResultModel<IEnumerable<KeyValueModel<string, int>>>> GetStoresBySearchAsync(string search);
        Task<ServiceResultModel<IEnumerable<KeyValueModel<string, int>>>> GetStoresAsync();
        Task<ServiceResultModel<IEnumerable<MasterDataSTRStoreModel>>> GetStoresAsync(bool IsActive);
        Task<ServiceResultModel<KeyValueModel<int, string>>> GetCityByCityCodeAsync(int cityCode);
        Task<ServiceResultModel<KeyValueModel<int, string>>> GetDistrictByDistrictCodeAsync(int districtCode);
    }
}
