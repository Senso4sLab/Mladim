using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;

public abstract class SurveyResponseSelector
{
    public string? Description { get; protected set; }
    public List<ParticipantPredicate> ParticipantPredicates { get; set; } = new();
}


public record ParticipantPredicate(string Name, Predicate<AnonymousParticipantVM> Predicate);


public class GenderSurveyResponseSelector : SurveyResponseSelector
{    
    public GenderSurveyResponseSelector()
    {
        this.Description = "Spol";
        
        this.ParticipantPredicates = Enum.GetValues<Gender>()
            .Select(g => new ParticipantPredicate(g.GetDisplayAttribute(), p => p.Gender == g))
            .ToList();          
    }
}

public class AgeGroupSurveyResponseSelector : SurveyResponseSelector
{
    public AgeGroupSurveyResponseSelector()
    {
        this.Description = "Starostna skupina";

        this.ParticipantPredicates = Enum.GetValues<AgeGroups>()
            .Select(a => new ParticipantPredicate(a.GetDisplayAttribute(), p => p.AgeGroup == a))
            .ToList();
    }
}
