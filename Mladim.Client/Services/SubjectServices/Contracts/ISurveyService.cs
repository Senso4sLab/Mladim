using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface ISurveyService
{
    Task<SurveyQuestionnairyVM> GetSurveyQuestionnairyAsync(int activityId, Gender gender);
}
