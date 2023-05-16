using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Mladim.Application.Contract;

public interface IGenericRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task AddAsync(IEnumerable<T> entities);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    bool Remove(T entity);
    void Remove(IEnumerable<T> entities);
    T Update(T entity);
}