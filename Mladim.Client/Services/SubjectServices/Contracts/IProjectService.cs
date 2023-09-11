using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Project;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IProjectService
{
    Task<bool> AddAsync(ProjectVM project, int organizationId);
    Task<IEnumerable<ProjectVM>> GetByOrganizationIdAsync(int organizationId, string userId);
    Task<ProjectVM?> GetByProjectIdAsync(int projectId);
    Task<ProjectStatisticsVM?> GetStatisticsAsync(int projectId);
    Task<bool> RemoveAsync(int projectId);
    Task<bool> UpdateAsync(ProjectVM project);
}
