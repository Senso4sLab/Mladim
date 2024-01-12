using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Domain.Models.Survey.Statistics;

public class SubQuestionResponseTypes
{
    public IEnumerable<ParticipantResponseType> ResponseTypes { get; set; } = new List<ParticipantResponseType>();

    public SubQuestionResponseTypes(IEnumerable<ParticipantResponseType> responses)
    {
        this.ResponseTypes = responses.ToList();
    }

}
