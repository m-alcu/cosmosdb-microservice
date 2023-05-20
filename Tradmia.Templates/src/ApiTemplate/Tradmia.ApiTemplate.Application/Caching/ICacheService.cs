﻿namespace Tradmia.ApiTemplate.Application.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) where T : class;

    }
}