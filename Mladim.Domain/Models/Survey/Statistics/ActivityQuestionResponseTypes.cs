namespace Mladim.Domain.Models.Survey.Statistics;

public class ActivityQuestionResponseTypes
{
    public int ActivityId { get; }
    public IEnumerable<QuestionResponseStatistics> SubQuestionResponseTypes { get; } = new List<QuestionResponseStatistics>();
    public ActivityQuestionResponseTypes(int activityId, IEnumerable<QuestionResponseStatistics> subQuestionResponseTypes)
    {
        this.ActivityId = activityId;
        this.SubQuestionResponseTypes = subQuestionResponseTypes.ToList();
    }
}
