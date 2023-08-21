using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Members.StaffMembers;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IStaffMemberService
{
    Task<StaffMemberVM?> AddAsync(int organizationId, StaffMemberVM staffMember);
    Task<IEnumerable<NamedEntityVM>> GetBaseByOrganizationIdAsync(int organizationId, bool isActive);
    Task<IEnumerable<StaffMemberVM>> GetByOrganizationIdAsync(int organizationId,  bool isActive);
    Task<IEnumerable<StaffMemberLeadVM>> GetLeadStaffMembersAsync(int organizationId);
    Task<bool> UpdateAsync(StaffMemberVM staffMember);
}
