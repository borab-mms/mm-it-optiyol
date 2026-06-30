using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Enums;
using MM.IT.Common.Helpers.RedisHelper;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.Sterling;
using MM.IT.Core.Adapters.RedisAdaptor.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.RedisAdaptor
{
    public class RedisAdapter : IRedisAdapter
    {
        private PubSubHelper _pubSubHelper;
        private readonly ILogger<RedisAdapter> _logger;
        private readonly IOptions<RedisConfigModel> _redisConfigs;
        public static string ApplicationCode = "MediaMarktIT";
        public RedisAdapter(IOptions<RedisConfigModel> redisConfigs
            , ILogger<RedisAdapter> logger)
        {
            _redisConfigs = redisConfigs;
            _logger = logger;
        }

        public void RedisConnect()
        {
            PubSubHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        }
        public bool IsRedisConnect()
        {
            var result = PubSubHelper.IsRedisConnect();

            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<ScanResultModel> ScanAsync(string pattern, int pageSize)
        {
            var cursor = 0;
            var keys = new List<string>();

            do
            {
                PubSubHelper.CheckRedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);

                var scanResult = await PubSubHelper.ExecuteScanAsync(cursor, pattern, 100);

                if (scanResult != null)
                {
                    var scanResultArray = (RedisResult[])scanResult;

                    cursor = Convert.ToInt32((string)scanResultArray[0]); // Yeni cursor değeri
                    var currentKeys = (RedisKey[])scanResultArray[1];  // Eşleşen anahtarlar

                    keys.AddRange(currentKeys.Select(k => k.ToString()));
                }

            } while (cursor != 0); // Cursor sıfır olduğunda tüm anahtarlar alınmış demektir

            return new ScanResultModel
            {
                Keys = keys,
                Cursor = cursor
            };
        }
        public async Task<List<CustomerOrderRedisModel>> ScanAndGetListsAsync(string pattern, int keyBatchSize = 100, int listPageSize = 500)
        {
            var allOrders = new List<CustomerOrderRedisModel>();

            try
            {

                var keys = await PubSubHelper.ExecuteKeysAsync(pattern: pattern, keyBatchSize: keyBatchSize);

                var tasks = keys.Select(async key =>
                {
                    var orders = await GetPagedListDataAsync(key, listPageSize);
                    lock (allOrders) // Thread safe ekleme
                    {
                        allOrders.AddRange(orders);
                    }
                });

                await Task.WhenAll(tasks);

                _logger.LogInformation($"Total {allOrders.Count} items fetched.");

                return allOrders;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while scanning Redis: {ex.Message}");
                throw;
            }
        }
        private async Task<List<CustomerOrderRedisModel>> GetPagedListDataAsync(RedisKey key, int pageSize)
        {
            var results = new List<CustomerOrderRedisModel>();
            try
            {
                long listLength = await PubSubHelper.ExecuteListLengthAsync(key);
                _logger.LogInformation($"Key {key} has {listLength} items.");

                for (long i = 0; i < listLength; i += pageSize)
                {
                    long end = Math.Min(i + pageSize - 1, listLength - 1);

                    var rangeItems = await PubSubHelper.ExecuteListRangeAsync(key, i, end);

                    foreach (var item in rangeItems)
                    {
                        var deserialized = JsonConvert.DeserializeObject<CustomerOrderRedisModel>(item);
                        if (deserialized != null)
                        {
                            results.Add(deserialized);
                        }
                    }

                    _logger.LogInformation($"Fetched {rangeItems.Count()} items from {key} [{i}-{end}]");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching list data from {key}: {ex.Message}");
                throw;
            }

            return results;
        }
    }
}
