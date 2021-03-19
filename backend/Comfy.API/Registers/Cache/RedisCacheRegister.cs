using Comfy.Cache.Redis;
using Comfy.SystemObjects.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Comfy.API.Registers.Cache
{
    public static class RedisCacheRegister
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(setup =>
            {
                setup.Configuration = configuration.GetConnectionString("comfyRedisCache");
            });

            services.AddSingleton<ICacheProvider, RedisCacheProvider>();
        }
    }
}
