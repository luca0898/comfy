using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace Comfy.Registers.Swagger
{
    public class SwaggerRegister
    {
        public static void Load(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Comfy API",
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
