﻿using Microsoft.EntityFrameworkCore;
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
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    string connectionString = configuration.GetConnectionString("comfyDbSqlConnectionString");
                    options.UseSqlServer(connectionString, o => o.MigrationsAssembly("Comfy.Repository.Db.SQL.Migrations"));
                });

            services
                .AddScoped<ApplicationDbContext>()
                .AddScoped<DbContext>((x) => x.GetService<ApplicationDbContext>())
                .AddScoped<IUnitOfWorkFactory<UnitOfWork>, UnitOfWorkFactory>();
        }
    }
}
