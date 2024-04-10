using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;
using Mladim.Domain.Models.Survey.Statistics;

namespace Mladim.Domain.Dtos.Survey.Statistics
{
    public class QuestionSurveyStatisticsDto
    {
        public SurveyQuestionQueryDto SurveyQuestion { get; set; } = default!;
        public SurveyStatisticsDto Statistics { get; set; } = default!; // enako kot SurveyStatistics

    }


    public class SurveyStatisticsDto
    {
        public int QuestionId { get; set; }        
        public List<QuestionResponseStatisticsDto> QuestionsResponseTypes { get; set; } = new List<QuestionResponseStatisticsDto>();
    }


    public class QuestionResponseStatisticsDto
    {
        public IEnumerable<ParticipantResponseTypeDto> ResponseTypes { get; set; } = new List<ParticipantResponseTypeDto>();       
    }

    public class ParticipantResponseTypeDto
    {
        public string ResponseType { get; set; } = default!;
        public float Value { get; set; }
    }
}
