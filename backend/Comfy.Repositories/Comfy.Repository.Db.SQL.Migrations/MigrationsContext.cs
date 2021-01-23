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

            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Console.WriteLine($"\nEnvironment Name:{environmentName}\n");

            builder
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{environmentName}.json"), false, true)
                .AddEnvironmentVariables();

            IConfigurationRoot config = builder.Build();

            optionsBuilder.UseSqlServer(config["Data:DefaultConnection:ConnectionString"]);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
