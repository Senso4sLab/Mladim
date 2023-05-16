using System.Linq.Expressions;

namespace Mladim.Application.Contract;

public interface IGenericRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task AddAsync(IEnumerable<T> entities);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, IEnumerable<Expression<Func<T, string>>> includes = null);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, IEnumerable<Expression<Func<T, string>>> includes = null);
    
    bool Remove(T entity);
    void Remove(IEnumerable<T> entities);
    T Update(T entity);
}