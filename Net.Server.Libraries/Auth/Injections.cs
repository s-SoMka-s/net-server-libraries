using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Net.Server.Libraries.Auth.Implementations;
using Net.Server.Libraries.Auth.Interfaces;
using System.Text;
using Net.Server.Libraries.Auth.Interfaces.Models;

namespace Net.Server.Libraries.Auth;

public static class Injections
{
    public static IServiceCollection AddJwtAuth<TJwtManager>(this IServiceCollection services, string issuer, string securittyKey) where TJwtManager : class, IJwtManager
    {
        // TODO Мб фабрику?
        services.AddScoped<IJwtManager, TJwtManager>();
        services.AddScoped<ICryptographyService, CryptographyService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(jwtOptions =>
                {
                    var keyBytes = Encoding.ASCII.GetBytes(securittyKey);
                    var key = new SymmetricSecurityKey(keyBytes);

                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,

                        ValidateIssuer = true,
                        ValidIssuer = issuer,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    jwtOptions.SaveToken = true;
                });

        // TODO Добавить регистрацию кастомных policies
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim(JwtRegisteredClaimNames.Typ, TokenTypes.Access).Build();

            /*options.AddPolicy(Policies.OnlyRefresh, builder =>
            {
                builder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(JwtRegisteredClaimNames.Typ, TokenTypes.Refresh)
                    .Build();
            });*/
        });

        return services;
    }
}
