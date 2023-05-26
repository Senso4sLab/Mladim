using Mladim.Client.ViewModels;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IStaffMemberService
{
    Task<StaffMemberVM?> AddAsync(int organizationId, StaffMemberVM staffMember);
    Task<IEnumerable<StaffMemberVM>> GetByOrganizationIdAsync(int organizationId, bool isAcitve);
    Task<bool> UpdateAsync(StaffMemberVM staffMember);
}
