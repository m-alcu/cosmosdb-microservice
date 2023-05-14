﻿using CoreApi.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace CoreApi.Infrastructure.Caching;

public class CacheService : ICacheService
{

    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();

    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) where T : class
    {

        string? cacheValue = await _distributedCache.GetStringAsync(
            key,
            cancellationToken);

        T? result;
        if (string.IsNullOrEmpty(cacheValue))
        {
            result = await factory();

            if (result is null)
            {
                return result;
            }

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(result),
                cancellationToken);

            return result;
        }

        result = JsonConvert.DeserializeObject<T>(cacheValue);

        return result;

    }
}