using Api.Registers.Authentication;
using Api.Registers.Contracts;
using Api.Registers.Mapping;
using Api.Registers.Validators;

namespace Api.Registers;

public static class LoadRegistrations
{
    public static void ConfigureContainers(IServiceCollection services, IConfiguration configuration)
    {
        LoadValidators.Load(services);
        DataBases.Database.Load(services, configuration);
        AuthenticationRegister.Load(services, configuration);
        AutoMapperLoadProfiles.Load(services);
        RegistryServices.Load(services);
        RegistryRepositories.Load(services);
    }
}