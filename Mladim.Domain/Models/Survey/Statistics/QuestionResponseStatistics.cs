using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Domain.Models.Survey.Statistics;

public class QuestionResponseStatistics
{
    public IEnumerable<ParticipantResponseType> ResponseTypes { get; set; } = new List<ParticipantResponseType>();

    public QuestionResponseStatistics(IEnumerable<ParticipantResponseType> responses)
    {
        this.ResponseTypes = responses.ToList();
    }
}
