namespace Mladim.Domain.Models.Survey.Statistics;

public class ActivityQuestionResponseTypes
{
    public int ActivityId { get; }
    public IEnumerable<SubQuestionResponseTypes> SubQuestionResponseTypes { get; } = new List<SubQuestionResponseTypes>();
    public ActivityQuestionResponseTypes(int activityId, IEnumerable<SubQuestionResponseTypes> subQuestionResponseTypes)
    {
        this.ActivityId = activityId;
        this.SubQuestionResponseTypes = subQuestionResponseTypes.ToList();
    }
}
