using Comfy.SystemObjects.Exceptions;
using Comfy.SystemObjects.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.Cache.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheProvider> _logger;

        public RedisCacheProvider(IDistributedCache cache, ILogger<RedisCacheProvider> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task CreateAsync<T>(string key, T data, DistributedCacheEntryOptions options = default, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(key) || data == null)
            {
                throw new ComfyApplicationException("Unable to create the cache entry because the arguments are null");
            }

            options ??= new DistributedCacheEntryOptions();

            string dataAsJson = JsonConvert.SerializeObject(data);

            byte[] dataAsBytesArray = Encoding.UTF8.GetBytes(dataAsJson);

            await _cache.SetAsync(key, dataAsBytesArray, options, cancellationToken);
            _logger.LogInformation("Cache was created. {EntityName}{CacheKey}{Options}", typeof(T).Name, key, options);

            await SaveKeyInTheSummaryAsync<T>(key);
        }

        public async Task<T> FindCacheAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Querying cached data for the \"{CACHE_KEY}\" key", key);
            string cache = await _cache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cache))
            {
                _logger.LogInformation("Cache not found. {key}", key);
                return default;
            }

            T dataDeserialized = JsonConvert.DeserializeObject<T>(cache);

            return dataDeserialized;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }

        public async Task InvalidateCacheAsync<T>()
        {
            string summary = $"Summary:{typeof(T).Name}";

            string summaryAsString = await _cache.GetStringAsync(summary);

            if (string.IsNullOrEmpty(summaryAsString) == false)
            {
                List<string> listOfKeysInTheSummary = JsonConvert.DeserializeObject<List<string>>(summaryAsString);

                foreach (var key in listOfKeysInTheSummary)
                {
                    await _cache.RemoveAsync(key);
                }

                await _cache.RemoveAsync(summary);
                _logger.LogInformation("Summary \"{SUMMARY_KEYS}\" was deleted with {AMOUNT} keys", summary, listOfKeysInTheSummary.Count);
            }
        }

        private async Task SaveKeyInTheSummaryAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogInformation("The key cannot be saved because it is empty");
                return;
            }

            string summary = GetSummaryKey<T>();

            string summaryAsString = await _cache.GetStringAsync(summary);

            string summaryAsJson;

            if (string.IsNullOrEmpty(summaryAsString) == false)
            {
                List<string> listOfKeysInTheSummary = JsonConvert.DeserializeObject<List<string>>(summaryAsString);

                listOfKeysInTheSummary.Add(key);

                summaryAsJson = JsonConvert.SerializeObject(listOfKeysInTheSummary);
            }
            else
            {
                summaryAsJson = JsonConvert.SerializeObject(new List<string> { key });
            }

            await _cache.SetAsync(summary, Encoding.UTF8.GetBytes(summaryAsJson), new DistributedCacheEntryOptions
            {
                SlidingExpiration = DateTime.Now.AddHours(1).TimeOfDay
            });
        }

        private string GetSummaryKey<T>()
        {
            Type type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return $"Summary:{type.GetGenericArguments()[0].Name}";
            }
            else
            {
                return $"Summary:{type.Name}";
            }
        }
    }
}
