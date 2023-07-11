using Microsoft.EntityFrameworkCore.Query;
using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{   
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    T Update(T entity);
    bool Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool tracking = true);   
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
    Task<T?> FindAsync(object id);
}