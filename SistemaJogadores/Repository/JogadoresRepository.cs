using SistemaJogadores.Api.Repository.Base;
using SistemaJogadores.Api.Repository.Context;
using SistemaJogadores.Api.Repository.Entities;
using SistemaJogadores.Api.Repository.Interfaces;

namespace SistemaJogadores.Api.Repository;

public class JogadoresRepository(SistemasJogadoresContext context) : BaseRepository<JogadorEntity>(context), IJogadoresRepository
{
}