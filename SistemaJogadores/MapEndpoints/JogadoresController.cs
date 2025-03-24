using Microsoft.AspNetCore.Authorization;
using SistemaJogadores.Api.Models.Jogador;
using SistemaJogadores.Api.Services.Interfaces;

namespace SistemaJogadores.Api.MapEndpoints;

public static class JogadoresController
{
    public static void MapJogadoresEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/listar-jogadores", [Authorize] async(IJogadorService _jogadorService) =>
        {
            var jogadoresDb = await _jogadorService.ExibirJogadores();

            return Results.Ok(jogadoresDb);
        })
        .WithName("listar-jogador")
        .WithOpenApi();

        app.MapPost("/cadastrar-jogador", [Authorize] async(IJogadorService _jogadorService, JogadorModel model) =>
        {
            if (model == null) return Results.BadRequest("model null");

            var novoJogadorDb = await _jogadorService.CadastrarJogador(model);

            return Results.Ok(novoJogadorDb);
        })
        .WithName("cadastrar-jogador")
        .WithOpenApi();


        app.MapPut("/editar-jogador", [Authorize] async (IJogadorService _jogadorService, JogadorModel model) =>
        {
            if (model == null) return Results.BadRequest("model null");

            var jogadorEditadoDb = await _jogadorService.CadastrarJogador(model);

            return Results.Ok(jogadorEditadoDb);
        })
        .WithName("editar-jogador")
        .WithOpenApi();

        app.MapDelete("/remover-jogador", [Authorize] async (IJogadorService _jogadorService, int idJogador) =>
        {
            await _jogadorService.RemoverJogador(idJogador);

            return Results.Ok($"Jogador: {idJogador} removido com sucesso.");
        })
        .WithName("remover-jogador")
        .WithOpenApi();
    }
}