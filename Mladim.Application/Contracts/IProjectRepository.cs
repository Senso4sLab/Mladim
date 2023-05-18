using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contract;

public interface IProjectRepository
{
    Task<Project?> AddAsync(Project entry);
    Task<bool> AnyAsync(Expression<Func<Project, bool>> predicate);
    Task<IEnumerable<Project>> GetAllAsync(Expression<Func<Project, bool>> predicate);   
    Task<Project?> GetByIdAsync(int id, params Expression<Func<Project, object>>[] includes);
    void Remove(Project Project);
    void Update(Project Project);
}