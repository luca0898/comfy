using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comfy.PRODUCT.Contracts.Services;
using Comfy.Service;

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
