using Mladim.Domain.Models;

namespace Mladim.Application.Contracts.Persistence;

public interface IGroupRepository : IGenericRepository<Group>
{
    Task<IEnumerable<TResult>> GetAllGroupsAsync<TResult>(System.Linq.Expressions.Expression<Func<TResult, bool>> predicate) where TResult : Group;
    Task<Group?> GetGroupDetailsAsync(int groupId, bool tracking = true);
}