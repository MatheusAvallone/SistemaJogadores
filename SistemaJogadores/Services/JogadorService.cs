using SistemaJogadores.Api.Models.Jogador;
using SistemaJogadores.Api.Repository.Interfaces;
using SistemaJogadores.Api.Services.Interfaces;

namespace SistemaJogadores.Api.Services;

public class JogadorService : IJogadorService
{
    private readonly IJogadoresRepository _jogadoresRepository;

    public JogadorService(IJogadoresRepository jogadoresRepository)
    {
        _jogadoresRepository = jogadoresRepository;
    }

    public async Task<List<JogadorModel>> ExibirJogadores()
    {
        throw new NotImplementedException("TODO: Implementar exibição de jogadores.");
    }

    public async Task<JogadorModel> CadastrarJogador(JogadorModel jogadores)
    {
        throw new NotImplementedException("TODO: Implementar cadastro de jogadores.");
    }

    public async Task EditarJogador(JogadorModel jogadores)
    {
        throw new NotImplementedException("TODO: Implementar edição de jogadores.");
    }

    public async Task RemoverJogador(int idJogador)
    {
        throw new NotImplementedException("TODO: Implementar remoção de jogadores.");
    }
}