using CoreApi.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace CoreApi.Infrastructure.Caching;

public class CacheService : ICacheService
{

    private readonly IDistributedCache _distributedCache;
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T> SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        throw new NotImplementedException();
    }
}