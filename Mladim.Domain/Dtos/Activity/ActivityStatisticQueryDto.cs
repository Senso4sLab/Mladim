using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos.Members.Participants;

namespace Mladim.Domain.Dtos;

public class ActivityStatisticQueryDto
{
    public int Id { get; set; }
    public ActivityAttributesQueryDto Attributes { get; set; } = default!;
    public NamedEntityDto Project { get; set; } = default!;
    public List<ParticipantsGenderDto> ParticipantsByGenders { get; set; } = new List<ParticipantsGenderDto>();
    public List<ParticipantsAgeGroupDto> ParticipantsByAgeGroups { get; set; } = new List<ParticipantsAgeGroupDto>();

}
