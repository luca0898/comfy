using Comfy.Cache.Redis;
using Comfy.SystemObjects.Exceptions;
using Comfy.SystemObjects.Interfaces;
using Comfy.Tests.Unit.Stubs;
using FakeItEasy;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Comfy.Tests.Unit.Cache
{
    public class RedisCacheProviderTests
    {
        ICacheProvider _cacheProvider;

        IDistributedCache _cache;
        ILogger<RedisCacheProvider> _logger;

        [SetUp]
        public void Setup()
        {
            _cache = A.Fake<IDistributedCache>();
            _logger = A.Fake<ILogger<RedisCacheProvider>>();

            _cacheProvider = new RedisCacheProvider(_cache, _logger);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateCacheAsync()
        {
            string key = $"Anonimo:{ typeof(MyStubClass).Name }:2021-03-19:";
            MyStubClass myStubClass = A.Fake<MyStubClass>();
            string dataAsJson = JsonConvert.SerializeObject(key);
            byte[] dataAsBytesArray = Encoding.UTF8.GetBytes(dataAsJson);

            await _cacheProvider.CreateAsync(key, myStubClass, null, default);

            A.CallTo(() => _cache.SetAsync(key, dataAsBytesArray, null, default))
                .WithAnyArguments()
                .MustHaveHappened();
        }

        [Test]
        public void CreateAsync_ShouldNotCreateCache_WithoutValidKey()
        {
            string key = ""; // invalid key
            MyStubClass myStubClass = null;
            string expectedErrorMessage = "Unable to create the cache entry because the arguments are null";

            ComfyApplicationException ex = Assert.ThrowsAsync<ComfyApplicationException>(
                () => _cacheProvider.CreateAsync(key, myStubClass, null, default)
            );

            Assert.AreEqual(expectedErrorMessage, ex.Message);
        }

        [Test]
        public async Task FindCacheAsync_ShouldSearchForCache_UsingTheCacheKeyAsync()
        {
            string key = $"Anonimo:{ typeof(MyStubClass).Name }:2021-03-19:";

            await _cacheProvider.FindCacheAsync<MyStubClass>(key);
        }

        [Test]
        public async Task RemoveAsync_ShouldRemoveCache_UsingTheCacheKeyAsync()
        {
            string key = $"Anonimo:{ typeof(MyStubClass).Name }:2021-03-19:";

            await _cacheProvider.RemoveAsync(key);
        }

        [Test]
        public async Task InvalidateCacheAsync_ShouldRemoveEachCacheKey_InTheSummaryAsync()
        {
            string key = $"Summary:{ typeof(MyStubClass).Name }";

            await _cacheProvider.InvalidateCacheAsync<MyStubClass>();
        }
    }
}
