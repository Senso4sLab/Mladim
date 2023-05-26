using Mladim.Client.ViewModels;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IPartnerService
{
    Task<PartnerVM?> AddAsync(int organizationId, PartnerVM partner);
    Task<IEnumerable<PartnerVM>> GetByOrganizationIdAsync(int organizationId, bool isAcitve);
    Task<bool> UpdateAsync(PartnerVM partner);
}
