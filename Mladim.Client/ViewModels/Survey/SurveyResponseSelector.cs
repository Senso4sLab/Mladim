using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;

public abstract class SurveyResponseSelector
{
    public string? Description { get; protected set; }
    public List<ParticipantPredicate> ParticipantPredicatesByType { get; set; } = new();
}


public record ParticipantPredicate(string Name, Predicate<AnonymousParticipantVM> Predicate)
{
    public static ParticipantPredicate All() => 
        new ParticipantPredicate("Skupaj", _ => true);
}


public class GenderSurveyResponseSelector : SurveyResponseSelector
{    
    public GenderSurveyResponseSelector()
    {
        this.Description = "Spol";

        this.ParticipantPredicatesByType = Enum.GetValues<Gender>()
            .Select(g => new ParticipantPredicate(g.GetDisplayAttribute(), p => p.Gender == g))
            .Union(new[] { ParticipantPredicate.All() })
            .ToList();          
    }
}

public class AgeGroupSurveyResponseSelector : SurveyResponseSelector
{
    public AgeGroupSurveyResponseSelector()
    {
        this.Description = "Starostna skupina";

        this.ParticipantPredicatesByType = Enum.GetValues<AgeGroups>()
            .Select(a => new ParticipantPredicate(a.GetDisplayAttribute(), p => p.AgeGroup == a))
            .Union(new[] { ParticipantPredicate.All() })
            .ToList();
    }
}
