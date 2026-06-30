using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Enums;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.MasterData;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.MarketPlace.Interfaces;
using MM.IT.Core.Services.MasterData;
using MM.IT.Core.Services.MasterData.Interfaces;
using MM.IT.Core.Services.MediaMarkt.Interfaces;
using MM.IT.Data.Entities.MediaMarktIT;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MediaMarkt
{
    public class MediaMarktService : BaseService, IMediaMarktService
    {
        private readonly IMediaMarktITRepositoryWrapper _mediaMarktITRepositoryWrapper;
        private readonly IMasterDataRepositoryWrapper _masterDataRepositoryWrapper;
        private readonly IMMOnlineRepositoryWrapper _mMOnlineRepositoryWrapper;
        private readonly IUnitOfWork<EFCoreMediaMarktITSqlProvider> _mmitUnitOfWork;
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IAyenSoftClientService _ayenSoftClientService;
        private readonly HttpClientConnectionModel _ayenSoftConnection;
        private readonly ILogger<MediaMarktService> _logger;
        public MediaMarktService(IServiceProvider serviceProvider
            , IMediaMarktITRepositoryWrapper mediaMarktITRepositoryWrapper
            , IUnitOfWork<EFCoreMediaMarktITSqlProvider> mmitUnitOfWork
            , IServiceWrapper serviceWrapper
            , IMasterDataRepositoryWrapper masterDataRepositoryWrapper
            , IMMOnlineRepositoryWrapper mMOnlineRepositoryWrapper
            , IAyenSoftClientService ayenSoftClientService
            , IOptions<HttpClientConfigModel> options
            , ILogger<MediaMarktService> logger) : base(serviceProvider)
        {
            _mediaMarktITRepositoryWrapper = mediaMarktITRepositoryWrapper;
            _mmitUnitOfWork = mmitUnitOfWork;
            _serviceWrapper = serviceWrapper;
            _masterDataRepositoryWrapper = masterDataRepositoryWrapper;
            _mMOnlineRepositoryWrapper = mMOnlineRepositoryWrapper;
            _ayenSoftClientService = ayenSoftClientService;
            _ayenSoftConnection = options.Value.AyenSoftConnection;
            _logger = logger;
        }

        public async Task<ServiceResultModel<StockModel>> GetMMITStocksAsync()
        {
            try
            {

                var stores = await _masterDataRepositoryWrapper.STRStoreRepository
                                                             .GetQuery()
                                                             .AsNoTracking()
                                                             .Where(a => a.MPStatus == true)
                                                             .Select(a => new MasterDataSTRStoreModel()
                                                             {
                                                                 SapCode = a.SapCode,
                                                                 MPThreshold = a.MPThreshold

                                                             }).ToArrayAsync();

                var response = new List<StoreStockModel>();

                #region checkT800

                var getT800ExcludeList = _mMOnlineRepositoryWrapper.T800ExcludeListRepository
                                        .GetQuery()
                                        .AsNoTracking()
                                        .Select(a => new { a.Article, a.PgId });

                var t800ExcludeList = new List<int>();

                var t800ExcludeListArtikellist = getT800ExcludeList.Select(a => a.Article).ToArray();

                foreach (var item in t800ExcludeListArtikellist)
                {
                    if (item.HasValue)
                    {
                        t800ExcludeList.Add(item.Value);
                    }
                }
                var t800ExcludeListPgist = getT800ExcludeList.Select(a => a.PgId).ToArray();

                #region pgs

                foreach (var item in t800ExcludeListPgist)
                {

                    using (var connectionMMIT = new DapperSqlServerProvider("Data Source=10.166.118.34;Initial Catalog=MASTERDATA;User ID=MMDefaultUser;Password=123c_C321;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=90;MultipleActiveResultSets=true;"))
                    {
                        var pgAndDeps = await connectionMMIT.GetListWithCommandAsync<PgDpDmModel>(@"
                    SELECT ArticleId, PgId
                    FROM  dbo.ART_Article WHERE PgId=" + item + "");

                        if (pgAndDeps.Any())
                        {

                            t800ExcludeList.AddRange(pgAndDeps.Select(a => a.ArticleId));
                        }
                    }
                }
                #endregion


                var t800toreStocks = await _mediaMarktITRepositoryWrapper
                                     .MMITT800StoreStockRepository
                                     .GetQuery()
                                     .AsNoTracking()
                                     .Where(p => !t800ExcludeList.Contains(p.Article) && p.SAP_CODE == "T800" && p.WarehouseID == 6)
                                     .OrderByDescending(a => a.CreatedDate)
                                     .Select(p => new StoreStockModel()
                                     {
                                         StockCode = p.Article,
                                         StockQuantity = p.Quantity,
                                         ReservationQuantity = p.ReservationQuantity

                                     })
                                     .ToListAsync();


                var isT800StoreMPThreshold = stores.FirstOrDefault(a => a.SapCode == "T800");
                if (isT800StoreMPThreshold != null)
                {

                    var t800StoreMPThreshold = isT800StoreMPThreshold.MPThreshold;
                    foreach (var item in t800toreStocks)
                    {
                        if (item.StockQuantity > t800StoreMPThreshold)
                        {
                            response.Add(new StoreStockModel()
                            {
                                StockCode = item.StockCode,
                                StockQuantity = (item.StockQuantity - item.ReservationQuantity) - t800StoreMPThreshold
                            });
                        }
                    }
                }
                else
                {
                    //foreach (var item in t800toreStocks.Where(a=>a.StockQuantity>0))
                    //{
                    //    response.Add(new StoreStockModel()
                    //    {
                    //        StockCode = item.StockCode,
                    //        StockQuantity = item.StockQuantity
                    //    });
                    //}
                }

                #endregion

                #region checkT601

                var t601toreStocks = await _mediaMarktITRepositoryWrapper
                             .MMITT601StoreStockRepository
                             .GetQuery()
                             .AsNoTracking()
                             .Where(p => p.SAP_CODE == "T601" && (p.WarehouseID == 4 || p.WarehouseID == 6))
                             .OrderByDescending(a => a.CreatedDate)
                             .Select(p => new StoreStockModel()
                             {
                                 StockCode = p.Article,
                                 StockQuantity = p.Quantity,
                                 ReservationQuantity = p.ReservationQuantity
                             })
                             .ToListAsync();

                var t601storeStocksGrp = t601toreStocks.GroupBy(a => a.StockCode).Select(res => new StoreStockModel()
                {
                    StockCode = res.First().StockCode,
                    StockQuantity = res.Sum(a => a.StockQuantity),
                    ReservationQuantity = res.Sum(a => a.ReservationQuantity)

                }).ToList();

                var isT601StoreMPThreshold = stores.FirstOrDefault(a => a.SapCode == "T601");
                if (isT601StoreMPThreshold != null)
                {
                    var t601StoreMPThreshold = isT601StoreMPThreshold.MPThreshold;
                    foreach (var item in t601storeStocksGrp)
                    {
                        if (item.StockQuantity > t601StoreMPThreshold)
                        {
                            response.Add(new StoreStockModel()
                            {
                                StockCode = item.StockCode,
                                StockQuantity = (item.StockQuantity - item.ReservationQuantity) - t601StoreMPThreshold
                            });
                        }
                    }
                }
                else
                {
                    //foreach (var item in t601toreStocks.Where(a=>a.StockQuantity>0))
                    //{
                    //    response.Add(new StoreStockModel()
                    //    {
                    //        StockCode = item.StockCode,
                    //        StockQuantity = item.StockQuantity
                    //    });
                    //}
                }

                #endregion

                var responseGrp = response.GroupBy(a => a.StockCode).Select(res => new StoreStockModel()
                {
                    StockCode = res.First().StockCode,
                    StockQuantity = res.Sum(a => a.StockQuantity)
                }).ToList();

                if (responseGrp.Any())
                {

                    var result = new StockModel()
                    {
                        StoreStocks = responseGrp
                    };

                    return Result<StockModel>(result);
                }
                return Result<StockModel>(null, StockMessageCodes.NotFoundData.GetDisplayName(), (int)StockMessageCodes.NotFoundData);

            }
            catch (Exception ex)
            {
                return Result<StockModel>(null, StockMessageCodes.UnknownError.GetDisplayName(), (int)StockMessageCodes.UnknownError);
            }
        }
        public async Task<ServiceResultModel<StockModel>> GetStocksByArtikelIdsAsync(StockRequestModel model)
        {
            try
            {
                if (model.ArtikelIds.Any())
                {
                    if (model.ArtikelIds.Count > 1000)
                    {
                        return Result<StockModel>(null, StockMessageCodes.ArtikelIdsCountExceeded.GetDisplayName(), (int)StockMessageCodes.ArtikelIdsCountExceeded);
                    }

                    var stores = await _masterDataRepositoryWrapper.STRStoreRepository
                                                            .GetQuery()
                                                            .AsNoTracking()
                                                            .Where(a => a.MPStatus == true)
                                                            .Select(a => new MasterDataSTRStoreModel()
                                                            {
                                                                SapCode = a.SapCode,
                                                                MPThreshold = a.MPThreshold

                                                            }).ToArrayAsync();

                    var response = new List<StoreStockModel>();


                    #region checkT800

                    var t800toreStocks = await _mediaMarktITRepositoryWrapper
                                                 .MMITT800StoreStockRepository
                                                 .GetQuery()
                                                 .AsNoTracking()
                                                 .Where(p => p.SAP_CODE == "T800" && p.WarehouseID == 6 && model.ArtikelIds.Select(a => a.ArtikelId).Contains(p.Article))
                                                 .OrderByDescending(a => a.CreatedDate)
                                                 .Select(p => new StoreStockModel()
                                                 {
                                                     StockCode = p.Article,
                                                     StockQuantity = p.Quantity,
                                                     ReservationQuantity = p.ReservationQuantity
                                                 })
                                                 .ToListAsync();

                    var isT800StoreMPThreshold = stores.FirstOrDefault(a => a.SapCode == "T800");
                    if (isT800StoreMPThreshold != null)
                    {
                        var t800StoreMPThreshold = isT800StoreMPThreshold.MPThreshold;

                        foreach (var item in t800toreStocks)
                        {
                            if (item.StockQuantity > t800StoreMPThreshold)
                            {
                                response.Add(new StoreStockModel()
                                {
                                    StockCode = item.StockCode,
                                    StockQuantity = (item.StockQuantity - item.ReservationQuantity) - t800StoreMPThreshold
                                });
                            }
                            else
                            {
                                response.Add(new StoreStockModel()
                                {
                                    StockCode = item.StockCode,
                                    StockQuantity = 0
                                });

                            }
                        }
                    }
                    else
                    {

                        foreach (var item in t800toreStocks)
                        {
                            response.Add(new StoreStockModel()
                            {
                                StockCode = item.StockCode,
                                StockQuantity = 0
                            });
                        }
                    }
                    #endregion

                    #region checkT601

                    var t601toreStocks = await _mediaMarktITRepositoryWrapper
                                                 .MMITT601StoreStockRepository
                                                 .GetQuery()
                                                 .AsNoTracking()
                                                 .Where(p => p.SAP_CODE == "T601" && (p.WarehouseID == 4 || p.WarehouseID == 6) && model.ArtikelIds.Select(a => a.ArtikelId).Contains(p.Article))
                                                 .OrderByDescending(a => a.CreatedDate)
                                                 .Select(p => new StoreStockModel()
                                                 {
                                                     StockCode = p.Article,
                                                     StockQuantity = p.Quantity,
                                                     ReservationQuantity = p.ReservationQuantity
                                                 })
                                                 .ToListAsync();

                    var t601storeStocksGrp = t601toreStocks.GroupBy(a => a.StockCode).Select(res => new StoreStockModel()
                    {
                        StockCode = res.First().StockCode,
                        StockQuantity = res.Sum(a => a.StockQuantity),
                        ReservationQuantity = res.Sum(a => a.ReservationQuantity)

                    }).ToList();

                    var isT601StoreMPThreshold = stores.FirstOrDefault(a => a.SapCode == "T601");
                    if (isT601StoreMPThreshold != null)
                    {
                        var t601StoreMPThreshold = isT601StoreMPThreshold.MPThreshold;

                        foreach (var item in t601storeStocksGrp)
                        {
                            if (item.StockQuantity > t601StoreMPThreshold)
                            {
                                response.Add(new StoreStockModel()
                                {
                                    StockCode = item.StockCode,
                                    StockQuantity = (item.StockQuantity - item.ReservationQuantity) - t601StoreMPThreshold
                                });
                            }
                            else
                            {
                                response.Add(new StoreStockModel()
                                {
                                    StockCode = item.StockCode,
                                    StockQuantity = 0
                                });

                            }
                        }
                    }
                    else
                    {
                        foreach (var item in t601toreStocks)
                        {
                            response.Add(new StoreStockModel()
                            {
                                StockCode = item.StockCode,
                                StockQuantity = 0
                            });
                        }
                    }

                    #endregion

                    var responseGrp = response.GroupBy(a => a.StockCode).Select(res => new StoreStockModel()
                    {
                        StockCode = res.First().StockCode,
                        StockQuantity = res.Sum(a => a.StockQuantity)
                    }).ToList();

                    responseGrp.AddRange(model.ArtikelIds.Where(a => !responseGrp.Select(b => b.StockCode).Contains(a.ArtikelId)).Select(a => new StoreStockModel()
                    {
                        StockCode = a.ArtikelId,
                        StockQuantity = 0
                    }));

                    if (responseGrp.Any())
                    {

                        var result = new StockModel()
                        {
                            StoreStocks = responseGrp
                        };

                        return Result<StockModel>(result);
                    }
                }
                else
                {
                    return Result<StockModel>(null, StockMessageCodes.NotNullArtikelIds.GetDisplayName(), (int)StockMessageCodes.NotNullArtikelIds);
                }
                return Result<StockModel>(null, StockMessageCodes.NotFoundData.GetDisplayName(), (int)StockMessageCodes.NotFoundData);
            }
            catch (Exception ex)
            {
                return Result<StockModel>(null, StockMessageCodes.UnknownError.GetDisplayName(), (int)StockMessageCodes.UnknownError);
            }
        }
        public async Task<ServiceResultModel<IEnumerable<KeyValueModel<string, string>>>> GetEmailContentByIdAsync(int Id)
        {
            var entities = await _mediaMarktITRepositoryWrapper.EmailContentRepository
                .GetQuery()
                .AsNoTracking()
                .Where(a => a.ID == Id)
                .Select(p => new KeyValueModel<string, string>
                {
                    Key = p.EmailDescription,
                    Value = p.EmailContent
                })
                .ToListAsync();

            return Result<IEnumerable<KeyValueModel<string, string>>>(entities);
        }
        public async Task<ServiceResultModel<StockModel>> GetStoreStocksAsync()
        {
            try
            {

                var stores = _masterDataRepositoryWrapper.STRStoreRepository
                                                             .GetQuery()
                                                             .AsNoTracking()
                                                             .Where(a => a.MPStatus == true)
                                                             .Select(a => new MasterDataSTRStoreModel()
                                                             {
                                                                 SapCode = a.SapCode,
                                                                 MPThreshold = a.MPThreshold

                                                             });

                if (stores.Any())
                {

                    var response = new List<StoreStockModel>();

                    var criteriaDate = DateTime.Now.AddMinutes(-4);

                    var ist601StoreStock = await stores.FirstOrDefaultAsync(a => a.SapCode == "T601");
                    if (ist601StoreStock != null)
                    {
                        var t601storeStocks = await _mediaMarktITRepositoryWrapper
                                 .MMITT601StoreStockRepository
                                 .GetQuery()
                                 .AsNoTracking()
                                 .Where(p => p.SAP_CODE == "T601"
                                             && (p.WarehouseID == 4 || p.WarehouseID == 6)
                                             && p.UpdatedDate >= criteriaDate)
                                 .OrderByDescending(a => a.CreatedDate)
                                 .Select(p => new StoreStockModel()
                                 {
                                     StockCode = p.Article,
                                     StockQuantity = p.Quantity,
                                     ReservationQuantity = p.ReservationQuantity
                                 })
                                 .ToListAsync();

                        var t601storeStocksGrp = t601storeStocks.GroupBy(a => a.StockCode).Select(res => new StoreStockModel()
                        {
                            StockCode = res.First().StockCode,
                            StockQuantity = res.Sum(a => a.StockQuantity),
                            ReservationQuantity = res.Sum(a => a.ReservationQuantity)

                        }).ToList();

                        var t601StoreMPThreshold = ist601StoreStock.MPThreshold;
                        foreach (var item in t601storeStocksGrp)
                        {
                            if (item.StockQuantity > t601StoreMPThreshold)
                            {
                                response.Add(new StoreStockModel()
                                {
                                    StockCode = item.StockCode,
                                    StockQuantity = (item.StockQuantity - item.ReservationQuantity) - t601StoreMPThreshold
                                });
                            }
                        }

                    }

                    var ist800StoreStock = await stores.FirstOrDefaultAsync(a => a.SapCode == "T800");
                    if (ist800StoreStock != null)
                    {
                        var t800StoreMPThreshold = ist800StoreStock.MPThreshold;

                        var t800storeStocks = await _mediaMarktITRepositoryWrapper
                                     .MMITT800StoreStockRepository
                                     .GetQuery()
                                     .AsNoTracking()
                                     .Where(p => p.SAP_CODE == "T800"
                                                && p.WarehouseID == 6
                                                && p.UpdatedDate >= criteriaDate)
                                     .OrderByDescending(a => a.CreatedDate)
                                     .Select(p => new StoreStockModel()
                                     {
                                         StockCode = p.Article,
                                         StockQuantity = p.Quantity,
                                         ReservationQuantity = p.ReservationQuantity
                                     })
                                     .ToListAsync();

                        foreach (var item in t800storeStocks)
                        {
                            if (item.StockQuantity > t800StoreMPThreshold)
                            {
                                response.Add(new StoreStockModel()
                                {
                                    StockCode = item.StockCode,
                                    StockQuantity = (item.StockQuantity - item.ReservationQuantity) - t800StoreMPThreshold
                                });
                            }
                        }

                    }

                    var t800Andt601ExcluedStores = stores.Where(a => a.SapCode != "T800" && a.SapCode != "T601");
                    if (t800Andt601ExcluedStores.Any())
                    {
                        foreach (var storeItem in t800Andt601ExcluedStores)
                        {
                            var store = storeItem.MPThreshold;

                            var storeStocks = await _mediaMarktITRepositoryWrapper
                                 .MMITStoreStockRepository
                                 .GetQuery()
                                 .AsNoTracking()
                                 .Where(p => p.SAP_CODE == storeItem.SapCode
                                            && p.WarehouseID == 6
                                            && p.UpdatedDate >= criteriaDate)
                                 .OrderByDescending(a => a.CreatedDate)
                                 .Select(p => new StoreStockModel()
                                 {
                                     StockCode = p.Article,
                                     StockQuantity = p.Quantity,
                                     ReservationQuantity = p.ReservationQuantity
                                 })
                                 .ToListAsync();

                            foreach (var item in storeStocks)
                            {
                                if (item.StockQuantity > store)
                                {
                                    response.Add(new StoreStockModel()
                                    {
                                        StockCode = item.StockCode,
                                        StockQuantity = (item.StockQuantity - item.ReservationQuantity) - store
                                    });
                                }
                            }
                        }
                    }

                    var responseGrp = response.GroupBy(a => a.StockCode).Select(res => new StoreStockModel()
                    {
                        StockCode = res.First().StockCode,
                        StockQuantity = res.Sum(a => a.StockQuantity)
                    }).ToList();

                    if (responseGrp.Any())
                    {

                        var result = new StockModel()
                        {
                            StoreStocks = responseGrp
                        };

                        return Result<StockModel>(result);
                    }
                    return Result<StockModel>(null, StockMessageCodes.NotFoundData.GetDisplayName(), (int)StockMessageCodes.NotFoundData);
                }
                return Result<StockModel>(null, StockMessageCodes.NotFoundData.GetDisplayName(), (int)StockMessageCodes.NotFoundData);

            }
            catch (Exception ex)
            {
                return Result<StockModel>(null, StockMessageCodes.UnknownError.GetDisplayName(), (int)StockMessageCodes.UnknownError);
            }
        }
        public async Task<ServiceResultModel<PushStocksResponseModel>> PushStocks()
        {
            var response = new PushStocksResponseModel();
            try
            {

                var getStocks = await GetMMITStocksAsync();
                if (getStocks != null)
                {
                    if (getStocks.Data != null)
                    {
                        if (getStocks.Data.StoreStocks.Any())
                        {
                            response.TotalStockCount = getStocks.Data.StoreStocks.Count();
                            var packages = getStocks.Data.StoreStocks.Chunk(100).ToHashSet();

                            #region withParallel

                            await Parallel.ForEachAsync(packages, new ParallelOptions() { MaxDegreeOfParallelism = 5 },
                                async (items, CancellationToken) =>
                                {

                                    var getStocksXml = new StringBuilder();
                                    getStocksXml.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\"> <soapenv:Header/> <soapenv:Body> <tem:AnlikStokFiyatGuncelle> <!--Optional:--> <tem:request> <!--Optional:--> <tem:KullaniciAdi>" + _ayenSoftConnection.Username + "</tem:KullaniciAdi> <!--Optional:--> <tem:Sifre>" + _ayenSoftConnection.Password + "</tem:Sifre>");
                                    foreach (var item in items)
                                    {
                                        getStocksXml.Append("<tem:Urunler>");
                                        getStocksXml.Append("<tem:PlatformBazliMusteriUrunModel>");
                                        getStocksXml.Append("<tem:StokKodu>" + item.StockCode + "</tem:StokKodu> ");
                                        getStocksXml.Append("<tem:Stok>" + item.StockQuantity + "</tem:Stok>");
                                        getStocksXml.Append("</tem:PlatformBazliMusteriUrunModel>");
                                        getStocksXml.Append("</tem:Urunler>");
                                    }
                                    getStocksXml.Append("</tem:request></tem:AnlikStokFiyatGuncelle></soapenv:Body></soapenv:Envelope>");
                                    var stringContent = new StringContent(getStocksXml.ToString(), Encoding.UTF8, "text/xml");

                                    var result = await _ayenSoftClientService.AnlikStokFiyatGuncelleAsync("", stringContent);
                                    if (result.Data.Any())
                                    {
                                        response.Result = StockMessageCodes.Successful.GetDisplayName();
                                    }
                                    else
                                    {
                                        _logger.LogError($"<MediaMarktService>PushStocks:ayensoft error!</MediaMarktService>");

                                    }
                                });

                            #endregion

                            #region MyRegion

                            //foreach (var package in packages)
                            //{
                            //    var getStocksXml = new StringBuilder();
                            //    getStocksXml.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\"> <soapenv:Header/> <soapenv:Body> <tem:AnlikStokFiyatGuncelle> <!--Optional:--> <tem:request> <!--Optional:--> <tem:KullaniciAdi>" + _ayenSoftConnection.Username + "</tem:KullaniciAdi> <!--Optional:--> <tem:Sifre>" + _ayenSoftConnection.Password + "</tem:Sifre>");
                            //    foreach (var item in package)
                            //    {
                            //        getStocksXml.Append("<tem:Urunler>");
                            //        getStocksXml.Append("<tem:PlatformBazliMusteriUrunModel>");
                            //        getStocksXml.Append("<tem:StokKodu>" + item.StockCode + "</tem:StokKodu> ");
                            //        getStocksXml.Append("<tem:Stok>" + item.StockQuantity + "</tem:Stok>");
                            //        getStocksXml.Append("</tem:PlatformBazliMusteriUrunModel>");
                            //        getStocksXml.Append("</tem:Urunler>");
                            //    }
                            //    getStocksXml.Append("</tem:request></tem:AnlikStokFiyatGuncelle></soapenv:Body></soapenv:Envelope>");
                            //    var stringContent = new StringContent(getStocksXml.ToString(), Encoding.UTF8, "text/xml");

                            //    var result = await _ayenSoftClientService.AnlikStokFiyatGuncelleAsync("", stringContent);
                            //    if (result.Data.Any())
                            //    {
                            //        response.Result = StockMessageCodes.Successful.GetDisplayName();
                            //    }
                            //    else
                            //    {
                            //        _logger.LogError($"<MediaMarktService>PushStocks:ayensoft error!</MediaMarktService>");
                            //        return Result<PushStocksResponseModel>(null, StockMessageCodes.UnknownError.GetDisplayName(), (int)StockMessageCodes.UnknownError);

                            //    }
                            //}

                            #endregion

                            return Result(response);
                        }
                    }

                }
                return Result<PushStocksResponseModel>(null, StockMessageCodes.NotFoundData.GetDisplayName(), (int)StockMessageCodes.NotFoundData);
            }

            catch (Exception ex)
            {
                return Result<PushStocksResponseModel>(null, StockMessageCodes.UnknownError.GetDisplayName(), (int)StockMessageCodes.UnknownError);
            }
        }
    }
}
