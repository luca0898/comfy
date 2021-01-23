using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Comfy.Registers.Contracts.Repositories;
using Comfy.Registers.Contracts.Services;
using Comfy.Registers.DataBases;
using Comfy.Registers.Mapping;

namespace Comfy.Registers
{
    public static class LoadRegistrations
    {
        public static void ConfigureContainers(IServiceCollection services)
        {
            AutoMapperLoadProfiles.Load(services);
            DbSQL.Load(services);
            RegistryServices.Load(services);
            RegistryRepositories.Load(services);
        }
    }
}
