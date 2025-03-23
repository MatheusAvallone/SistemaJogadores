using Microsoft.EntityFrameworkCore;
using SistemaJogadores.Api.Repository.Entities;
using System;

namespace SistemaJogadores.Api.Repository.Context;

public class SistemasJogadoresContext(DbContextOptions<SistemasJogadoresContext> options) : DbContext(options)
{
    public DbSet<UserEntity> User { get; set; }
    public DbSet<JogadorEntity> Jogadores { get; set; }
}