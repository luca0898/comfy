using Comfy.API.Registers.Cache;
using Comfy.Registers.Contracts.Repositories;
using Comfy.Registers.Contracts.Services;
using Comfy.Registers.DataBases;
using Comfy.Registers.Mapping;
using Comfy.Registers.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Comfy.Registers
{
    public static class LoadRegistrations
    {
        public static void ConfigureContainers(IServiceCollection services, IConfiguration configuration)
        {
            DbSQL.Load(services, configuration);
            RedisCacheRegister.Load(services, configuration);
            AuthenticationRegister.Load(services, configuration);
            SwaggerRegister.Load(services, configuration);

            AutoMapperLoadProfiles.Load(services);
            RegistryServices.Load(services);
            RegistryRepositories.Load(services);
        }
    }
}
