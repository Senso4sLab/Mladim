using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos.Members.Participants;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos;

public class ActivityStatisticQueryDto
{    
    public ActivityAttributesQueryDto Attributes { get; set; } = default!;
    public string ProjectName { get; set; } = string.Empty;
    public List<ParticipantsGenderDto> ParticipantsByGenders { get; set; } = new List<ParticipantsGenderDto>();
    public List<ParticipantsAgeGroupDto> ParticipantsByAgeGroups { get; set; } = new List<ParticipantsAgeGroupDto>();


    private ActivityStatisticQueryDto(ActivityAttributesQueryDto attributes, string projectName, IEnumerable<ParticipantsGenderDto> participantsByGenders, IEnumerable<ParticipantsAgeGroupDto> participantsByAgeGroups)
    {
        Attributes = attributes;
        ProjectName = projectName;
        ParticipantsByGenders = participantsByGenders.ToList();
        ParticipantsByAgeGroups = participantsByAgeGroups.ToList();
    }

    public static ActivityStatisticQueryDto Create(ActivityAttributesQueryDto attributes, string projectName, IEnumerable<ParticipantsAgeGroupDto> participantsByAgeGroups, IEnumerable<ParticipantsGenderDto> participantsByGenders)
    {
        return new ActivityStatisticQueryDto(attributes, projectName, participantsByGenders, participantsByAgeGroups);
    }

}
