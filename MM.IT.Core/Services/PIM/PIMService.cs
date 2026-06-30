using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MM.IT.Common.Enums;
using MM.IT.Common.Helpers.PIMHelper;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.PIM;
using MM.IT.Common.Models.Sms;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.PIM.Interfaces;
using MM.IT.Core.Services.SMS.Interfaces;
using MM.IT.Data.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.PIM
{
    public class PIMService : BaseService, IPIMService
    {
        private readonly ILogger<PIMService> _logger;
        private readonly IMediaMarktITRepositoryWrapper _mediaMarktITRepositoryWrapper;
        public PIMService(IServiceProvider serviceProvider
            , IMediaMarktITRepositoryWrapper mediaMarktITRepositoryWrapper
            , ILogger<PIMService> logger) : base(serviceProvider)
        {
            _mediaMarktITRepositoryWrapper = mediaMarktITRepositoryWrapper;
            _logger = logger;
        }

        public async Task<ServiceResultModel<List<ProductSummaryModel>>> MasterDataPIM()
        {
            try
            {
                List<ProductSummaryModel> productSummaryModels = new List<ProductSummaryModel>();
                try
                {
                    List<List<int>> products = new List<List<int>>();
                    try
                    {
                        var xmlDatas = PIMXMLHelper.GetXMLData("https://storage.crwizard.com/175423cff950/e23d2441a86c/991eff6a696d_MasterData.xml");


                        for (int i = 0; i < xmlDatas.channel.items.Count; i = i + 50)
                        {
                            var items = xmlDatas.channel.items.Skip(i).Take(50);
                            List<int> productItems = new List<int>();
                            foreach (var item in items)
                            {
                                productItems.Add(Convert.ToInt32(item.article));
                            }
                            products.Add(productItems);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"<PIMService>xmlDatas:{ex.Message}</PIMService>");
                    }

                    foreach (var item in products)
                    {
                    var mMProducts = await _mediaMarktITRepositoryWrapper.MMProductsRepository
                               .GetQuery()
                               .AsNoTracking()
                               .Where(a => item.Contains((int)a.ProductId))
                               .ToListAsync();

                        if (mMProducts.Any())
                        {
                            foreach (var mMProductItem in mMProducts)
                            {
                                var contentJson = mMProductItem.ContentJSon;
                                if (!string.IsNullOrEmpty(contentJson))
                                {
                                    try
                                    {
                                        var featureAndSubFeatureModel = JsonConvert.DeserializeObject<FeatureAndSubFeatureModel>(contentJson);

                                        ProductSummaryModel productSummaryModel = new ProductSummaryModel();

                                        productSummaryModel.Id = (int)mMProductItem.ProductId;
                                        productSummaryModel.Name = mMProductItem.ProductName;
                                        productSummaryModel.DescriptionShort = mMProductItem.DescriptionShort;
                                        productSummaryModel.DescriptionLong = mMProductItem.DescriptionLong;
                                        productSummaryModel.PG = (mMProductItem.PG != null || mMProductItem.PG != default) ? (int)mMProductItem.PG : 0;
                                        productSummaryModel.MPG = (mMProductItem.MPG != null || mMProductItem.MPG != default) ? (int)mMProductItem.MPG : 0;
                                        productSummaryModel.SalesPrice = mMProductItem.SalesPrice;
                                        productSummaryModel.Barcode = mMProductItem.Barcode;

                                        List<ProductFeatureItem> productFeatureItems = new List<ProductFeatureItem>();
                                        if (featureAndSubFeatureModel.Features.Any())
                                        {
                                            foreach (var featureIdItem in featureAndSubFeatureModel.Features)
                                            {
                                                ProductFeatureItem productFeatureItem = new ProductFeatureItem();

                                                productFeatureItem.FeatureId = featureIdItem.FeatureId;
                                                productFeatureItem.FeatureName = featureIdItem.FeatureName;
                                                productFeatureItem.SubFeatures = featureIdItem.SubFeatures
                                                    .Select(a => new ProductSubFeaturesItem()
                                                    {
                                                        SubFeatureId = a.SubFeatureId,
                                                        SubFeatureName = a.SubFeatureName,
                                                        SubFeatureValue = a.SubFeatureValue
                                                    }).ToList();
                                                productFeatureItems.Add(productFeatureItem);
                                            }

                                        }
                                        productSummaryModel.ProductFeatureItems = productFeatureItems;
                                        productSummaryModels.Add(productSummaryModel);

                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Result<List<ProductSummaryModel>>(null, "", 400);
                }

                return Result(productSummaryModels,"Başarılı");

            }
            catch (Exception ex)
            {

                _logger.LogError($"<PIMService>MasterDataPIM:{ex.Message}</PIMService>");

                return Result<List<ProductSummaryModel>>(null, ex.Message, 404);
            }
        }
    }
}
