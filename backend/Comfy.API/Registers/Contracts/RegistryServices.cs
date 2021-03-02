using Comfy.Product.Contracts.Services;
using Comfy.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Comfy.Registers.Contracts.Services
{
    public class RegistryServices
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<IScheduleService, ScheduleService>();
        }
    }
}
