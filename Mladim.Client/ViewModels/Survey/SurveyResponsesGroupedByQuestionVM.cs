using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
namespace Mladim.Client.ViewModels.Survey;


public interface ITextableReponseType
{
    IEnumerable<ParticipantTextResponse> GetParticipantTextResponse();
}

public interface ISelectableReponseType
{
    IEnumerable<string> ExistingResponseTypes { get; }
    IEnumerable<ParticipantsPerResponseTypes> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name);
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

    public virtual IEnumerable<ParticipantsPerResponseTypes> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
        new List<ParticipantsPerResponseTypes>()
        {
             new ParticipantsPerResponseTypes(name, this.Question!, this.ParticipantQuestionResponses
                .Where(pqr => predicate(pqr.AnonymousParticipant))
                .GroupBy(pqr => pqr.ToString(), (key, sequence) => new ParticipantsPerResponseType(key!, sequence.Count()))
                .UnionBy(InitialParticipantResponses(), ppr => ppr.ResponseType)
                .OrderBy(g => g.ResponseType)
                .ToList()),
        };

    public abstract IEnumerable<string> ExistingResponseTypes { get; }
    private IEnumerable<ParticipantsPerResponseType> InitialParticipantResponses() =>
        this.ExistingResponseTypes
            .Select(type => new ParticipantsPerResponseType(type, 0))
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
    public override IEnumerable<string> ExistingResponseTypes =>
        Enum.GetValues<SurveyRatingResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}



public record ParticipantsPerResponseType(string ResponseType, int NumOfParticipants);
public record ParticipantsPerResponseTypes(string Criterion, string Question, List<ParticipantsPerResponseType> ParticipantsPerType);


public class SurveyBoleanResponsesGroupedByQuestion : SurveySelectableResponsesGroupedByQuestionVM<SurveyBooleanResponseType>
{
    public SurveyBoleanResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();
    }

    public override IEnumerable<string> ExistingResponseTypes =>
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
    public override IEnumerable<string> ExistingResponseTypes =>
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

    public override IEnumerable<string> ExistingResponseTypes =>
         Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}






