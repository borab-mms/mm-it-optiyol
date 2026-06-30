using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MM.IT.Common.Constants;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MasterData;
using MM.IT.Common.Resources;
using MM.IT.Core.Adapters.MapperAdapter;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.MasterData.Interfaces;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MasterData
{
    /// <summary>
    /// IMasterDataService şartlarını sağlayan Servis Nesnesi
    /// </summary>
    public partial class MasterDataService : BaseService, IMasterDataService
    {
        private readonly IMasterDataRepositoryWrapper _masterDataRepositoryWrapper;
        private readonly IUnitOfWork<EFCoreMasterDataSqlProvider> _masterDataUnitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IServiceWrapper _serviceWrapper;

        public MasterDataService(IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<SharedResources> stringLocalizer,
            IServiceWrapper serviceWrapper,
            IMasterDataRepositoryWrapper masterDataRepositoryWrapper,
            IUnitOfWork<EFCoreMasterDataSqlProvider> masterDataUnitOfWork) : base(serviceProvider)
        {
            _masterDataRepositoryWrapper = masterDataRepositoryWrapper;
            _httpContextAccessor = httpContextAccessor;
            _stringLocalizer = stringLocalizer;
            _serviceWrapper = serviceWrapper;
            _masterDataUnitOfWork = masterDataUnitOfWork;
        }


        #region Articles
        public async Task<ServiceResultModel<IEnumerable<KeyValueModel<string>>>> GetArticlesBySearchAsync(string search)
        {
            var entities = await _masterDataRepositoryWrapper
                .ARTArticleRepository
                .GetQuery()
                .AsNoTracking()
                .Where(a =>
                a.ArticleId.ToString().StartsWith(search) ||
                a.ArticleName.StartsWith(search) ||
                a.ArticleId.ToString().Contains(search) ||
                a.ArticleName.Contains(search) ||
                a.ArticleName.ToLower().Contains(search.ToSearchMode()))
                .Select(p => new KeyValueModel<string>
                {
                    Key = p.ArticleId + " - " + p.ArticleName.ToString(),
                    Value = p.ArticleId.ToString()
                })
                .OrderByDescending(p => p.Value == search || p.Value.StartsWith(search))
                .Take(15)
                .ToListAsync();

            return Result<IEnumerable<KeyValueModel<string>>>(entities);
        }
        public async Task<ServiceResultModel<IEnumerable<ArtikelInfoModel>>> GetArticleInfoByArtikelIdAsync(List<int> model)
        {
            try
            {
                var employee = _masterDataRepositoryWrapper.ARTArticleRepository
                    .GetQuery()
                    .AsNoTracking()
                    .Where(a=>model.Contains(a.ArticleId))
                    .Select(p => new ArtikelInfoModel()
                    {
                        ArticleId = p.ArticleId,
                        PgId = (short)p.PgId,
                        BrandId = (short)p.BrandId
                    })
                    .AsEnumerable();

                if (employee == null)
                {
                    return Result<IEnumerable<ArtikelInfoModel>>(null, _stringLocalizer["Global.NotFound"].Value, StatusCodes.Status404NotFound);
                }

                return Result(employee);
            }
            catch (Exception ex)
            {

                return Result<IEnumerable<ArtikelInfoModel>>(null, ex.Message, 400);
            }
        }
        #endregion
        public async Task<ServiceResultModel<IEnumerable<KeyValueModel<string, int>>>> GetStoresBySearchAsync(string search)
        {
            var stores = await _masterDataRepositoryWrapper.STRStoreRepository
                                                             .GetQuery()
                                                             .AsNoTracking()
                                                             .Where(a => a.MPStatus == true &&
                                                             (a.SapCode.Equals(search)
                                                             || a.SapCode.Contains(search)))
                                                              .Select(p => new KeyValueModel<string, int>
                                                              {
                                                                  Key = p.SapCode,
                                                                  Value = p.MPThreshold
                                                              })
                                                              .ToListAsync();

            return Result<IEnumerable<KeyValueModel<string, int>>>(stores);
        }
        public async Task<ServiceResultModel<IEnumerable<KeyValueModel<string, int>>>> GetStoresAsync()
        {
            var stores = await _masterDataRepositoryWrapper.STRStoreRepository
                                                             .GetQuery()
                                                             .AsNoTracking()
                                                             .Where(a=>a.IsActive== true)
                                                             .Select(p => new KeyValueModel<string, int>
                                                              {
                                                                  Key = p.SapCode,
                                                                  Value = p.MPThreshold
                                                              })
                                                              .ToListAsync();

            return Result<IEnumerable<KeyValueModel<string, int>>>(stores);
        }
        public async Task<ServiceResultModel<IEnumerable<MasterDataSTRStoreModel>>> GetStoresAsync(bool IsActive)
        {
            var stores = await _masterDataRepositoryWrapper.STRStoreRepository
                                                             .GetQuery()
                                                             .AsNoTracking()
                                                             .Where(a => a.IsActive == IsActive)
                                                             .Select(a=>new MasterDataSTRStoreModel()
                                                             {
                                                                 Id = a.Id,
                                                                 CityCode= a.CityCode,
                                                                 DistrictCode= a.DistrictCode,
                                                                 IsActive= a.IsActive,
                                                                 MPThreshold = a.MPThreshold,
                                                                 Name = a.StoreName,
                                                                 OutletId = a.OutletId,
                                                                 SapCode=a.SapCode,
                                                                 StoreAddress=a.StoreAddress,
                                                                 ZipCode = a.ZipCode
                                                             })
                                                              .ToListAsync();
            if (!stores.Any())
            {

                return Result<IEnumerable<MasterDataSTRStoreModel>>(null, "Kayıt bulunamadı!", StatusCodes.Status404NotFound);
            }

            return Result<IEnumerable<MasterDataSTRStoreModel>>(stores);
        }
        public async Task<ServiceResultModel<KeyValueModel<int, string>>> GetCityByCityCodeAsync(int cityCode)
        {
            var city = await _masterDataRepositoryWrapper.ZCCityRepository
                                                           .GetQuery()
                                                           .AsNoTracking()
                                                           .Where(a => (a.Code.Equals(cityCode)))
                                                            .Select(p => new KeyValueModel<int, string>
                                                            {
                                                                Key = p.Code,
                                                                Value = p.Name
                                                            })
                                                            .FirstOrDefaultAsync();

            return Result<KeyValueModel<int, string>>(city);
        }
        public async Task<ServiceResultModel<KeyValueModel<int, string>>> GetDistrictByDistrictCodeAsync(int districtCode)
        {
            var city = await _masterDataRepositoryWrapper.ZCDistrictRepository
                                                           .GetQuery()
                                                           .AsNoTracking()
                                                           .Where(a => (a.Code.Equals(districtCode)))
                                                            .Select(p => new KeyValueModel<int, string>
                                                            {
                                                                Key = p.Code,
                                                                Value = p.Name
                                                            })
                                                            .FirstOrDefaultAsync();

            return Result<KeyValueModel<int, string>>(city);
        }
    }
}
