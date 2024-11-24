using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Members.StaffMembers;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IStaffMemberService
{
    Task<Result?> ResendEmailAsync(int organizationId, string email);
    Task<StaffMemberVM?> AddAsync(int organizationId, StaffMemberVM staffMember);
    Task<IEnumerable<NamedEntityVM>> GetBaseByOrganizationIdAsync(int organizationId, bool isActive);
    Task<IEnumerable<StaffMemberVM>> GetByOrganizationIdAsync(int organizationId,  bool isActive);
    Task<IEnumerable<StaffMemberLeadVM>> GetLeadStaffMembersAsync(int organizationId);
    Task<bool> UpdateAsync(StaffMemberVM staffMember);
}
