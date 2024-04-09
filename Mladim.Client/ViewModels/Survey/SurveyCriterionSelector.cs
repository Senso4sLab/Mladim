using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;

public abstract class SurveyCriterionSelector
{
    public string Name { get;}
    public string SurveyTitle { get;  }
    public List<ParticipantPredicate> ParticipantPredicatesByType { get; set; } = new();
    public SurveyCriterionSelector(string name, string surveyTitle)
    {
        this.Name = name;
        this.SurveyTitle = surveyTitle;
    }

    public static SurveyCriterionSelector GenderSelector() =>
        new GenderSelector();
    public static SurveyCriterionSelector AgeGroupSelector() =>
       new AgeGroupSelector();

    
    
}

public class GenderSelector : SurveyCriterionSelector
{    
    public GenderSelector(): base("Spol", "Prikaz rezultatov po spolu") 
    {
        this.ParticipantPredicatesByType = ParticipantPredicate.Genders.Concat(new[] { ParticipantPredicate.None }).ToList();        
    }
}

public class AgeGroupSelector : SurveyCriterionSelector
{
    public AgeGroupSelector() : base("Starostna skupina", "Prikaz rezultatov po starostni skupini")
    {
        this.ParticipantPredicatesByType = ParticipantPredicate.AgeGroups.Concat(new[] { ParticipantPredicate.None }).ToList();           
    }
}




public record ParticipantPredicate(string Name, Predicate<AnonymousParticipantVM> Predicate)
{
    public static ParticipantPredicate None =>
        new ParticipantPredicate("Skupaj", _ => true);

    public static IEnumerable<ParticipantPredicate> AgeGroups =>
         Enum.GetValues<AgeGroups>()
            .Select(a => new ParticipantPredicate(a.GetDisplayAttribute(), p => p.AgeGroup == a))
            .ToList();


    public static IEnumerable<ParticipantPredicate> Genders =>
        Enum.GetValues<Gender>()
            .Select(g => new ParticipantPredicate(g.GetDisplayAttribute(), p => p.Gender == g))
            .ToList();


}

