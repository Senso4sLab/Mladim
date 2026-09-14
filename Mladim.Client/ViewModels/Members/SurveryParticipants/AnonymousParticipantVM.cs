using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;


public abstract record SurveyParticipantVM
{
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
}

public record AnonymousParticipantVM : SurveyParticipantVM
{    
   
}

public record AnonymousYouthWorkerVM : SurveyParticipantVM
{
    public YouthWorkRole Role { get; set; }
    public YearsOfExperienceInYouthWork YearsOfExperience { get; set; }
}



