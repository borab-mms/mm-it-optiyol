using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.Integration.VCR;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Common.Models.Sterling;
using MM.IT.Core.Adapters.RedisAdaptor.Interfaces;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.MediaMarkt.Interfaces;
using MM.IT.Core.Services.MMCustomerInfo.Interfaces;
using MM.IT.Data.Entities.MMIT;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MediaMarkt
{
    public class MMITService : BaseService, IMMITService
    {
        private readonly IMMITRepositoryWrapper _mmITRepositoryWrapper;
        private readonly IUnitOfWork<EFCoreMasterDataSqlProvider> _masterDataUnitOfWork;
        private readonly IUnitOfWork<EFCoreMMOnlineSqlProvider> _mmOnlineUnitOfWork;
        private readonly IMasterDataRepositoryWrapper _masterDataRepositoryWrapper;
        private readonly IMMOfflineRepositoryWrapper _mmOfflineRepositoryWrapper;
        private readonly IMMOnlineRepositoryWrapper _mmOnlineRepositoryWrapper;
        private readonly ILogger<MMITService> _logger;
        private readonly IUnitOfWork<EFCoreMMITSqlProvider> _mmITUnitOfWork;
        private readonly IOptions<RedisConfigModel> _redisConfigs;
        private readonly ISterlingOrderClientService _sterlingOrderClientService;
        private readonly IRedisDistributedAdapter _redisDistributedAdapter;
        public MMITService(IServiceProvider serviceProvider
            , ILogger<MMITService> logger
            , IMMITRepositoryWrapper mmITRepositoryWrapper
            , IUnitOfWork<EFCoreMMITSqlProvider> mmITUnitOfWork
            , IUnitOfWork<EFCoreMasterDataSqlProvider> masterDataUnitOfWork
            , IMasterDataRepositoryWrapper masterDataRepositoryWrapper
            , IMMOfflineRepositoryWrapper mmOfflineRepositoryWrapper
            , IMMOnlineRepositoryWrapper mmOnlineRepositoryWrapper
            , IOptions<RedisConfigModel> redisConfigs
            , ISterlingOrderClientService sterlingOrderClientService
            , IRedisDistributedAdapter redisDistributedAdapter
            , IUnitOfWork<EFCoreMMOnlineSqlProvider> mmOnlineUnitOfWork) : base(serviceProvider)
        {
            _logger = logger;
            _mmITRepositoryWrapper = mmITRepositoryWrapper;
            _mmITUnitOfWork = mmITUnitOfWork;
            _masterDataUnitOfWork = masterDataUnitOfWork;
            _masterDataRepositoryWrapper = masterDataRepositoryWrapper;
            _mmOfflineRepositoryWrapper = mmOfflineRepositoryWrapper;
            _mmOnlineRepositoryWrapper = mmOnlineRepositoryWrapper;
            _redisConfigs = redisConfigs;
            _sterlingOrderClientService = sterlingOrderClientService;
            _redisDistributedAdapter = redisDistributedAdapter;
            _mmOnlineUnitOfWork = mmOnlineUnitOfWork;
        }

        #region PUBLIC_METHODS
        public string GetDecimalJsonFormat(decimal input)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);

            return string.Format("{0:c}", Convert.ToDecimal(input)).Replace("$", "").Replace(",", "");
        }
        private string NumberToWriting(string tutar)
        {
            var sTutarTemp = Convert.ToDecimal(tutar);

            string sTutar = sTutarTemp.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
            string lira = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" };
            string[] onlar = { "", "On", "Yirmi", "Otuz", "Kırk", "Elli", "Altmış", "Yetmiş", "Seksen", "Doksan" };
            string[] binler = { "Katrilyon", "Trilyon", "Milyar", "Milyon", "Bin", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
                                //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            lira = lira.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.            

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "Yüz"; //yüzler                

                if (grupDegeri == "BirYüz") //biryüz düzeltiliyor.
                    grupDegeri = "Yüz";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler                

                if (grupDegeri != "") //binler
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "BirBin") //birbin düzeltiliyor.
                    grupDegeri = "Bin";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " TL ";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0") //kuruş onlar
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0") //kuruş birler
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " Kr";
            //else
            //yazi += "Sıfır Kr";

            return yazi;
        }

        #endregion
        public async Task<ServiceResultModel<CreateInvoiceByCustomerOrderNumberResponseModel>> SendDataToFOMByCustomerOrderNumbersAsync(CreateInvoiceByCustomerOrderNumberRequestModel model)
        {

            var response = new CreateInvoiceByCustomerOrderNumberResponseModel();
            var customerOrderNumbers = model.CustomerOrderNumbers.Split(",");

            foreach (var customerOrderNumber in customerOrderNumbers)
            {
                try
                {
                    var redisCategory = _redisConfigs.Value.FOMRedisSettings.RedisCategory;
                    var rawData = _redisConfigs.Value.FOMRedisSettings.RawData;

                    var firstData = _redisConfigs.Value.FOMRedisSettings.FirstData;

                    var url = "customer-order/customer-orders?order_number=" + customerOrderNumber + "&limit=1";
                    var result = await _sterlingOrderClientService.GetCustomerOrdersAsync(url);

                    try
                    {
                        if (result.Data != null)
                        {
                            if (result.Data.customer_orders.Any())
                            {

                                foreach (var customerOrderItem in result.Data.customer_orders)
                                {
                                    var key = _redisConfigs.Value.FOMRedisSettings.RedisCategory + "_" + customerOrderItem.number + "_" + Guid.NewGuid();
                                    var redisModel = new CustomerOrderRedisModel()
                                    {
                                        CustomerOrder = customerOrderItem,
                                        Key = key,
                                        CreatedDate = DateTime.Now
                                    };

                                    var changeRedisState = _redisDistributedAdapter.SetRedisData<CustomerOrderRedisModel>("", redisCategory, firstData, redisModel, 0);
                                    if (!changeRedisState)
                                    {
                                        _logger.LogError($"<MMITService>CreateInvoiceByCustomerOrderNumbersAsync:{JsonSerializer.Serialize(result)}</MMITService>");

                                    }
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = "Hata:" + ex.Message;

                        _logger.LogError($"<MMITService>CreateInvoiceByCustomerOrderNumbersAsync:{ex.Message}{ex.InnerException}{JsonSerializer.Serialize(result)}</MMITService>");

                        return Result(response);
                    }

                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Hata:" + ex.Message;
                    return Result(response);
                }
            }

            response.IsSuccess = true;
            response.Code = 1000;
            response.Message = "Başarılı!";

            return Result(response);
        }
    }
}
