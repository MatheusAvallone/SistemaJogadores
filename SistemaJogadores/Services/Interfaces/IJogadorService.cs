using SistemaJogadores.Api.Models.Jogador;

namespace SistemaJogadores.Api.Services.Interfaces;

public interface IJogadorService
{
    Task<List<JogadorModel>> ExibirJogadores();
    Task<JogadorModel> CadastrarJogador(JogadorModel jogadores);
    Task EditarJogador(JogadorModel jogadores);
    Task RemoverJogador(int idJogador);
}
