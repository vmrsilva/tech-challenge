﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TechChallange.Domain.Cache;

namespace TechChallange.Infrastructure.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CacheRepository(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(300),
                SlidingExpiration = TimeSpan.FromSeconds(1200),
            };
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            var redisValue = await _cache.GetStringAsync(key).ConfigureAwait(false);

            return !string.IsNullOrEmpty(redisValue) ? Deserialize<T>(redisValue) : default;
        }

        public async Task<T> GetValueAsync<T>(string key, Func<Task<T>> producer)
        {
            try
            {
                var t = await GetValueAsync<T>(key);

                if (t != null)
                    return t;

                t = await producer();

                if (t != null)
                    await SetValueAsync(key, t);

                return t;

            }
            catch (Exception)
            {
                return await producer();
            }
        }

        public async Task SetValueAsync<T>(string key, T t)
        {
            var json = Serialize(t);

            await _cache.SetStringAsync(key, json);
        }

        private static T Deserialize<T>(string json)
        {
            return json == null ? default : JsonConvert.DeserializeObject<T>(json);
        }

        private static string Serialize<T>(T t)
        {
            return t == null ? null : JsonConvert.SerializeObject(t);
        }
    }
}