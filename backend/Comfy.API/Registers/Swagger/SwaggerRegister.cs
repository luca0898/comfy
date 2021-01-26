using Comfy.SystemObjects.Entities.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Comfy.Registers.Swagger
{
    public class SwaggerRegister
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            AuthenticationSettings authenticationSettings = new AuthenticationSettings();
            configuration.Bind(nameof(authenticationSettings), authenticationSettings);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Comfy API",
                });

                string url = authenticationSettings.KeycloakSettings.BaseUrl;
                string realm = authenticationSettings.KeycloakSettings.Realm;

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{url}/auth/realms/{realm}/protocol/openid-connect/token"),
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new string[] { }
                    }
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
