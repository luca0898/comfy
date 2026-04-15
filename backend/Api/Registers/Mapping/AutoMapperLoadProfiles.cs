using Api.Registers.Mapping.Profiles;

namespace Api.Registers.Mapping;

public static class AutoMapperLoadProfiles
{
    public static void Load(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AllowNullDestinationValues = true;
            config.AllowNullCollections = true;

            config.AddProfile<AppointmentProfile>();
        });
    }
}