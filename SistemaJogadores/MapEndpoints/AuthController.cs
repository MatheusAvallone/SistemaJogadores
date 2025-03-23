using Microsoft.AspNetCore.Mvc;
using SistemaJogadores.Api.Models.Auth;
using SistemaJogadores.Api.Services.Interfaces;

namespace SistemaJogadores.Api.MapEndpoints;

public static class AuthController
{
    public static void MapAuthEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create-user", async (IAuthService _authService, [FromBody] CreateUserModel model) =>
        {
            if (model == null) return Results.BadRequest("model null");

            var newUser = await _authService.CreateUser(model);
            if (newUser.Sucesso)
            {
                return Results.Ok(newUser);
            }
            else
            {
                return Results.BadRequest(newUser);
            }
        })
        .WithName("create-user")
        .WithOpenApi();

        app.MapPost("/login", async (IAuthService _authService, [FromBody] LoginModel model) =>
        {
            if (model == null) return Results.BadRequest("model null");

            var userAuthenticated = await _authService.Authenticate(model);
            if (userAuthenticated.Sucesso)
            {
                return Results.Ok(userAuthenticated);
            }
            else
            {
                return Results.BadRequest(userAuthenticated);
            }
        })
        .WithName("login")
        .WithOpenApi();
    }
}
