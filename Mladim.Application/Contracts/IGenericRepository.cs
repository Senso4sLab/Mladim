using Microsoft.EntityFrameworkCore.Query;
using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contract;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);    
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task AddAsync(IEnumerable<T> entities);
    T Update(T entity);   
    bool Remove(T entity);
    void Remove(IEnumerable<T> entities);    
}