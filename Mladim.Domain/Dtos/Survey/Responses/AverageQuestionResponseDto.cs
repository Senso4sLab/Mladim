namespace Mladim.Domain.Dtos.Survey.Responses;

public class AverageQuestionResponseDto
{
    public QuestionResponseDto Response { get; set; } = default!;
    public float Percent { get;set; }
}
