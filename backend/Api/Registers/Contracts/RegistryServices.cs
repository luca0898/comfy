using Domain.Contracts.Services;

namespace Api.Registers.Contracts;

public static class RegistryServices
{
    public static void Load(IServiceCollection services)
    {
        services.AddScoped<IAppointmentService, IAppointmentService>();
    }
}