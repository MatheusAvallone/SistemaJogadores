using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SistemaJogadores.Api.MapEndpoints;
using SistemaJogadores.Api.Repository.Context;
using SistemaJogadores.Api.Settings;

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

builder.Services.AddDbContext<SistemasJogadoresContext>(options =>
options.UseInMemoryDatabase("JogadoresDB"));

builder.Services.AddAuthenticationConfig();
builder.Services.AddServicesDI();
builder.Services.AddRepositoryDI();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
});

app.MapAuthEndpoint();
app.MapJogadoresEndpoint();

app.UseHttpsRedirection();

#endregion

await app.RunAsync();