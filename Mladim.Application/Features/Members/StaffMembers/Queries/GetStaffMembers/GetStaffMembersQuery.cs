using MediatR;
using Mladim.Domain.Dtos.Members;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;

public class GetStaffMembersQuery : IRequest<IEnumerable<NamedEntityDto>>
{    
    public int? ProjectId { get; set; }
    public int? ActivityId { get; set; }
    public int? OrganizationId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsMemberAbbreviated { get; set; } = false;
}
