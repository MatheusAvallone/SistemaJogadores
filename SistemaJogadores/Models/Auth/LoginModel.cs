using System.ComponentModel.DataAnnotations;

namespace SistemaJogadores.Api.Models.Auth;

public class LoginModel
{
    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }
}