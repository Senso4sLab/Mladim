using Mladim.Client.ViewModels;
using Mladim.Domain.Enums;

namespace Mladim.Client.Services.SubjectServices.Contracts
{
    public interface IGroupService
    {
        Task<GroupVM?> AddAsync(int organizationId, GroupVM group);
        Task<GroupVM?> GetByGroupIdAsync(int groupId);
        Task<IEnumerable<GroupVM>> GetByOrganizationIdAsync(int organizationId, GroupType groupType, bool isActive);
        Task<bool> UpdateAsync(GroupVM group);
    }
}
