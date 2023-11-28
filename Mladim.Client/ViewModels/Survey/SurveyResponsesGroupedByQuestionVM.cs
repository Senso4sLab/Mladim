using Mladim.Domain.Enums;
using Mladim.Domain.Extensions;
namespace Mladim.Client.ViewModels.Survey;


public abstract class SurveyResponsesGroupedByQuestionVM
{
    public string? QuestionDescription { get; set; } = null;
    public string? Question { get; set; } = null;

    public SurveyResponsesGroupedByQuestionVM() {}

    public abstract IEnumerable<string> ExistingResponses { get;}

    public abstract int NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> predicate);
    public abstract IEnumerable<SurveyParticipantRow> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name);

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
    public override int NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> predicate) =>
        ParticipantQuestionResponses.Where(pqr => predicate(pqr.AnonymousParticipant)).Count();

    public override IEnumerable<SurveyParticipantRow> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
        new List<SurveyParticipantRow>()
        {
             new SurveyParticipantRow(name, this.Question!, this.ParticipantQuestionResponses
                .Where(pqr => predicate(pqr.AnonymousParticipant))
                .GroupBy(pqr => pqr.ToString(), (key, sequence) => new ParticipantsPerExistingResponse(key!, sequence.Count()))              
                .UnionBy(InitialParticipantResponses(), ppr => ppr.ResponseType)
                .OrderBy(g => g.ResponseType)
                .ToList()),
        };   
    

    private IEnumerable<ParticipantsPerExistingResponse> InitialParticipantResponses() =>
        this.ExistingResponses
            .Select(type => new ParticipantsPerExistingResponse(type, 0))
            .ToList(); 

}

public record AnonymousCommand(AnonymousParticipantVM Participant, string Comment);

public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<string>
{
    public SurveyTextResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();
    }

    public IEnumerable<AnonymousCommand> GetAnonymousComments()
    {
        return this.ParticipantQuestionResponses.Select(pqr => new AnonymousCommand(pqr.AnonymousParticipant, pqr.Response))
            .ToList();
    }

    public override IEnumerable<string> ExistingResponses =>
        new List<string>();

   
}


public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveyRatingResponseType>
{    
    public SurveyRatingResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();

    }
    public override IEnumerable<string> ExistingResponses =>
        Enum.GetValues<SurveyRatingResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}



public record ParticipantsPerExistingResponse(string ResponseType, int NumOfParticipants);

public record SurveyParticipantRow(string Criterion, string Question, List<ParticipantsPerExistingResponse> ParticipantsPerType);


public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveyBooleanResponseType>
{
    public SurveyBoleanResponsesGroupedByQuestion(IEnumerable<string> questions, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = questions.FirstOrDefault();
    }

    public override IEnumerable<string> ExistingResponses =>
         Enum.GetValues<SurveyBooleanResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}


public class SurveyButtonResponseGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<SurveryButtonResponseVM>
{   
    public SurveyButtonResponseGroupedByQuestion(string question, IEnumerable<ParticipantQuestionResponseVM> participantQuestionResponses) 
        : base(participantQuestionResponses)
    {
        this.QuestionDescription = null;
        this.Question = question; 
    }
    public override IEnumerable<string> ExistingResponses =>
         Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => type.GetDisplayAttribute()) 
         .ToList();

}



public class SurveyButtonGroupResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestionVM<List<SurveryButtonResponseVM>>
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

    public override IEnumerable<SurveyParticipantRow> NumberOfParticipantsByCriterion(Predicate<AnonymousParticipantVM> predicate, string name) =>
        this.ButtonGroupResponses.SelectMany(bgr => bgr.NumberOfParticipantsByCriterion(predicate, name)).ToList();   

    public override IEnumerable<string> ExistingResponses =>
         Enum.GetValues<SurveyButtonResponseType>()
         .Select(type => type.GetDisplayAttribute())
         .ToList();
}






