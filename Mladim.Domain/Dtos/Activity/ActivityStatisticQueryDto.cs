using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos.Members.Participants;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos;

public class ActivityStatisticQueryDto
{    
    public ActivityAttributesQueryDto Attributes { get; set; } = default!;
    public string ProjectName { get; set; } = string.Empty;

    public DateTime Start { get;set; }
    public DateTime End { get;set; }
    public double DurationHours { get;set; }

    
    public List<ParticipantsGenderDto> ParticipantsByGenders { get; set; } = new List<ParticipantsGenderDto>();
    public List<ParticipantsAgeGroupDto> ParticipantsByAgeGroups { get; set; } = new List<ParticipantsAgeGroupDto>();


    public ActivityStatisticQueryDto()
    {
        
    }


    private ActivityStatisticQueryDto(ActivityAttributesQueryDto attributes, DateTime start, DateTime end, double durationHours, string projectName, IEnumerable<ParticipantsGenderDto> participantsByGenders, IEnumerable<ParticipantsAgeGroupDto> participantsByAgeGroups)
    {
        Attributes = attributes;
        Start = start;
        End = end;
        DurationHours = durationHours;
        ProjectName = projectName;
        ParticipantsByGenders = participantsByGenders.ToList();
        ParticipantsByAgeGroups = participantsByAgeGroups.ToList();
    }

    public static ActivityStatisticQueryDto Create(ActivityAttributesQueryDto attributes, DateTime start, DateTime end, double durationHours, string projectName, IEnumerable<ParticipantsAgeGroupDto> participantsByAgeGroups, IEnumerable<ParticipantsGenderDto> participantsByGenders)
    {
        return new ActivityStatisticQueryDto(attributes, start, end, durationHours, projectName, participantsByGenders, participantsByAgeGroups);
    }

}
