using SistemaJogadores.Api.Repository.Base;

namespace SistemaJogadores.Api.Repository.Entities;

public class UserEntity : BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
