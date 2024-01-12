using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface ISurveyService
{
    Task<IEnumerable<AnonymousSurveyResponseVM>> GetAnonymousSurveyResponsesAsync(int activityId);
    Task<IEnumerable<SurveyQuestionVM>> GetSurveyQuestionnairyAsync(int activityId, Gender gender);
    Task<bool> PostAnonymousSurveyResponseAsync(int activityId, AnonymousSurveyResponseVM anonymousSurveyResponse);    
    Task<IEnumerable<QuestionResponseStatisticsVM>> GetStatisticsByProjectIdIdAsync(int projectId);
    Task<IEnumerable<QuestionResponseStatisticsVM>> GetStatisticsByOrganizationIdAsync(int organizationId, int year);
}
