namespace Mladim.Domain.Models.Survey.Statistics;

public class QuestionResponseTypes
{
    public int QuestionId { get; set; }
    public IEnumerable<SubQuestionResponseTypes> SubQuestionResponseTypes { get; set; } = new List<SubQuestionResponseTypes>();
    public QuestionResponseTypes(int questionId, IEnumerable<SubQuestionResponseTypes> subQuestionResponseTypes)
    {
        this.QuestionId = questionId;
        this.SubQuestionResponseTypes = subQuestionResponseTypes.ToList();
    }
}
