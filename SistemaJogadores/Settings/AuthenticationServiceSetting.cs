using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SistemaJogadores.Api.Repository.Entities;

namespace SistemaJogadores.Api.Settings;

public static class AuthenticationServiceSetting
{
    public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services)
    {
        var key = Encoding.ASCII.GetBytes(ApiKeySetting.Secret);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorization();

        services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

        return services;
    }
}
