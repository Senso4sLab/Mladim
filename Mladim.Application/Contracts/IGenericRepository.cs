using System.Linq.Expressions;

namespace Mladim.Application.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, IEnumerable<Expression<Func<T, string>>> includes = null);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, IEnumerable<Expression<Func<T, string>>> includes = null);
        void Remove(IEnumerable<T> entities);
        bool Remove(T entity);
        T Update(T entity);
    }
}