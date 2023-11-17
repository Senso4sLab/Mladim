using Mladim.Domain.Enums;
using Mladim.Client.Extensions;
using Mladim.Domain.Extensions;

namespace Mladim.Client.ViewModels.Survey;


public abstract class SurveyResponsesGroupedByQuestion
{
    public string? Question {  get; set; }
    public List<ParticipantQuestionResponse> ParticipantQuestionResponses = new();

    public SurveyResponsesGroupedByQuestion(string? question, IEnumerable<ParticipantQuestionResponse> participantQuestionResponses)
    {
       this.Question = question;
       this.ParticipantQuestionResponses = participantQuestionResponses.ToList();
    }

    public abstract SurveyParticipantRow NumberOfParticipantsByCriterion(ParticipantPredicate participantPredicate);

    //public abstract string PrintResultsCsvFormat();

    public static SurveyResponsesGroupedByQuestion Create(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponse> participantQuestionResponses)
    {
        return surveyQuestion?.Type switch
        {
            SurveyQuestionType.Text => new SurveyTextResponsesGroupedByQuestion(surveyQuestion.Texts.FirstOrDefault(), participantQuestionResponses),
            SurveyQuestionType.Rating => new SurveyRatingResponsesGroupedByQuestion(surveyQuestion.Texts.FirstOrDefault(), participantQuestionResponses),
            SurveyQuestionType.Boolean => new SurveyBoleanResponsesGroupedByQuestion(surveyQuestion.Texts.FirstOrDefault(), participantQuestionResponses),
            SurveyQuestionType.Multiple => new SurveyButtonGroupResponsesGroupedByQuestion(surveyQuestion.Texts, participantQuestionResponses),
            _ => throw new NotImplementedException()
        };
    }
}

public record AnonymousCommand(AnonymousParticipantVM Participant, string Comment);

public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public SurveyTextResponsesGroupedByQuestion(string? question,
            IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(question, participantQuestionResponses)
    {

    }

    public IEnumerable<AnonymousCommand> GetAnonymousComments()
    {
        return this.ParticipantQuestionResponses.Select(pqr => new AnonymousCommand(pqr.AnonymousParticipant, (pqr.QuestionResponse as QuestionTextResponseVM)!.Response))
            .ToList();

    }

    public override SurveyParticipantRow NumberOfParticipantsByCriterion(ParticipantPredicate participantPredicate) =>
        new SurveyParticipantRow(string.Empty, new List<(string SurveyReponseType, int NumOfParticipants)>());  
  
}


public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public SurveyRatingResponsesGroupedByQuestion(string? question,
            IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(question, participantQuestionResponses)
    {

    }

    public override SurveyParticipantRow NumberOfParticipantsByCriterion(ParticipantPredicate pp)
    {

        return new SurveyParticipantRow(pp.Name, this.ParticipantQuestionResponses
             .Where(pqr => pp.Predicate(pqr.AnonymousParticipant))
             .Select(r => r.QuestionResponse)
             .OfType<QuestionRatingResponseVM>()
             .GroupBy(g => g.Response)
             .Select(g => (type: g.Key, count: g.Count()))
             .UnionBy(Enum.GetValues<SurveyRatingResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
             .OrderBy(g => g.type)
             .Select(g => (g.type.GetDisplayAttribute(), g.count))
             .ToList());
    }    
}


public record SurveyParticipantRow(string Criterion, List<(string SurveyReponseType, int NumOfParticipants)> ParticipantsByType);


public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public SurveyBoleanResponsesGroupedByQuestion(string? question,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(question, participantQuestionResponses)
    {

    }

    public override SurveyParticipantRow NumberOfParticipantsByCriterion(ParticipantPredicate pp)
    {
        return new SurveyParticipantRow(pp.Name, 
              this.ParticipantQuestionResponses
             .Where(pqr => pp.Predicate(pqr.AnonymousParticipant))
             .Select(r => r.QuestionResponse)
             .OfType<QuestionBooleanResponseVM>()
             .GroupBy(g => g.Response)
             .Select(g => (type: g.Key, count: g.Count()))
             .UnionBy(Enum.GetValues<SurveyBooleanResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
             .OrderBy(g => g.type)
             .Select(g => (g.type.GetDisplayAttribute(), g.count))
             .ToList());
    }  
}


public class SurveyButtonResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    private int Index;
    public SurveyButtonResponsesGroupedByQuestion(string? question, int index,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(question, participantQuestionResponses)
    {
        this.Index = index;
    }
    public override SurveyParticipantRow NumberOfParticipantsByCriterion(ParticipantPredicate pp)
    {
        return new SurveyParticipantRow(pp.Name,
            this.ParticipantQuestionResponses
           .Where(pqr => pp.Predicate(pqr.AnonymousParticipant))
           .Select(r => r.QuestionResponse)
           .OfType<QuestionMultiButtonResponseVM>()
           .GroupBy(g => g.Response[this.Index].ButtonType)
           .Select(g => (type: g.Key, count: g.Count()))
           .UnionBy(Enum.GetValues<SurveyButtonResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
           .OrderBy(g => g.type)
           .Select(g => (g.type.GetDisplayAttribute(), g.count))
           .ToList());
    }
}



public class SurveyButtonGroupResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public List<SurveyButtonResponsesGroupedByQuestion> ButtonGroupResponses { get; set; } = new ();
    public SurveyButtonGroupResponsesGroupedByQuestion(IEnumerable<string> questions,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(questions.FirstOrDefault(), participantQuestionResponses)
    {
        ButtonGroupResponses.AddRange(questions.Skip(1).Select((q, index) => new SurveyButtonResponsesGroupedByQuestion(q, index, participantQuestionResponses)).ToList()); 
    }

    public override SurveyParticipantRow NumberOfParticipantsByCriterion(ParticipantPredicate pp)
    {
        return new SurveyParticipantRow(pp.Name, this.ButtonGroupResponses
            .SelectMany(bgr => bgr.NumberOfParticipantsByCriterion(pp).ParticipantsByType).ToList());

    }
}






