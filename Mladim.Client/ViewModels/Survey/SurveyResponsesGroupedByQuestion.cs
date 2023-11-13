using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Survey;


public class SurveyResponsesGroupedByQuestion
{
    public SurveyQuestionVM? SurveyQuestion { get; set; }

    public List<ParticipantQuestionResponse> ParticipantQuestionResponses = new();

    public SurveyResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponse> participantQuestionResponses)
    {
        this.SurveyQuestion = surveyQuestion;
        this.ParticipantQuestionResponses = participantQuestionResponses.ToList();
    }


    public static SurveyResponsesGroupedByQuestion Create(SurveyQuestionVM? surveyQuestion, IEnumerable<ParticipantQuestionResponse> participantQuestionResponses)
    {
        return surveyQuestion?.Type switch
        {
            SurveyQuestionType.Text => new SurveyTextResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
            SurveyQuestionType.Rating => new SurveyRatingResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
            SurveyQuestionType.Boolean => new SurveyBoleanResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
            SurveyQuestionType.Multiple => new SurveyButtonResponsesGroupedByQuestion(surveyQuestion, participantQuestionResponses),
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
        
}


public class SurveyRatingResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion, ISurveyParticipantStatistics
{
    public SurveyRatingResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
            IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }

    public List<int> GetNumberOfParticipantsByAge(AgeGroups ageGroup, int numOfQuestion = 0)
    {
        return this.ParticipantQuestionResponses.Where(r => ageGroup.HasFlag(r.AnonymousParticipant.AgeGroup))
            .Select(r => r.QuestionResponse)
            .OfType<QuestionRatingResponseVM>()
            .GroupBy(g => g.Response)
            .Select(g => (type: g.Key, count: g.Count()))
            .UnionBy(Enum.GetValues<SurveyRatingResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
            .OrderBy(g => g.type)
            .Select(g => g.count)
            .ToList();
    }

    public List<int> GetNumberOfParticipantsByGender(Gender gender, int numOfQuestion = 0)
    {

        return this.ParticipantQuestionResponses.Where(r => gender.HasFlag(r.AnonymousParticipant.Gender))
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

public class SurveyBoleanResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion, ISurveyParticipantStatistics
{
    public SurveyBoleanResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }

    public List<int> GetNumberOfParticipantsByAge(AgeGroups ageGroup, int numOfQuestion = 0)
    {
        return this.ParticipantQuestionResponses.Where(r => ageGroup.HasFlag(r.AnonymousParticipant.AgeGroup))
            .Select(r => r.QuestionResponse)
            .OfType<QuestionBooleanResponseVM>()
            .GroupBy(g => g.Response)
            .Select(g => (type: g.Key, count: g.Count()))
            .UnionBy(Enum.GetValues<SurveyBooleanResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
            .OrderBy(g => g.type)
            .Select(g => g.count)
            .ToList();
    }

    public List<int> GetNumberOfParticipantsByGender(Gender gender, int numOfQuestion = 0)
    {
        return this.ParticipantQuestionResponses.Where(r => gender.HasFlag(r.AnonymousParticipant.Gender))
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


public class SurveyButtonResponsesGroupedByQuestion : SurveyResponsesGroupedByQuestion, ISurveyParticipantStatistics
{
    public SurveyButtonResponsesGroupedByQuestion(SurveyQuestionVM? surveyQuestion,
             IEnumerable<ParticipantQuestionResponse> participantQuestionResponses) : base(surveyQuestion, participantQuestionResponses)
    {

    }

    public List<int> GetNumberOfParticipantsByAge(AgeGroups ageGroup, int numOfQuestion = 0)
    {
        return this.ParticipantQuestionResponses.Where(r => ageGroup.HasFlag(r.AnonymousParticipant.AgeGroup))
            .Select(r => r.QuestionResponse)
            .OfType<QuestionMultiButtonResponseVM>()
            .GroupBy(g => g.Response[numOfQuestion].ButtonType)
            .Select(g => (type: g.Key, count: g.Count()))
            .UnionBy(Enum.GetValues<SurveyButtonResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
            .OrderBy(g => g.type)
            .Select(g => g.count)
            .ToList();

    }

    public List<int> GetNumberOfParticipantsByGender(Gender gender, int numOfQuestion = 0)
    {
        return this.ParticipantQuestionResponses.Where(r => gender.HasFlag(r.AnonymousParticipant.Gender))
           .Select(r => r.QuestionResponse)
           .OfType<QuestionMultiButtonResponseVM>()
           .GroupBy(g => g.Response[numOfQuestion].ButtonType)
           .Select(g => (type: g.Key, count: g.Count()))
           .UnionBy(Enum.GetValues<SurveyButtonResponseType>().Select(type => (type, count: 0)), tuple => tuple.type)
           .OrderBy(g => g.type)
           .Select(g => g.count)
           .ToList();
    }
}








public interface ISurveyParticipantStatistics
{
    List<int> GetNumberOfParticipantsByGender(Gender gender, int numOfQuestion = 0);
    List<int> GetNumberOfParticipantsByAge(AgeGroups ageGroup, int numOfQuestion = 0);
}

