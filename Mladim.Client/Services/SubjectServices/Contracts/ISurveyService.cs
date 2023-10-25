using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface ISurveyService
{
    Task<IEnumerable<SurveyQuestionVM>> GetSurveyQuestionnairyAsync(int activityId, Gender gender);
}
