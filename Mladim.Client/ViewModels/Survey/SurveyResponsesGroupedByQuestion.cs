using Mladim.Domain.Enums;
using System.Dynamic;


namespace Mladim.Client.ViewModels.Survey;


public abstract class SurveyResponsesGroupedByQuestion
{
    public SurveyQuestionVM? SurveyQuestion { get; set; }

    public List<ParticipantQuestionResponse> ParticipantQuestionResponses = new();

    public SurveyResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponse> participantQuestionResponses)
    {
        this.SurveyQuestion = surveyQuestion;
        this.ParticipantQuestionResponses = participantQuestionResponses.ToList();
    }

    public abstract IAsyncEnumerable<List<int>> NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> participant = null);


    public static SurveyResponsesGroupedByQuestion Create(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponse> participantQuestionResponses)
    {
        return surveyQuestion?.Type switch
        {
            SurveyQuestionType.Text => new SurveyTextResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
            SurveyQuestionType.Rating => new SurveyRatingResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
            SurveyQuestionType.Boolean => new SurveyBoleanResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
            SurveyQuestionType.Multiple => new SurveyButtonGroupResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
            _ => throw new NotImplementedException()
        };
    }
}

public record AnonymousCommand(AnonymousParticipantVM Participant, string Comment);

public class SurveyTextResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public SurveyTextResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
            IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }

    public IEnumerable<AnonymousCommand> GetAnonymousComments()
    {
        return this.ParticipantQuestionResponses.Select(pqr => new AnonymousCommand(pqr.AnonymousParticipant, (pqr.QuestionResponse as QuestionTextResponseVM)!.Response))
            .ToList();

    }

    public async override IAsyncEnumerable<List<int>> NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> participant = null)
    {
        yield return new List<int>(); 
    }
}


public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public SurveyRatingResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
            IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }

    public async override IAsyncEnumerable<List<int>> NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> participant = null)
    {
        yield return this.ParticipantQuestionResponses.Where(pqr => participant(pqr.AnonymousParticipant))
             .Select(r => r.QuestionResponse)
             .OfType<QuestionRatingResponseVM>()
             .GroupBy(g => g.Response)
             .Select(g => (type: g.Key, count: g.Count()))
             .UnionBy(Enum.GetValues<SurveyRatingResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
             .OrderBy(g => g.type)
             .Select(g => g.count)
             .ToList();
    }    
}

public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public SurveyBoleanResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }

    public async override IAsyncEnumerable<List<int>> NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> participant = null)
    {
        yield return this.ParticipantQuestionResponses.Where(pqr => participant(pqr.AnonymousParticipant))
           .Select(r => r.QuestionResponse)
           .OfType<QuestionBooleanResponseVM>()
           .GroupBy(g => g.Response)
           .Select(g => (type: g.Key, count: g.Count()))
           .UnionBy(Enum.GetValues<SurveyBooleanResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
           .OrderBy(g => g.type)
           .Select(g => g.count)
           .ToList();
    }  
}


public class SurveyButtonResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{
    public SurveyButtonResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }
    public override IAsyncEnumerable<List<int>> NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> participant = null)
    {
        throw new NotImplementedException();
    }
}



public class SurveyButtonGroupResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion
{

    public SurveyButtonGroupResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }

    public override IAsyncEnumerable<List<int>> NumberOfParticipantsBy(Predicate<AnonymousParticipantVM> participant = null)
    {

        var query = participant == null ? this.ParticipantQuestionResponses : 
            this.ParticipantQuestionResponses.Where(pqr => participant(pqr.AnonymousParticipant));        

        return query
          .Select(r => r.QuestionResponse)
          .OfType<QuestionMultiButtonResponseVM>()
          .SelectMany(qmb => qmb.Response, (dd, s) => new { Index = dd.Response.IndexOf(s), s.ButtonType })
          .GroupBy(g => g.Index, x => x.ButtonType)
          .Select(s => s.GroupBy(d => d).Select(t => (key: t.Key, count: t.Count())))
          .Select(c => c.UnionBy(Enum.GetValues<SurveyButtonResponseType>().Select(t => (key: t, count: 0)), tuple => tuple.key))
          .OrderBy(o => o.OrderBy(or => or.key))
          .Select(s => s.Select(s => s.count).ToList())
          .ToAsyncEnumerable();
         
         //.GroupBy(g => {     g.Response[numOfQuestion].ButtonType)
         //.Select(g => (type: g.Key, count: g.Count()))
         //.UnionBy(Enum.GetValues<SurveyButtonResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
         //.OrderBy(g => g.type)
         //.Select(g => g.count)
         //.ToList();
    }
}






