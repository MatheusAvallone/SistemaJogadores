using Microsoft.AspNetCore.Identity;
using SistemaJogadores.Api.Models.Auth;
using SistemaJogadores.Api.Repository.Entities;
using SistemaJogadores.Api.Repository.Interfaces;
using SistemaJogadores.Api.Services.Interfaces;

namespace SistemaJogadores.Api.Services;

public class AuthService(IUserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<UserEntity> _passwordHasher = passwordHasher;

    public async Task<UserModel> CreateUser(CreateUserModel model)
    {
        var userModel = new UserModel();

        var userDbExists = await _userRepository.GetAllAsync(db => db.UserName.Equals(model.Login));
        
        if(userDbExists is not null && userDbExists.Count > 0)
        {
            userModel.Mensagem = "Usuário já existente";
            userModel.Sucesso = false;

            return userModel;
        }
        
        var newUser = new UserEntity
        {
            UserName = model.Login,
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, model.Password);
        newUser.Password = hashedPassword;

        await _userRepository.AddAsync(newUser);

        userModel.Mensagem = "Usuário criado com sucesso";
        userModel.Sucesso = true;

        return userModel;
    }

    public async Task<dynamic> Authenticate(LoginModel model)
    {
        var userModel = new UserModel();

        var user = (await _userRepository.GetAllAsync(db => db.UserName.Equals(model.Login))).FirstOrDefault();

        if (user == null) return null;

        if (_passwordHasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Failed)
        {
            userModel.Mensagem = "Usuário ou senha inválidos";
            userModel.Sucesso = false;
            return userModel;
        }

        var token = TokenService.GenerateToken(user);

        return new UserAuthenticationModel
        {
            Mensagem = "Usuário validado",
            Sucesso = true,
            Token = token,
            DateExpires = DateTime.UtcNow.AddHours(2),
        };
    }
}