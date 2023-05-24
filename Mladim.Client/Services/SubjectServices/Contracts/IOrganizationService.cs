using Mladim.Client.Models;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IOrganizationService
{
    Task<int?> DefaultOrganizationIdAsync();
    Task<IEnumerable<Organization>> GetByUserIdAsync(string userId);
    Task<bool> RemoveAsync(int organiozationId);
    Task SetDefaultOrganizationAsync(int orgId);
}
