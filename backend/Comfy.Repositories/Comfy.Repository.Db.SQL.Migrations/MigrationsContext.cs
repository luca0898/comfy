using Comfy.Db.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Comfy.Repository.Db.SQL.Migrations
{
    public class MigrationsContext : ApplicationDbContext
    {
        public MigrationsContext() : base(new DbContextOptions<ApplicationDbContext>())
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();

            var currentDirectory = Directory.GetCurrentDirectory();

            builder.AddJsonFile(Path.Combine(currentDirectory, $"appsettings.json"), false, true);

            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(environmentName) == false){
                
                Console.WriteLine($"\nEnvironment Name:{environmentName}\n");

                builder.AddJsonFile(Path.Combine(currentDirectory, $"appsettings.{environmentName}.json"), true, true);
            }

            IConfigurationRoot config = builder
                .AddEnvironmentVariables()
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("comfyDbSqlConnectionString"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
