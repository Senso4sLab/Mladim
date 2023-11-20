using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;

public abstract class SurveyResponseSelector
{
    public string Name { get;}
    public SurveyResponseSelector(string name)
    {
        this.Name = name;
    }
    
    public List<ParticipantPredicate> ParticipantPredicatesByType { get; set; } = new();
}
public class GenderSurveyResponseSelector : SurveyResponseSelector
{    
    public GenderSurveyResponseSelector(): base("Spol") 
    {
        this.ParticipantPredicatesByType = Enum.GetValues<Gender>()
            .Select(g => new ParticipantPredicate(g.GetDisplayAttribute(), p => p.Gender == g))
            .Union(new[] { ParticipantPredicate.All() })
            .ToList();          
    }
}

public class AgeGroupSurveyResponseSelector : SurveyResponseSelector
{
    public AgeGroupSurveyResponseSelector() : base("Starostna skupina")
    {
        this.ParticipantPredicatesByType = Enum.GetValues<AgeGroups>()
            .Select(a => new ParticipantPredicate(a.GetDisplayAttribute(), p => p.AgeGroup == a))
            .Union(new[] { ParticipantPredicate.All() })
            .ToList();
    }
}

public record ParticipantPredicate(string Name, Predicate<AnonymousParticipantVM> Predicate)
{
    public static ParticipantPredicate All() =>
        new ParticipantPredicate("Skupaj", _ => true);
}

