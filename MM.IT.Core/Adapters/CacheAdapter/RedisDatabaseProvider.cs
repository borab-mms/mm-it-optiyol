using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Core.Adapters.CacheAdapter.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.CacheAdapter;

/// <summary>
/// Redis Database Erişimi Sağlayan Nesne
/// </summary>
public class RedisDatabaseProvider : IRedisDatabaseProvider
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IOptions<RedisConfigModel> _redisConfigs;

    /// <summary>
    /// Constructor Injection
    /// </summary>
    /// <param name="redisConfigs"></param>
    public RedisDatabaseProvider(IOptions<RedisConfigModel> redisConfigs)
    {
        _redisConfigs = redisConfigs;

        var options = ConfigurationOptions.Parse($"{redisConfigs.Value.Host}:{redisConfigs.Value.Port},password={redisConfigs.Value.Password}");
        options.AbortOnConnectFail = false;
        _connectionMultiplexer = ConnectionMultiplexer.Connect(options);
    }

    /// <summary>
    /// Connection Bilgisini Döndürür.
    /// </summary>
    /// <returns>IConnectionMultiplexer</returns>
    public IConnectionMultiplexer GetConnection()
    {
        return _connectionMultiplexer;
    }

    /// <summary>
    /// Gönderilen Pattern bilgisine göre Redis sunucusundaki key'leri döndürür.
    /// </summary>
    /// <param name="searchPattern">String</param>
    /// <returns>RedisKey List</returns>
    public IEnumerable<RedisKey> GetKeysByPattern(string searchPattern)
    {
        return _connectionMultiplexer.GetServer($"{_redisConfigs.Value.Host}:{_redisConfigs.Value.Port}")
            .Keys(database: _connectionMultiplexer.GetDatabase().Database, pattern: $"{_redisConfigs.Value.Name}.{searchPattern}")
            .ToArray();
    }
}
