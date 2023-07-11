using Mladim.Domain.Models;

namespace Mladim.Application.Contracts.Persistence;

public interface IGroupRepository : IGenericRepository<Group>
{
    Task<Group?> GetGroupDetailsAsync(int groupId, bool tracking = true);
}