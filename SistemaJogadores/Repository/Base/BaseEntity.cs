using System.ComponentModel.DataAnnotations;

namespace SistemaJogadores.Api.Repository.Base;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime Criado { get; set; }
    public DateTime? Atualizado { get; set; }

    public BaseEntity()
    {
        Criado = DateTime.Now;
    }
}
