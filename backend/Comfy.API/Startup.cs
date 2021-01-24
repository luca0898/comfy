using Comfy.Middlewares;
using Comfy.Registers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Comfy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = true;
            });

            LoadRegistrations.ConfigureContainers(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            app.UseAuthentication();

            app.UseCors((builder) =>
            {
                builder.WithOrigins(Configuration["CorsAllowedHosts"].Split(','));
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
            });

            app.ConfigureExceptionHandler(logger);

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.StatusCode = 200;
                    return Task.CompletedTask;
                });

                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId(Configuration["authenticationSettings:KeycloakSettings:ClientId"]);
                c.OAuthClientSecret(Configuration["authenticationSettings:KeycloakSettings:ClientSecret"]);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comfy API V1");
            });
        }
    }
}
