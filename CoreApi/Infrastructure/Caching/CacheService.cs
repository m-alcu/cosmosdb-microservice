using CoreApi.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace CoreApi.Infrastructure.Caching;

public class CacheService : ICacheService
{

    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();

    private readonly IDistributedCache _distributedCache;
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        string? cachedValue = await _distributedCache.GetStringAsync(
            key,
            cancellationToken);

        if (cachedValue is null)
        {
            return null;
        }

        T? value = JsonConvert.DeserializeObject<T>(cachedValue);

        return value;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {

        await _distributedCache.RemoveAsync(key, cancellationToken);

        CacheKeys.TryRemove(key, out bool _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        //foreach (string key in cachekeys.keys)
        //{
        //    if (key.startswith(prefixkey))
        //    {
        //        await removeasync(key, cancellationtoken);
        //    }
        //}

        IEnumerable<Task> tasks = CacheKeys
            .Keys
            .Where(k => k.StartsWith(prefixKey))
            .Select(k => RemoveAsync(k, cancellationToken));

        //paralel execution normally
        await Task.WhenAll(tasks);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        string cacheValue = JsonConvert.SerializeObject(value);

        await _distributedCache.SetStringAsync(key, cacheValue, cancellationToken);

        CacheKeys.TryAdd(key, false);
    }
}