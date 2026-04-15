using Database.Repositories;
using Domain.Contracts.Repositories;

namespace Api.Registers.Contracts;

public static class RegistryRepositories
{
    public static void Load(IServiceCollection services)
    {
        services.AddTransient<IAppointmentRepository, AppointmentRepository>();
    }
}