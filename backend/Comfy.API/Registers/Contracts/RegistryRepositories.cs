using Comfy.PRODUCT.Contracts.Repositories;
using Comfy.REPOSITORIES;
using Microsoft.Extensions.DependencyInjection;

namespace Comfy.Registers.Contracts.Repositories
{
    public class RegistryRepositories
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
        }
    }
}
