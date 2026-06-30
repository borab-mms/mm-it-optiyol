using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.CacheAdapter.Interfaces;

/// <summary>
/// Redis Database Erişimi için Tanımlanan Interface
/// </summary>
public interface IRedisDatabaseProvider
{
    /// <summary>
    /// Connection Bilgisini Döndürür.
    /// </summary>
    /// <returns>IConnectionMultiplexer</returns>
    IConnectionMultiplexer GetConnection();

    /// <summary>
    /// Gönderilen Pattern bilgisine göre Redis sunucusundaki key'leri döndürür.
    /// </summary>
    /// <param name="searchPattern">String</param>
    /// <returns>RedisKey List</returns>
    IEnumerable<RedisKey> GetKeysByPattern(string searchPattern);
}