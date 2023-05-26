using Mladim.Client.ViewModels;
using System.Threading.Tasks;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IOrganizationService
{
    Task<DefaultOrganization?> DefaultOrganizationAsync();
    Task SetDefaultOrganizationAsync(DefaultOrganization defaultOrganization);
    Task<IEnumerable<OrganizationVM>> GetByUserIdAsync(string userId);

    Task<OrganizationVM?> GetByIdAsync(int organizationId);
    Task<OrganizationVM?> AddAsync(OrganizationVM organization, string userId);
    Task<bool> UpdateAsync(OrganizationVM organization);
    Task<bool> RemoveAsync(int organiozationId);
    
}
