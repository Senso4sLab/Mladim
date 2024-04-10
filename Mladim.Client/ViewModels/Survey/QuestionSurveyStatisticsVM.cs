using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Statistics;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Client.ViewModels.Survey;


public class QuestionSurveyStatisticsVM
{
    public SurveyQuestionVM SurveyQuestion { get; set; } = default!;

    public SurveyStatisticsVM Statistics { get; set; } = default!;
}

public class SurveyStatisticsVM
{
    public int QuestionId { get; set; }
    public List<QuestionResponseStatisticsVM> QuestionsResponseTypes { get; set; } = new List<QuestionResponseStatisticsVM>();
}



public class QuestionResponseStatisticsVM
{
    public IEnumerable<ParticipantResponseTypeVM> ResponseTypes { get; set; } = new List<ParticipantResponseTypeVM>();  
}

public class ParticipantResponseTypeVM
{
    public string ResponseType { get; set; } = default!;
    public float Value { get; set; }
} 

