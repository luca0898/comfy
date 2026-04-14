using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace Comfy.SystemObjects.Interfaces
{
    public interface ICacheProvider
    {
        Task<T> FindCacheAsync<T>(string key, CancellationToken cancellationToken = default);
        Task CreateAsync<T>(string key, T data, DistributedCacheEntryOptions options = default, CancellationToken cancellationToken = default);
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
        Task InvalidateCacheAsync<T>();
    }
}
