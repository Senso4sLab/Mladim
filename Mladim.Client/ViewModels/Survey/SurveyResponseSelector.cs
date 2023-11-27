using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;

public abstract class SurveyResponseSelector
{
    public string Name { get;}

    public List<ParticipantPredicate> ParticipantPredicatesByType { get; set; } = new();
    public SurveyResponseSelector(string name)
    {
        this.Name = name;
    }

    public static SurveyResponseSelector CreateGenderSelector() =>
        new GenderSurveyResponseSelector();

    public static SurveyResponseSelector CreateAgeGroupSelector() =>
       new AgeGroupSurveyResponseSelector();
}

public class GenderSurveyResponseSelector : SurveyResponseSelector
{    
    public GenderSurveyResponseSelector(): base("Spol") 
    {
        this.ParticipantPredicatesByType = ParticipantPredicate.Genders.ToList();
        this.ParticipantPredicatesByType.Add(ParticipantPredicate.None);
    }
}

public class AgeGroupSurveyResponseSelector : SurveyResponseSelector
{
    public AgeGroupSurveyResponseSelector() : base("Starostna skupina")
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

