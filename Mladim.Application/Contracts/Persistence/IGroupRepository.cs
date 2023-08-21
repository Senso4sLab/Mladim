using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IGroupRepository : IGenericRepository<Group>
{
    Task<IEnumerable<TResult>> GetAllGroupsAsync<TResult>(Expression<Func<TResult, bool>> predicate) where TResult : Group;
    Task<Group?> GetGroupDetailsAsync(int groupId, bool tracking = true);
    Task<int> NumberOfMemberInGroupAsync<TResult>(Expression<Func<TResult, bool>> predicate) where TResult : Group;
}