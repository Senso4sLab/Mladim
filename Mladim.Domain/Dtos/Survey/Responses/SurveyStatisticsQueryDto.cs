using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Domain.Dtos.Survey.Responses;


public class QuestionResponseTypesDto
{
    public int QuestionId { get; set; }
    public IEnumerable<SubQuestionResponseTypesDto> SubQuestionResponseTypes { get; set; } = new List<SubQuestionResponseTypesDto>();   
}


public class SubQuestionResponseTypesDto
{
    public IEnumerable<ParticipantResponseTypeDto> ResponseTypes { get; set; } = new List<ParticipantResponseTypeDto>();
}

public record ParticipantResponseTypeDto(string ResponseType, float Value);
