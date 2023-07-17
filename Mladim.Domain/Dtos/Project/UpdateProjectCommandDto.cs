
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;

namespace Mladim.Domain.Dtos;

public class UpdateProjectCommandDto
{
    public int Id { get; set; }
    
    public ProjectAttributesCommandDto Attributes { get; set; } = default!;
    public DateTimeRangeCommandDto DateTimeRange { get; set; } = default!;
    public List<StaffMemberCommandDto> Staff { get; set; } = new();
    public List<GroupCommandDto> Groups { get; set; } = new();
    public List<PartnerCommandDto> Partners { get; set; } = new();
}
