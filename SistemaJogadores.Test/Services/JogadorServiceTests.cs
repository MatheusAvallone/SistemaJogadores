using FluentAssertions;
using Moq;
using SistemaJogadores.Api.Models.Jogador;
using SistemaJogadores.Api.Repository.Entities;
using SistemaJogadores.Api.Repository.Interfaces;
using SistemaJogadores.Api.Services;

namespace SistemaJogadores.Test.Services;

public class JogadorServiceTests
{
    private readonly Mock<IJogadoresRepository> _mockRepository;
    private readonly JogadorService _jogadorService;

    public JogadorServiceTests()
    {
        _mockRepository = new Mock<IJogadoresRepository>();
        _jogadorService = new JogadorService(_mockRepository.Object);
    }

    [Fact]
    public async Task ExibirJogadores_DeveRetornarListaDeJogadores()
    {
        // Arrange
        var jogadoresDb = new List<JogadorEntity>
        {
            new("Carlos", 25, "Brasil", "Atacante", "Clube A", 1.75, 70, 9, "Destro"),
            new("Rafael", 28, "Argentina", "Meia", "Clube B", 1.80, 75, 10, "Canhoto")
        };

        _mockRepository.Setup(repo => repo.GetAllAsync(null))
            .ReturnsAsync(jogadoresDb);

        var jogadoresEsperados = new List<JogadorModel>
        {
            new("Carlos", 25, "Brasil", "Atacante", "Clube A", 1.75, 70, 9, "Destro"),
            new("Rafael", 28, "Argentina", "Meia", "Clube B", 1.80, 75, 10, "Canhoto")
        };

        // Act
        var resultado = await _jogadorService.ExibirJogadores();

        // Assert
        resultado.Should().BeEquivalentTo(jogadoresEsperados);
        _mockRepository.Verify(repo => repo.GetAllAsync(null), Times.Once);
    }

    [Fact]
    public async Task CadastrarJogador_DeveAdicionarJogador()
    {
        // Arrange
        var novoJogador = new JogadorModel("Carlos", 25, "Brasil", "Atacante", "Clube A", 1.75, 70, 9, "Destro");

        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<JogadorEntity>()))
            .ReturnsAsync(new JogadorEntity(novoJogador.Nome, novoJogador.Idade, novoJogador.Nacionalidade,
                                            novoJogador.Posicao, novoJogador.Clube, novoJogador.Altura,
                                            novoJogador.Peso, novoJogador.NumeroCamisa, novoJogador.Pe));

        // Act
        var resultado = await _jogadorService.CadastrarJogador(novoJogador);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Nome.Should().Be(novoJogador.Nome);
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<JogadorEntity>()), Times.Once);
    }

    [Fact]
    public async Task EditarJogador_DeveEditarJogador()
    {
        // Arrange
        var jogadorExistente = new JogadorEntity("Carlos", 25, "Brasil", "Atacante", "Clube A", 1.75, 70, 9, "Destro");
        var jogadorEditado = new JogadorModel("Carlos Silva", 26, "Brasil", "Meia", "Clube C", 1.76, 72, 8, "Canhoto");

        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(jogadorExistente);

        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<JogadorEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        await _jogadorService.EditarJogador(jogadorEditado);

        // Assert
        _mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<JogadorEntity>()), Times.Once);
    }

    [Fact]
    public async Task RemoverJogador_DeveRemoverJogador()
    {
        // Arrange
        var jogadorExistente = new JogadorEntity("Carlos", 25, "Brasil", "Atacante", "Clube A", 1.75, 70, 9, "Destro");

        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(jogadorExistente);

        _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<JogadorEntity>()))
            .Returns(Task.CompletedTask);

        // Act
        await _jogadorService.RemoverJogador(1);

        // Assert
        _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<JogadorEntity>()), Times.Once);
    }
}
