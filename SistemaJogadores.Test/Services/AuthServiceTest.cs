using Microsoft.AspNetCore.Identity;
using Moq;
using SistemaJogadores.Api.Models.Auth;
using SistemaJogadores.Api.Repository.Entities;
using SistemaJogadores.Api.Repository.Interfaces;
using SistemaJogadores.Api.Services;
using SistemaJogadores.Api.Services.Interfaces;

namespace SistemaJogadores.Test.Services;

public class AuthServiceTest
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IPasswordHasher<UserEntity>> _mockPasswordHasher;
    private readonly IAuthService _authService;

    public AuthServiceTest()
    {
        _mockUserRepository = new Mock<IUserRepository>(); // Agora mockamos a interface
        _mockPasswordHasher = new Mock<IPasswordHasher<UserEntity>>();
        _authService = new AuthService(_mockUserRepository.Object, _mockPasswordHasher.Object);
    }

    [Fact]
    public async Task CreateUser_DeveCriarUsuarioComSucesso()
    {
        // Arrange
        var userModel = new CreateUserModel
        {
            Login = "carlos123",
            Password = "senha123"
        };

        _mockPasswordHasher.Setup(ph => ph.HashPassword(It.IsAny<UserEntity>(), It.IsAny<string>()))
            .Returns("hashedPassword");

        _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<UserEntity>()))
            .ReturnsAsync(new UserEntity() { Id = 1, UserName = userModel.Login, Password = "hashedPassword"});

        // Act
        var result = await _authService.CreateUser(userModel);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Usuário criado com sucesso", result.Mensagem);
        Assert.True(result.Sucesso);
        Assert.Null(result.Password);  // A senha não deve estar presente no UserModel
        _mockUserRepository.Verify(repo => repo.AddAsync(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task Authenticate_DeveRetornarTokenQuandoUsuarioForValido()
    {
        // Arrange
        var LoginModel = new LoginModel
        {
            Login = "carlos123",
            Password = "senha123"
        };

        var userEntity = new UserEntity
        {
            UserName = "carlos123",
            Password = "hashedPassword"
        };

        _mockUserRepository.Setup(repo => repo.GetAllAsync(db => db.UserName.Equals(LoginModel.Login)))
            .ReturnsAsync(new List<UserEntity> { userEntity });

        _mockPasswordHasher.Setup(ph => ph.VerifyHashedPassword(It.IsAny<UserEntity>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Success);

        var token = TokenService.GenerateToken(userEntity);
        var expirationDate = DateTime.UtcNow.AddHours(2);

        // Act
        var result = await _authService.Authenticate(LoginModel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Sucesso);
        Assert.Equal("Usuário validado", result.Mensagem);
        Assert.Equal(token, result.Token);
        _mockUserRepository.Verify(repo => repo.GetAllAsync(db => db.UserName.Equals(LoginModel.Login)), Times.Once);
        _mockPasswordHasher.Verify(ph => ph.VerifyHashedPassword(It.IsAny<UserEntity>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Authenticate_DeveRetornarErroQuandoUsuarioNaoForEncontrado()
    {
        // Arrange
        var LoginModel = new LoginModel
        {
            Login = "carlos123",
            Password = "senha123"
        };

        _mockUserRepository.Setup(repo => repo.GetAllAsync(db => db.UserName.Equals(LoginModel.Login)))
            .ReturnsAsync(new List<UserEntity>());

        // Act
        var result = await _authService.Authenticate(LoginModel);

        // Assert
        Assert.Null(result);
        _mockUserRepository.Verify(repo => repo.GetAllAsync(db => db.UserName.Equals(LoginModel.Login)), Times.Once);
    }

    [Fact]
    public async Task Authenticate_DeveRetornarErroQuandoSenhaForInvalida()
    {
        // Arrange
        var LoginModel = new LoginModel
        {
            Login = "carlos123",
            Password = "senhaErrada"
        };

        var userEntity = new UserEntity
        {
            UserName = "carlos123",
            Password = "hashedPassword"
        };

        _mockUserRepository.Setup(repo => repo.GetAllAsync(db => db.UserName.Equals(LoginModel.Login)))
            .ReturnsAsync(new List<UserEntity> { userEntity });

        _mockPasswordHasher.Setup(ph => ph.VerifyHashedPassword(It.IsAny<UserEntity>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Failed);

        // Act
        var result = await _authService.Authenticate(LoginModel);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Sucesso);
        Assert.Equal("Usuário ou senha inválidos", result.Mensagem);
        _mockUserRepository.Verify(repo => repo.GetAllAsync(db => db.UserName.Equals(LoginModel.Login)), Times.Once);
        _mockPasswordHasher.Verify(ph => ph.VerifyHashedPassword(It.IsAny<UserEntity>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}