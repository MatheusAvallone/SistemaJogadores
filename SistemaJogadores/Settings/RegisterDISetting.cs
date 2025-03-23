using SistemaJogadores.Api.Repository.Interfaces;
using SistemaJogadores.Api.Repository;
using SistemaJogadores.Api.Services.Interfaces;
using SistemaJogadores.Api.Services;

namespace SistemaJogadores.Api.Settings;

public static class RegisterDiSetting
{
    public static IServiceCollection AddServicesDI(this IServiceCollection services)
    {

        services.AddScoped<IJogadorService, JogadorService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection AddRepositoryDI(this IServiceCollection services)
    {
        services.AddScoped<IJogadoresRepository, JogadoresRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}