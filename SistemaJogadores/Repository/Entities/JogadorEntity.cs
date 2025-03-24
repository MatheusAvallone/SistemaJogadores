using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaJogadores.Api.Repository.Base;

namespace SistemaJogadores.Api.Repository.Entities;

[Table("Jogador")]
public class JogadorEntity : BaseEntity
{
    public JogadorEntity() { }

    public JogadorEntity(string nome, int idade, string nacionalidade, string posicao,
                   string clube, double altura, double peso, int numeroCamisa, string pe)
    {
        Nome = nome;
        Idade = idade;
        Nacionalidade = nacionalidade;
        Posicao = posicao;
        Clube = clube;
        Altura = altura;
        Peso = peso;
        NumeroCamisa = numeroCamisa;
        Pe = pe;
    }

    [Required]
    public string Nome { get; set; }

    [Range(16, 50, ErrorMessage = "A idade deve estar entre 16 e 50 anos.")]
    public int Idade { get; set; }

    public string Nacionalidade { get; set; }
    public string Posicao { get; set; }
    public string Clube { get; set; }
    public double Altura { get; set; }
    public double Peso { get; set; }
    public int NumeroCamisa { get; set; }
    public string Pe { get; set; }
}