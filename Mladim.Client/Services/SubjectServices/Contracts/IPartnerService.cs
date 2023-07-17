using Mladim.Client.ViewModels;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IPartnerService
{
    Task<PartnerVM?> AddAsync(int organizationId, PartnerVM partner);

    Task<IEnumerable<NamedEntityVM>> GetBaseByOrganizationIdAsync(int organizationId, bool isActive);
    Task<IEnumerable<PartnerVM>> GetByOrganizationIdAsync(int organizationId, bool isAcitve);
    Task<bool> UpdateAsync(PartnerVM partner);
}
