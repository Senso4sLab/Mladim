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
    IEnumerable<ParticipantsByResponseType> NumOfParticipantsByResponseTypes(Predicate<AnonymousParticipantVM> predicate);
    //IEnumerable<string> ExistingResponseTypes { get; }
    //IEnumerable<ParticipantsPerResponseTypes> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name);
}


public abstract class SurveyResponsesGroupedByQuestionVM
{
    public string? QuestionDescription { get; set; } = null;
    public string? Question { get; set; } = null;

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


public abstract class SurveySelectableResponsesGroupedByQuestionVM<T> : SurveyResponsesGroupedByQuestionVM, ISelectableReponseType   
{

    public List<ParticipantQuestionResponseVM<T>> ParticipantQuestionResponses = new();
    public SurveySelectableResponsesGroupedByQuestionVM(IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses)
    {
        ParticipantQuestionResponses = participantQuestionResponses.OfType<ParticipantQuestionResponseVM<T>>().ToList();
    }

    public IEnumerable<ParticipantsByResponseType> NumOfParticipantsByResponseTypes(Predicate<AnonymousParticipantVM> predicate) =>        
            this.ParticipantQuestionResponses
                .Where(pqr => predicate(pqr.AnonymousParticipant))
                .GroupBy(pqr => pqr.ToString(), (key, sequence) => new ParticipantsByResponseType(key!, sequence.Count()))
                .UnionBy(InitResponseTypes(), ppr => ppr.ResponseType)
                .OrderBy(g => g.ResponseType)
                .ToList();              
    


    public virtual IEnumerable<ParticipantsPerResponseTypes> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
        new List<ParticipantsPerResponseTypes>()
        {
             new ParticipantsPerResponseTypes(name, this.Question!, this.ParticipantQuestionResponses
                .Where(pqr => predicate(pqr.AnonymousParticipant))
                .GroupBy(pqr => pqr.ToString(), (key, sequence) => new ParticipantsByResponseType(key!, sequence.Count()))
                .UnionBy(InitResponseTypes(), ppr => ppr.ResponseType)
                .OrderBy(g => g.ResponseType)
                .ToList()),
        };

    public abstract IEnumerable<string> ResponseTypes { get; }
    private IEnumerable<ParticipantsByResponseType> InitResponseTypes() =>
        this.ResponseTypes
            .Select(type => new ParticipantsByResponseType(type, 0))
            .ToList();
}





public class SurveyRatingResponsesGroupedByQuestion : SurveySelectableResponsesGroupedByQuestionVM<SurveyRatingResponseType>
{    
    public SurveyRatingResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();

    }
    public override IEnumerable<string> ResponseTypes =>
        Enum.GetValues<SurveyRatingResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}



public record ParticipantsByResponseType(string ResponseType, float Unit)
{
    public ParticipantsByResponseType ToPercent(float numOfParticipants) =>
        new ParticipantsByResponseType(ResponseType, (float)Math.Round((this.Unit * 100 / numOfParticipants), 1));


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
        ParticipantsByReponseTypes.Sum(pbr => pbr.Unit);


}
public record ParticipantsPerResponseTypes(string Criterion, string Question, List<ParticipantsByResponseType> ParticipantsPerType);


public class ContingencyTable
{
    public string Name { get; }
    public IEnumerable<ParticipantsByCriterion> ParticipantsByCriteria { get; } = new List<ParticipantsByCriterion>();
    public ContingencyTable(string name, IEnumerable<ParticipantsByCriterion> participantsByCriteria)
    {
        this.Name = name;
        this.ParticipantsByCriteria = participantsByCriteria;
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

    public override IEnumerable<string> ResponseTypes =>
         Enum.GetValues<SurveyBooleanResponseType>()
         .Select(type => type.GetDisplayAttribute())
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
    public override IEnumerable<string> ResponseTypes =>
         Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => type.GetDisplayAttribute()) 
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

    public override IEnumerable<ParticipantsPerResponseTypes> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
        this.ButtonGroupResponses.SelectMany(bgr => bgr.NumberOfParticipantsByCriterion(predicate, name)).ToList();   

    public override IEnumerable<string> ResponseTypes =>
         Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}



public abstract class ContingencyCalculator
{
    protected string Name { get; }
    protected List<ParticipantPredicate> ParticipantPredicatesByTypes { get; } = new();
    public ContingencyCalculator(string name, IEnumerable<ParticipantPredicate> participantPredicatesByTypes)
    {
        this.Name = name;
        this.ParticipantPredicatesByTypes = participantPredicatesByTypes.ToList();
        this.ParticipantPredicatesByTypes.Add(ParticipantPredicate.None);
    }

    public abstract ContingencyTable GetContingencyTableFor(ISelectableReponseType responses);
    public IEnumerable<ParticipantsByCriterion> GetParticipantsByResponseTypes(ISelectableReponseType responses, IEnumerable<ParticipantPredicate> participantPredicates) =>
      participantPredicates.Select(pp => GetParticipantsByResponseTypes(responses, pp))
          .ToList();


    public ParticipantsByCriterion GetParticipantsByResponseTypes(ISelectableReponseType responses, ParticipantPredicate participantPredicates) =>    
         new ParticipantsByCriterion(participantPredicates.Name, responses.NumOfParticipantsByResponseTypes(participantPredicates.Predicate)
             .ToList());
    


}

public class ContingencyTableParticipants : ContingencyCalculator
{   
    public ContingencyTableParticipants(string name, IEnumerable<ParticipantPredicate> participantPredicatesByTypes) 
        : base(name, participantPredicatesByTypes)
    {
       
    }
    public override ContingencyTable GetContingencyTableFor(ISelectableReponseType responses) =>
        new ContingencyTable(this.Name, GetParticipantsByResponseTypes(responses, this.ParticipantPredicatesByTypes));
        
}

public class ContingencyTablePercentages : ContingencyCalculator
{
    public ContingencyTablePercentages(string name, IEnumerable<ParticipantPredicate> participantPredicatesByTypes)
        : base(name, participantPredicatesByTypes)
    {
        
    }
    private float NumOfParticipants(ISelectableReponseType responses) =>
        this.GetParticipantsByResponseTypes(responses, ParticipantPredicate.None)
            .Sum();
             

    public override ContingencyTable GetContingencyTableFor(ISelectableReponseType responses)
    {
        var numOfParticipants = NumOfParticipants(responses);
        var participantByCriteria = this.GetParticipantsByResponseTypes(responses, this.ParticipantPredicatesByTypes)
            .Select(pc => new ParticipantsByCriterion(pc.Criterion, pc.ParticipantsByReponseTypes.Select(pt => pt.ToPercent(numOfParticipants))))
            .ToList();
            
        return new ContingencyTable(this.Name, participantByCriteria);
    }

    
}








