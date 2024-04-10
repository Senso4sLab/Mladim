namespace Mladim.Domain.Models.Survey.Statistics;

public class SurveyStatistics
{
    public int QuestionId { get; set; }

    //SubQuestionResponseTypes -> QuestionResponseStatistics
    public IEnumerable<QuestionResponseStatistics> SubQuestionResponseTypes { get; set; } = new List<QuestionResponseStatistics>();

    
    public SurveyStatistics(int questionId, IEnumerable<QuestionResponseStatistics> subQuestionResponseTypes)
    {
        this.QuestionId = questionId;
        this.SubQuestionResponseTypes = subQuestionResponseTypes.ToList();
    }
}
