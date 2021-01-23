using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Comfy.Db.SQL;
using Comfy.SystemObjects.Interfaces;
using Comfy.SystemObjects;
using Comfy.Repository.Db.SQL;

namespace Comfy.Registers.DataBases
{
    public class DbSQL
    {
        public static void Load(IServiceCollection services)
        {
            services
                .AddScoped<ApplicationDbContext>()
                .AddScoped<DbContext>((x) => x.GetService<ApplicationDbContext>())
                .AddScoped<IUnitOfWorkFactory<UnitOfWork>, UnitOfWorkFactory>();
        }
    }
}
