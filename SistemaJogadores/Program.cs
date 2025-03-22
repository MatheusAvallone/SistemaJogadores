using Microsoft.OpenApi.Models;
using SistemaJogadores.Models;
using SistemaJogadores.Services;

#region [CONFIGURAÇÃO]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API",
        Version = "v1",
        Description = "Uma API simples com Minimal API e Swagger"
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
});

app.UseHttpsRedirection();

#endregion

app.MapGet("/listar-jogadores", () =>
{
    var jogadoresDb = new JogadorService().ExibirJogadores();

    return Results.Ok(jogadoresDb);
})
.WithName("listar-jogador")
.WithOpenApi();

app.MapPost("/cadastrar-jogador", (JogadorModel model) =>
{
    if (model == null) return Results.BadRequest("model null");

    var novoJogadorDb = new JogadorService().CadastrarJogador(model);

    return Results.Ok(novoJogadorDb);
})
.WithName("cadastrar-jogador")
.WithOpenApi();


app.MapPut("/editar-jogador", (JogadorModel model) =>
{
    if (model == null) return Results.BadRequest("model null");

    var jogadorEditadoDb = new JogadorService().CadastrarJogador(model);

    return Results.Ok(jogadorEditadoDb);
})
.WithName("editar-jogador")
.WithOpenApi();

app.MapDelete("/remover-jogador", (int idJogador) =>
{
    new JogadorService().RemoverJogador(idJogador);

    return Results.Ok($"Jogador: {idJogador} removido com sucesso.");
})
.WithName("remover-jogador")
.WithOpenApi();

await app.RunAsync();