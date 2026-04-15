using System.Security.Claims;
using System.Security.Cryptography;
using CrossCutting.Entities.Authentication;
using Domain.Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Registers.Authentication;

public static class AuthenticationRegister
{
    public static void Load(IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = new AuthenticationSettings();
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
                o.Authority = authenticationSettings.KeycloakSettings?.Authority;
                o.Audience = authenticationSettings.KeycloakSettings?.Audience;

                var publicRsa = RSA.Create();
                publicRsa.FromXmlString(
                    File.ReadAllText(
                        Path.Combine(Directory.GetCurrentDirectory(), "Cert",
                            authenticationSettings.PublicKey ?? throw new InvalidOperationException()))
                );

                var signingKey = new RsaSecurityKey(publicRsa);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = authenticationSettings.KeycloakSettings?.Issuer,
                    ValidateIssuer = true,

                    ValidAudience = authenticationSettings.KeycloakSettings?.Audience,
                    ValidateAudience = true
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/v1/notify"))
                            context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddCors();
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("Authenticated", p => p.RequireAssertion(require => require.User.Identity!.IsAuthenticated));
            opt.AddPolicy("Anonymous", p => p.RequireAssertion(o => true));
        });

        services.AddScoped<ICurrentSessionUser>(c =>
        {
            var context = c.GetRequiredService<IHttpContextAccessor>().HttpContext;

            if (context?.User.Identity is not ClaimsIdentity { IsAuthenticated: true } identity)
                throw new Exception("No claims found");

            var claimsSchemaPrefix = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
            var clamis = identity.Claims.ToArray() ?? throw new Exception("No claims found");

            return new CurrentSessionUser
            {
                Id = clamis.FirstOrDefault(claim => claim.Type == $"{claimsSchemaPrefix}/nameidentifier")?.Value,
                GivenName = clamis.FirstOrDefault(claim => claim.Type == $"{claimsSchemaPrefix}/givenname")?.Value,
                SurName = clamis.FirstOrDefault(claim => claim.Type == $"{claimsSchemaPrefix}/surname")?.Value,
                EmailAddress = clamis.FirstOrDefault(claim => claim.Type == $"{claimsSchemaPrefix}/emailaddress")?.Value
            };
        });
    }
}