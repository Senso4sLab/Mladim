using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contract;

public interface IActivityRepository
{
    Task<Activity?> AddAsync(Activity entity);
    Task<bool> AnyAsync(Expression<Func<Activity, bool>> predicate);
    Task<IEnumerable<Activity>> GetAllAsync(Expression<Func<Activity, bool>> predicate);
    Task<Activity?> GetByIdAsync(int id, params Expression<Func<Activity, object>>[] includes);
    void Remove(Activity Activity);
    void Update(Activity Activity);
}
