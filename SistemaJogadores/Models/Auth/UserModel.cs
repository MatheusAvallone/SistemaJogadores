using System.ComponentModel.DataAnnotations;
using SistemaJogadores.Api.Models.Base;

namespace SistemaJogadores.Api.Models.Auth;

public class UserModel : BaseModel
{
    public string Login { get; set; }

    public string Password { get; set; }
}
