using System.Linq.Expressions;

namespace SistemaJogadores.Api.Repository.Base.Interface;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T jogador);
    Task UpdateAsync(T jogador);
    Task DeleteAsync(T jogador);
}
