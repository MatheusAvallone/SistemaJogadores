using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SistemaJogadores.Api.Repository.Context;

namespace SistemaJogadores.Api.Repository.Base;

public class BaseRepository<T> where T : class
{
    protected readonly SistemasJogadoresContext _context;

    public BaseRepository(SistemasJogadoresContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (predicate != null)
        {
            query = query.Where(predicate); 
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}