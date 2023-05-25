using Mladim.Client.Models;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IStaffMemberService
{
    Task<StaffMember?> AddAsync(int organizationId, StaffMember staffMember);
    Task<IEnumerable<StaffMember>> GetByOrganizationIdAsync(int organizationId, bool isAcitve);
    Task<bool> UpdateAsync(StaffMember staffMember);
}
