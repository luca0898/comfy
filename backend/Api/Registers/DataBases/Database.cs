using CrossCutting.Interfaces;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Registers.DataBases;

public static class Database
{
    public static void Load(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("comfyDbSqlConnectionString")));
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
    }
}