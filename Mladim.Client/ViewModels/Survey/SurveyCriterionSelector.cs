using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;

public abstract class SurveyCriterionSelector
{
    public string Name { get;}
    public List<ParticipantPredicate> ParticipantPredicatesByType { get; set; } = new();
    public SurveyCriterionSelector(string name)
    {
        this.Name = name;
    }

    public static SurveyCriterionSelector GenderSelector() =>
        new GenderSelector();
    public static SurveyCriterionSelector AgeGroupSelector() =>
       new AgeGroupSelector();

    
    
}

public class GenderSelector : SurveyCriterionSelector
{    
    public GenderSelector(): base("Spol") 
    {
        this.ParticipantPredicatesByType = ParticipantPredicate.Genders.ToList();
        this.ParticipantPredicatesByType.Add(ParticipantPredicate.None);
    }
}

public class AgeGroupSelector : SurveyCriterionSelector
{
    public AgeGroupSelector() : base("Starostna skupina")
    {
        this.ParticipantPredicatesByType = ParticipantPredicate.AgeGroups.ToList();
        this.ParticipantPredicatesByType.Add(ParticipantPredicate.None);     
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

