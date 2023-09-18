using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<Project?> GetProjectDetailsAsync(int projectId, bool tracking = true);
    Task<IEnumerable<Project>> GetProjectsWithStaffMemberWithAsync(int organizationId, string email);
}