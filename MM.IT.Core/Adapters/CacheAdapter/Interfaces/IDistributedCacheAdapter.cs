using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.CacheAdapter.Interfaces;

/// <summary>
/// DistributedCache Adapter Interface Tanımı
/// </summary>
public interface IDistributedCacheAdapter : IDistributedCache
{
    /// <summary>
    /// String key bilgisi alarak Cache'den string tipinde data döner.
    /// </summary>
    /// <param name="key">String: Cache key</param>
    /// <returns>String data</returns>
    string GetString(string key);

    /// <summary>
    /// Async : String key bilgisi alarak Cache'den string tipinde data döner.
    /// </summary>
    /// <param name="key">String: Cache key</param>
    /// <param name="token">CancellationToken: token</param>
    /// <returns>Async: string data</returns>
    Task<string> GetStringAsync(string key, CancellationToken token = default);

    /// <summary>
    /// String key bilgisi alarak Cache'den Generic T tipinde data döner.
    /// </summary>
    /// <typeparam name="T">T: Cache'de tutulan data tipi</typeparam>
    /// <param name="key">String: Cache key</param>
    /// <returns>T: T tipinde data</returns>
    T Get<T>(string key);

    /// <summary>
    /// Async: String key bilgisi alarak Cache'den Generic T tipinde data döner.
    /// </summary>
    /// <typeparam name="T">T: Cache'de tutulan data tipi</typeparam>
    /// <param name="key">String: Cache key</param>
    /// <param name="token">CancellationToken: token</param>
    /// <returns>Async: T: T tipinde data</returns>
    Task<T> GetAsync<T>(string key, CancellationToken token = default);

    /// <summary>
    /// Key, value ve options bilgilerini alarak Cache'de string tipinde data saklar.
    /// </summary>
    /// <param name="key">String: Cache key</param>
    /// <param name="value">String: Cache data</param>
    /// <param name="options">DistributedCacheEntryOptions: Cache ayarları</param>
    void SetString(string key, string value, DistributedCacheEntryOptions options);

    /// <summary>
    /// Async: Key, value ve options bilgilerini alarak Cache'de string tipinde data saklar.
    /// </summary>
    /// <param name="key">String: Cache key</param>
    /// <param name="value">String: Cache data</param>
    /// <param name="options">DistributedCacheEntryOptions: Cache ayarları</param>
    /// <param name="token">CancellationToken: token</param>
    /// <returns>Async işlem bilgisi</returns>
    Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options, CancellationToken token = default);

    /// <summary>
    /// Key, value ve options bilgilerini alarak Cache'de Generic T tipinde data saklar.
    /// </summary>
    /// <typeparam name="T">T: Cache'de tutulan data tipi</typeparam>
    /// <param name="key">String: Cache key</param>
    /// <param name="value">T: Generic T tipinde Cache data</param>
    /// <param name="options">DistributedCacheEntryOptions: Cache ayarları</param>
    void Set<T>(string key, T value, DistributedCacheEntryOptions options);

    /// <summary>
    /// Async: Key, value ve options bilgilerini alarak Cache'de Generic T tipinde data saklar.
    /// </summary>
    /// <typeparam name="T">T: Cache'de tutulan data tipi</typeparam>
    /// <param name="key">String: Cache key</param>
    /// <param name="value">T: Generic T tipinde Cache data</param>
    /// <param name="options">DistributedCacheEntryOptions: Cache ayarları</param>
    /// <param name="token">CancellationToken: token</param>
    /// <returns>Async işlem bilgisi</returns>
    Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default);

    /// <summary>
    /// Pattern Bilgisi Alarak Cache'deki datayı temizler.
    /// </summary>
    /// <param name="searchPattern">String: Pattern</param>
    void RemoveByPattern(string searchPattern);

    /// <summary>
    /// Async: Pattern Bilgisi Alarak Cache'deki datayı temizler.
    /// </summary>
    /// <param name="searchPattern">String: Pattern</param>
    Task RemoveByPatternAsync(string searchPattern);
}
