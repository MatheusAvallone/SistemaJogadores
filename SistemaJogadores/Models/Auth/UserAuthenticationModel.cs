using SistemaJogadores.Api.Models.Base;

namespace SistemaJogadores.Api.Models.Auth;

public class UserAuthenticationModel : BaseModel
{
    public DateTime DateExpires { get; set; }
    public string Token { get; set; }
}
