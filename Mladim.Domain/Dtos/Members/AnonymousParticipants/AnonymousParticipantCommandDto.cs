using Mladim.Domain.Enums;
using System.Text.Json.Serialization;

namespace Mladim.Domain.Dtos.Members.AnonymousParticipants;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AnonymousParticipantCommandDto), "AnonymousParticipantCommandDto")]
[JsonDerivedType(typeof(AnonymousYouthWorkerCommandDto), "AnonymousYouthWorkerCommandDto")]
public abstract record SurveyParticipantCommandDto
{
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
}

public record AnonymousParticipantCommandDto : SurveyParticipantCommandDto
{      
}

public record AnonymousYouthWorkerCommandDto : SurveyParticipantCommandDto
{   
    public YouthWorkRole Role { get; set; }
    public YearsOfExperienceInYouthWork YearsOfExperience { get; set; }
}
