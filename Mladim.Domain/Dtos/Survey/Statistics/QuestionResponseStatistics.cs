using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;

namespace Mladim.Domain.Dtos.Survey.Statistics
{
    public class SurveyStatisticsDto
    {
        public SurveyQuestionQueryDto SurveyQuestion { get; set; } = default!;
        public IEnumerable<QuestionResponseStatisticsDto> QuestionsStatistics { get; set; } = new List<QuestionResponseStatisticsDto>();

    } 
}
