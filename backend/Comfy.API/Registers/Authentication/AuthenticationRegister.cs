using Comfy.Product.Contracts.Services;
using Comfy.Product.Entities;
using Comfy.SystemObjects.Entities.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Comfy.Registers.Authentication
{
    public static class AuthenticationRegister
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            AuthenticationSettings authenticationSettings = new AuthenticationSettings();
            configuration.Bind(nameof(authenticationSettings), authenticationSettings);
            services.AddSingleton(authenticationSettings);

            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.Authority = authenticationSettings.KeycloakSettings.Authority;
                    o.Audience = authenticationSettings.KeycloakSettings.Audience;

                    RSA publicRsa = RSA.Create();
                    publicRsa.FromXmlString(
                        File.ReadAllText(
                            Path.Combine(Directory.GetCurrentDirectory(), "Cert", authenticationSettings.PublicKey))
                        );

                    RsaSecurityKey signingKey = new RsaSecurityKey(publicRsa);
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = signingKey,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = authenticationSettings.KeycloakSettings.Issuer,
                        ValidateIssuer = true,

                        ValidAudience = authenticationSettings.KeycloakSettings.Audience,
                        ValidateAudience = true
                    };

                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/v1/notify"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddCors();
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Authenticated", p => p.RequireAssertion(require => require.User.Identity.IsAuthenticated));
                opt.AddPolicy("Anonymous", p => p.RequireAssertion(o => true));
            });

            services.AddScoped<ICurrentSessionUser>(c =>
            {
                var context = c.GetRequiredService<IHttpContextAccessor>().HttpContext;

                if (context != null && context.User.Identity.IsAuthenticated)
                {
                    ClaimsIdentity identity = (ClaimsIdentity)context.User.Identity;

                    string claimsSchemaPrefix = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

                    return new CurrentSessionUser
                    {
                        Id = identity.Claims.FirstOrDefault(c => c.Type == $"{claimsSchemaPrefix}/nameidentifier")?.Value,
                        GivenName = identity.Claims.FirstOrDefault(c => c.Type == $"{claimsSchemaPrefix}/givenname")?.Value,
                        SurName = identity.Claims.FirstOrDefault(c => c.Type == $"{claimsSchemaPrefix}/surname")?.Value,
                        EmailAddress = identity.Claims.FirstOrDefault(c => c.Type == $"{claimsSchemaPrefix}/emailaddress")?.Value
                    };
                }

                return null;
            });
        }
    }
}