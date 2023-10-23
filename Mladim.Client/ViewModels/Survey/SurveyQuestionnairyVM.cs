using Mladim.Domain.Dtos.Survey.Questions;

namespace Mladim.Client.ViewModels.Survey;

public class SurveyQuestionnairyVM
{
    public int Id { get; set; }
    public List<SurveyQuestionVM> Questions { get; set; } = new();
}
