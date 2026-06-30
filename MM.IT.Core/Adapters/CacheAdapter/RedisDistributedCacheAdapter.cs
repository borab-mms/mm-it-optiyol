using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Configs;
using MM.IT.Core.Adapters.CacheAdapter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.CacheAdapter;

/// <summary>
/// IDistributedCacheAdapter şartlarını barındıran RedisDistributedCacheAdapter Nesnesi
/// </summary>
public class RedisDistributedCacheAdapter : IDistributedCacheAdapter
{
    private readonly IDistributedCache _distributedCache;
    private readonly IRedisDatabaseProvider _redisDatabaseProvider;
    private readonly IOptions<RedisConfigModel> _redisConfigs;

    public RedisDistributedCacheAdapter(IDistributedCache distributedCache,
        IRedisDatabaseProvider redisDatabaseProvider,
        IOptions<RedisConfigModel> redisConfigs)
    {
        _distributedCache = distributedCache;
        _redisDatabaseProvider = redisDatabaseProvider;
        _redisConfigs = redisConfigs;
    }

    public string GetString(string key)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        return isConnected ? _distributedCache.GetString(key) : null;
    }

    public async Task<string> GetStringAsync(string key, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        return isConnected ? await _distributedCache.GetStringAsync(key, token) : null;
    }

    public byte[] Get(string key)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        return isConnected ? _distributedCache.Get(key) : null;
    }

    public T Get<T>(string key)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        return isConnected ? _distributedCache.Get(key).ContertTo<T>() : default;
    }

    public async Task<byte[]> GetAsync(string key, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        return isConnected ? await _distributedCache.GetAsync(key, token) : null;
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        var data = isConnected ? await _distributedCache.GetAsync(key, token) : null;
        return data.ContertTo<T>();
    }

    public void Refresh(string key)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            _distributedCache.Refresh(key);
        }

    }

    public async Task RefreshAsync(string key, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            await _distributedCache.RefreshAsync(key, token);
        }

    }

    public void Remove(string key)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            _distributedCache.Remove(key);
        }

    }

    public void RemoveByPattern(string searchPattern)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            var keys = _redisDatabaseProvider.GetKeysByPattern(searchPattern);
            foreach (var key in keys)
            {
                Remove(key.ToString().Replace($"{_redisConfigs.Value.Name}.", string.Empty));
            }
        }
    }

    public async Task RemoveByPatternAsync(string searchPattern)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            var keys = _redisDatabaseProvider.GetKeysByPattern(searchPattern);
            foreach (var key in keys)
            {
                await RemoveAsync(key);
            }
        }
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            await _distributedCache.RemoveAsync(key, token);
        }

    }

    public void SetString(string key, string value, DistributedCacheEntryOptions options)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            _distributedCache.SetString(key, value, options);
        }
    }

    public async Task SetStringAsync(string key, string value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            await _distributedCache.SetStringAsync(key, value, options, token);
        }
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            _distributedCache.Set(key, value, options);
        }
    }

    public void Set<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            _distributedCache.Set(key, value.ConvertToByteArray(), options);
        }
    }

    public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            await _distributedCache.SetAsync(key, value, token);
        }
    }

    public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        var isConnected = _redisDatabaseProvider.GetConnection().IsConnected;
        if (isConnected)
        {
            await _distributedCache.SetAsync(key, value.ConvertToByteArray(), options, token);
        }
    }
}