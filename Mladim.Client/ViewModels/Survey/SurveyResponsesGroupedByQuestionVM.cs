using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mladim.Client.Extensions;
using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
using System.Linq;

namespace Mladim.Client.ViewModels.Survey;


public interface ITextableReponseType
{
    IEnumerable<ParticipantTextResponse> GetParticipantTextResponse();
}

public interface ISelectableReponseType
{
    IEnumerable<ParticipantsByResponseType> ParticipantsByResponseTypes(Predicate<AnonymousParticipantVM> predicate);
    IEnumerable<ParticipantsByResponseType> ResponseTypes { get; }
   
}


public abstract class SurveyResponsesGroupedByQuestionVM
{
    public string? QuestionDescription { get; set; } = null;
    public string? Question { get; set; } = null;
    public abstract IEnumerable<AnonymousParticipantVM> AnonymousParticipant { get; }

    public SurveyResponsesGroupedByQuestionVM() {}   

    public static SurveyResponsesGroupedByQuestionVM Create(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses)
    {
        return surveyQuestion?.Type switch
        {
            SurveyQuestionType.Text => new SurveyTextResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            SurveyQuestionType.Rating => new SurveyRatingResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            SurveyQuestionType.Boolean => new SurveyBoleanResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            SurveyQuestionType.Multiple => new SurveyButtonGroupResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            _ => throw new NotImplementedException()
        };
    }   
}

public abstract class SurveyResponsesGroupedByQuestionVM<T> : SurveyResponsesGroupedByQuestionVM
{

    public List<ParticipantQuestionResponseVM<T>> ParticipantQuestionResponses = new();

    public override IEnumerable<AnonymousParticipantVM> AnonymousParticipant =>
        ParticipantQuestionResponses.Select(pqr => pqr.AnonymousParticipant)
        .ToList();
    public SurveyResponsesGroupedByQuestionVM(IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
    {
        ParticipantQuestionResponses = participantQuestionResponses.OfType<ParticipantQuestionResponseVM<T>>().ToList();
    }
}

public record ParticipantTextResponse(AnonymousParticipantVM Participant, string TextResponse);

public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<string>, ITextableReponseType
{
    public SurveyTextResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();
    }    

    public IEnumerable<ParticipantTextResponse> GetParticipantTextResponse() =>    
        this.ParticipantQuestionResponses.Select(pqr => new ParticipantTextResponse(pqr.AnonymousParticipant, pqr.Response)).ToList();  
}


public abstract class SurveySelectableResponsesGroupedByQuestionVM<T> : SurveyResponsesGroupedByQuestionVM<T>, ISelectableReponseType   
{

    //public List<ParticipantQuestionResponseVM<T>> ParticipantQuestionResponses = new();
    public SurveySelectableResponsesGroupedByQuestionVM(IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses)
        :base(participantQuestionResponses)
    {
       // ParticipantQuestionResponses = participantQuestionResponses.OfType<ParticipantQuestionResponseVM<T>>().ToList();
    }

    public virtual IEnumerable<ParticipantsByResponseType> ParticipantsByResponseTypes(Predicate<AnonymousParticipantVM> predicate) =>        
            this.ParticipantQuestionResponses
                .Where(pqr => predicate(pqr.AnonymousParticipant))
                .GroupBy(pqr => pqr.ToString(), (key, sequence) => new ParticipantsByResponseType(key!, sequence.Count()))
                .UnionBy(ResponseTypes, ppr => ppr.ResponseType)
                .OrderBy(g => g.ResponseType)
                .ToList();   

    public abstract IEnumerable<ParticipantsByResponseType> ResponseTypes { get; }
   
}





public class SurveyRatingResponsesGroupedByQuestion : SurveySelectableResponsesGroupedByQuestionVM<SurveyRatingResponseType>
{    
    public SurveyRatingResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();

    }
    public override IEnumerable<ParticipantsByResponseType> ResponseTypes =>
        Enum.GetValues<SurveyRatingResponseType>()
         .Select(type => ParticipantsByResponseType.Default(type.GetDisplayAttribute()))
         .ToList();
}



public record ParticipantsByResponseType(string ResponseType, float Unit)
{

    public static ParticipantsByResponseType Default(string responseType) => 
        new ParticipantsByResponseType(responseType, 0);
    public ParticipantsByResponseType InPercent(float numOfParticipants) =>
        new ParticipantsByResponseType(ResponseType, (float) Math.Round((this.Unit * 100 / numOfParticipants), 1));


}

public class ParticipantsByCriterion
{
   public string Criterion { get; }
    public IEnumerable<ParticipantsByResponseType> ParticipantsByReponseTypes { get; } = new List<ParticipantsByResponseType>();
    public ParticipantsByCriterion(string criterion, IEnumerable<ParticipantsByResponseType> participantsByReponseTypes)
    {
        this.Criterion = criterion;
        this.ParticipantsByReponseTypes = participantsByReponseTypes.ToList();
    }
    public float Sum() => 
        this.ParticipantsByReponseTypes.Sum(pbr => pbr.Unit);

    public ParticipantsByCriterion InPercent(float numOfParticipants) =>
       new ParticipantsByCriterion(this.Criterion, ParticipantsByReponseTypes.Select(prt => prt.InPercent(numOfParticipants)));


}
public class ContingencyTable
{
    public string Name { get; }
    public IEnumerable<ParticipantsByCriterion> ParticipantsByCriteria { get; } = new List<ParticipantsByCriterion>();
    public ContingencyTable(string name, IEnumerable<ParticipantsByCriterion> participantsByCriteria)
    {
        this.Name = name;
        this.ParticipantsByCriteria = participantsByCriteria.ToList();
    }
}


public class SurveyBoleanResponsesGroupedByQuestion : SurveySelectableResponsesGroupedByQuestionVM<SurveyBooleanResponseType>
{
    public SurveyBoleanResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();
    }

    public override IEnumerable<ParticipantsByResponseType> ResponseTypes =>
      Enum.GetValues<SurveyBooleanResponseType>()
       .Select(type => ParticipantsByResponseType.Default(type.GetDisplayAttribute()))
       .ToList();   
}


public class SurveyButtonResponseGroupedByQuestion : SurveySelectableResponsesGroupedByQuestionVM<SurveryButtonResponseVM>
{   
    public SurveyButtonResponseGroupedByQuestion(string question, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = question; 
    }   

    public override IEnumerable<ParticipantsByResponseType> ResponseTypes =>
        Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => ParticipantsByResponseType.Default(type.GetDisplayAttribute()))
         .ToList();

}



public class SurveyButtonGroupResponsesGroupedByQuestion : SurveySelectableResponsesGroupedByQuestionVM<List<SurveryButtonResponseVM>>
{
    public List<SurveyButtonResponseGroupedByQuestion> ButtonGroupResponses { get; set; } = new();
    public SurveyButtonGroupResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses)
        :base(participantQuestionResponses)
    {
        this.QuestionDescription = questions.FirstOrDefault();
        this.Question = null;

        var response = questions.Skip(1)
            .Select((q, index) => new SurveyButtonResponseGroupedByQuestion(q, ParticipantQuestionResponses
                .Select(pqr => ParticipantQuestionResponseVM.Create(pqr.AnonymousParticipant,
                    QuestionResponseVM.Create(pqr.UniqueQuestionId, pqr.Response[index])))));               

        this.ButtonGroupResponses.AddRange(response);
    }
    public override IEnumerable<ParticipantsByResponseType> ParticipantsByResponseTypes(Predicate<AnonymousParticipantVM> predicate) =>    
        this.ButtonGroupResponses.SelectMany(bgr => bgr.ParticipantsByResponseTypes(predicate)).ToList();   
       
    public override IEnumerable<ParticipantsByResponseType> ResponseTypes =>
      Enum.GetValues<SurveyButtonResponseType>()
       .Select(type => ParticipantsByResponseType.Default(type.GetDisplayAttribute()))
       .ToList();
}



public class CogntigencyTableContext
{
    public UnitSelector UnitSelector { get; set; } = UnitSelector.PercentagesAsUnit();
    public SurveyCriterionSelector CriterionSelector { get; set; } = SurveyCriterionSelector.GenderSelector();  

    public ContingencyTable GetContingencyTableFor(ISelectableReponseType responses)
    {
        return new ContingencyTable(CriterionSelector.Name, GetParticipantsByResponseTypes(responses, CriterionSelector.ParticipantPredicatesByType));
    }
    public IEnumerable<ParticipantsByCriterion> GetParticipantsByResponseTypes(ISelectableReponseType responses, IEnumerable<ParticipantPredicate> participantPredicates) =>
      participantPredicates.Select(pp => GetParticipantsByResponseTypes(responses, pp))
          .ToList();

    public ParticipantsByCriterion GetParticipantsByResponseTypes(ISelectableReponseType responses, ParticipantPredicate participantPredicate) =>
         new ParticipantsByCriterion(participantPredicate.Name, UnitSelector.ParticipantCalculationUnit(responses, participantPredicate));

}



public abstract class UnitSelector
{
    public string Type { get; }
    
    public UnitSelector(string type)
    {        
       this.Type = type;      
    }

    public static UnitSelector ParticipantsAsUnit() =>
        new NumOfParticipantsUnit();

    public static UnitSelector PercentagesAsUnit() =>
        new PercantagesUnit();

    public abstract IEnumerable<ParticipantsByResponseType> ParticipantCalculationUnit(ISelectableReponseType responses, ParticipantPredicate participantPredicate);


}

public class NumOfParticipantsUnit : UnitSelector
{   
    public NumOfParticipantsUnit() 
        : base("Št. udeležencev")
    {
       
    }

    public override IEnumerable<ParticipantsByResponseType> ParticipantCalculationUnit(ISelectableReponseType responses, ParticipantPredicate participantPredicate)
    {
        return responses.ParticipantsByResponseTypes(participantPredicate.Predicate).ToList();
    }
}

public class PercantagesUnit : UnitSelector
{
    public PercantagesUnit()
        :base("Procenti")
    {
        
    }
    private float NumOfParticipants(ISelectableReponseType responses) =>
        responses.ParticipantsByResponseTypes(ParticipantPredicate.None.Predicate)
                 .Sum(prt => prt.Unit);

    public override IEnumerable<ParticipantsByResponseType> ParticipantCalculationUnit(ISelectableReponseType responses, ParticipantPredicate participantPredicate)
    {
        float numOfParticipants = NumOfParticipants(responses);

        return responses.ParticipantsByResponseTypes(participantPredicate.Predicate)
            .Select(pc => pc.InPercent(numOfParticipants))
            .ToList();
    }
}








