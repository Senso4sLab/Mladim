using Mladim.Domain.Dtos.Survey.Questions;

namespace Mladim.Domain.Dtos.Survey.Responses;

public class SurveyStatisticsQueryDto
{
    public List<AverageQuestionResponseDto> AverageResponses { get; set; } = new();
    public SurveyQuestionQueryDto Question { get; set; } = default!;
}
