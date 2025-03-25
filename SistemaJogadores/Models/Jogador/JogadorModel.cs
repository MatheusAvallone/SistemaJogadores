using SistemaJogadores.Api.Models.Base;

namespace SistemaJogadores.Api.Models.Jogador;

public class JogadorModel : BaseModel
{
    public JogadorModel(string nome, int idade, string nacionalidade, string posicao, string clube, double altura, double peso,
                        int numeroCamisa, string pe)
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

    public JogadorModel(string nome, int idade, string nacionalidade, string posicao, string clube, double altura, double peso,
                         int numeroCamisa, string pe, int id)
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
        Id = id;
    }
    public JogadorModel()
    {

    }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Nacionalidade { get; set; }
    public string Posicao { get; set; }
    public string Clube { get; set; }
    public double Altura { get; set; }
    public double Peso { get; set; }
    public int NumeroCamisa { get; set; }
    public string Pe { get; set; }
    public int Id { get; set; }
}