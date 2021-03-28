using Comfy.Db.SQL.Repositories;
using Comfy.Product.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Comfy.Registers.Contracts.Repositories
{
    public class RegistryRepositories
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
