using Mladim.Domain.Dtos.Survey.Questions;

namespace Mladim.Client.ViewModels.Survey;


public class QuestionResponseStatisticsVM
{
    public QuestionResponseTypesVM QuestionResponseTypes { get; set; } 

    public SurveyQuestionVM SurveyQuestion { get; set; } 
}

public class QuestionResponseTypesVM
{
    public int QuestionId { get; set; }
    public IEnumerable<SubQuestionResponseTypesVM> SubQuestionResponseTypes { get; set; } = new List<SubQuestionResponseTypesVM>();
}


public class SubQuestionResponseTypesVM
{
    public IEnumerable<ParticipantResponseTypeVM> ResponseTypes { get; set; } = new List<ParticipantResponseTypeVM>();
}

public record ParticipantResponseTypeVM(string ResponseType, float Value);

