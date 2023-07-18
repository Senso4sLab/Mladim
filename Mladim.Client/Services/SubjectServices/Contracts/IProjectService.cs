using Mladim.Client.ViewModels;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IProjectService
{
    Task<bool> AddAsync(ProjectVM project, int organizationId);
    Task<IEnumerable<ProjectVM>> GetByOrganizationIdAsync(int organizationId);
    Task<ProjectVM?> GetByProjectIdAsync(int projectId);
    Task<bool> RemoveAsync(int projectId);
    Task<bool> UpdateAsync(ProjectVM project);
}
