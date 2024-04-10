namespace Mladim.Domain.Dtos.Survey.Responses;


//public class QuestionResponseTypesDto
//{    
//    public IEnumerable<SubQuestionResponseTypesDto> SubQuestionResponseTypes { get; set; } = new List<SubQuestionResponseTypesDto>();   
//}


//public class SubQuestionResponseTypesDto
//{
//    public IEnumerable<ParticipantResponseTypeDto> ResponseTypes { get; set; } = new List<ParticipantResponseTypeDto>();
//}


public class QuestionResponseStatisticsDto
{
    public IEnumerable<ParticipantResponseTypeDto> ResponseTypes { get; set; } = new List<ParticipantResponseTypeDto>();
}

public record ParticipantResponseTypeDto(string ResponseType, float Value);
