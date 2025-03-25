using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaJogadores.Api.Models.Jogador;
using SistemaJogadores.Api.Repository.Entities;
using SistemaJogadores.Api.Repository.Interfaces;
using SistemaJogadores.Api.Services.Interfaces;

namespace SistemaJogadores.Api.Services;

public class JogadorService : IJogadorService
{
    private readonly IJogadoresRepository _jogadoresRepository;

    public JogadorService(IJogadoresRepository jogadoresRepository)
    {
        _jogadoresRepository = jogadoresRepository;
    }
    public async Task<List<JogadorModel>> ExibirJogadores()
    {

        var jogadoresEntity = await _jogadoresRepository.GetAllAsync();

        var jogadoresModel = new List<JogadorModel>();

        foreach (var jogadorEntity in jogadoresEntity)
        {
            JogadorModel jogadorModel = new JogadorModel
            {
                Id = jogadorEntity.Id,
                Nome = jogadorEntity.Nome,
                Idade = jogadorEntity.Idade,
                Nacionalidade = jogadorEntity.Nacionalidade,
                Posicao = jogadorEntity.Posicao,
                Clube = jogadorEntity.Clube,
                Altura = jogadorEntity.Altura,
                Peso = jogadorEntity.Peso,
                NumeroCamisa = jogadorEntity.NumeroCamisa,
                Pe = jogadorEntity.Pe,
            };

            jogadoresModel.Add(jogadorModel);
 
        }
            return jogadoresModel;
    }
    public async Task<JogadorModel> CadastrarJogador(JogadorModel model)
    {
        var jogadorEntity = new JogadorEntity
        {
            Id = (int) model.Id,
            Nome = model.Nome,
            Idade = model.Idade,
            Nacionalidade = model.Nacionalidade,
            Posicao = model.Posicao,
            Clube = model.Clube,
            Altura = model.Altura,
            Peso = model.Peso,
            NumeroCamisa = model.NumeroCamisa,
            Pe = model.Pe,
        };

        await _jogadoresRepository.AddAsync(jogadorEntity);

        return model; 
        
    }
    public async Task<JogadorModel> EditarJogador(JogadorModel jogadores)
    {
        var jogadorEntity = await _jogadoresRepository.GetByIdAsync(jogadores.Id);

        if (jogadorEntity== null)
        {
            throw new Exception("Jogador não encontrado.");
        }

        jogadorEntity.Nome = jogadores.Nome;
        jogadorEntity.Idade = jogadores.Idade;
        jogadorEntity.Nacionalidade = jogadores.Nacionalidade;
        jogadorEntity.Posicao = jogadores.Posicao;
        jogadorEntity.Clube = jogadores.Clube;
        jogadorEntity.Altura = jogadores.Altura;
        jogadorEntity.NumeroCamisa = jogadores.NumeroCamisa;
        jogadorEntity.Pe = jogadores.Pe;

        await _jogadoresRepository.UpdateAsync(jogadorEntity);

        jogadores.Mensagem = " Jogador alterado com sucesso. ";

        return jogadores;
            
    }

    public async Task RemoverJogador(int idJogador)
    {
       var jogador = await _jogadoresRepository.GetByIdAsync(idJogador);

        if (jogador == null) 
        if (jogador != null)

        {
            await _jogadoresRepository.DeleteAsync(jogador);
        }
        else 
        {
            throw new Exception(" jogador não encontrado.");
        }

        await _jogadoresRepository.DeleteAsync(jogador);

    }

}
