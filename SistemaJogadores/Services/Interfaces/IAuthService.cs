using Microsoft.AspNetCore.Mvc;
using SistemaJogadores.Api.Models.Auth;

namespace SistemaJogadores.Api.Services.Interfaces;

public interface IAuthService
{
    Task<UserModel> CreateUser(CreateUserModel model);
    Task<dynamic> Authenticate(LoginModel model);
}
