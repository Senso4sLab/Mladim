using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;

namespace Mladim.Domain.Dtos;

public class ActivityQueryDetailsDto
{
    public int Id { get; set; }
    public ActivityAttributesQueryDto Attributes { get; set; } = default!;
    public DateTimeRangeQueryDto TimeRange { get; set; } = default!;
    public List<PartnerQueryDto> Partners { get; set; } = new();
    public List<GroupQueryDto> Groups { get; set; } = new();
    public List<ParticipantQueryDto> Participants { get; set; } = new();
    public List<StaffMemberQueryDto> Staff { get; set; } = new();
    public List<AnonymousParticipantQueryDto> AnonymousParticipantActivities { get; set; } = new();

}
