namespace SistemaJogadores.Api.Models.Jogador;

public class JogadorModel(string nome, int idade, string nacionalidade, string posicao, string clube, double altura, double peso,
                     int numerocamisa, string pe)
{
    public string Nome { get; set; } = nome;
    public int Idade { get; set; } = idade;
    public string Nacionalidade { get; set; } = nacionalidade;
    public string Posicao { get; set; } = posicao;
    public string Clube { get; set; } = clube;
    public double Altura { get; set; } = altura;
    public double Peso { get; set; } = peso;
    public int NumeroCamisa { get; set; } = numerocamisa;
    public string Pe { get; set; } = pe;
}
