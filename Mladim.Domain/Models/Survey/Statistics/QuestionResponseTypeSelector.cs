using Mladim.Domain.Models.Survey.ParticipantResponseTypes;
using Mladim.Domain.Models.Survey.Responses;

namespace Mladim.Domain.Models.Survey.Statistics;

//cel class je za eno vprašanje
public class QuestionResponseTypeSelector // eno vprašanje
{
    public int QuestionId { get; }
    private IEnumerable<ActivityQuestionResponse> QuestionResponses { get; }

    public QuestionResponseTypeSelector(int questionId, IEnumerable<ActivityQuestionResponse> questionResponses)
    {
        this.QuestionId = questionId;
        this.QuestionResponses = questionResponses.ToList();
    }

    public SurveyStatistics AverageQuestionResponseTypes()
    {
        var result = GroupedAverageActivitiesByQuestionResponseTypes()
            .SelectMany(qr => qr.SubQuestionResponseTypes.Select((sqr, index) => (sqr, index)))
            .GroupBy(tuple => tuple.index, (index, tuples) => tuples.Select(tuple => tuple.sqr))
            .Select(AverageSubQuestionResponseTypes).ToList();

        return new SurveyStatistics(this.QuestionId, result);
    }

    private QuestionResponseStatistics AverageSubQuestionResponseTypes(IEnumerable<QuestionResponseStatistics> subQuestions)
    {
        var numOfParticipants = subQuestions.Count();

        var result = subQuestions.SelectMany(sbrt => sbrt.ResponseTypes)
            .GroupBy(prt => prt.ResponseType)
            .Select(g => new ParticipantResponseType(g.Key, AveragePercent(g.ToList(), numOfParticipants)))
            .ToList();

        return new QuestionResponseStatistics(result);
    }

    private float AveragePercent(List<ParticipantResponseType> participantResponseTypes, int len)
    {
        return (float)Math.Round(participantResponseTypes.Sum(prt => prt.Value) / len , 1);
    }

    // eno vprašanje za vse aktivnosti
    private IEnumerable<ActivityQuestionResponseTypes> GroupedAverageActivitiesByQuestionResponseTypes()
    {
        return QuestionResponses.GroupBy(qr => qr.ActivityId, (activityId, qrs) => (activityId, response: qrs.Select(gr => gr.QuestionResponse)))
            .Select(g => ActivityAverageResponseTypes(g.activityId, g.response))
            .ToList();
    }

    // eno vprašanje za eno aktivity
    private ActivityQuestionResponseTypes ActivityAverageResponseTypes(int activityId, IEnumerable<QuestionResponse> questionResponses)
    {
        var result = questionResponses.OfType<SelectableQuestionResponse>()
             .SelectMany(qr => qr.ResponseEnum.Select((r, index) => (r, index)))
             .GroupBy(tuple => tuple.index, (index, tuples) => tuples.Select(tuple => tuple.r))
             .Select(AverageResponseTypes);

        return new ActivityQuestionResponseTypes(activityId, result);
    }


    private QuestionResponseStatistics AverageResponseTypes(IEnumerable<Enum> responseTypes)
    {
        var length = responseTypes.Count();        

        return new QuestionResponseStatistics(responseTypes.GroupBy(rt => rt)
            .Select(g => ParticipantResponseType.Create(g.Key, Percent(g.Count(), length))));
    }

    private float Percent(int element, int length) =>
        (float)Math.Round((float)element / length,1);

}
