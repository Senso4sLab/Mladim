using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Models.Survey.ParticipantResponseTypes;

namespace Mladim.Client.ViewModels.Survey;


public class SurveyStatisticsVM
{    
    public IEnumerable<QuestionResponseStatisticsVM> QuestionsStatistics { get; set; } = new List<QuestionResponseStatisticsVM>();

    public SurveyQuestionVM SurveyQuestion { get; set; } = default!;
}



public class QuestionResponseStatisticsVM
{
    public IEnumerable<ParticipantResponseType> ResponseTypes { get; set; } = new List<ParticipantResponseType>();  
}


//public class ResponseStatisticsVM
//{  
//    public IEnumerable<QuestionResponseStatisticsVM> QuestionResponses { get; set; } = new List<QuestionResponseStatisticsVM>();
//}


//public record ParticipantResponseTypeVM(string ResponseType, float Value);

