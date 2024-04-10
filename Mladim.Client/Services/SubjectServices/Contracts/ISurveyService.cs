using Mladim.Client.ViewModels.Survey;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface ISurveyService
{
    Task<IEnumerable<AnonymousSurveyResponseVM>> GetAnonymousSurveyResponsesAsync(int activityId);
    Task<IEnumerable<SurveyQuestionVM>> GetSurveyQuestionnairyAsync(int activityId, Gender gender);
    Task<bool> PostAnonymousSurveyResponseAsync(int activityId, AnonymousSurveyResponseVM anonymousSurveyResponse);    
    Task<IEnumerable<SurveyStatisticsVM>> GetStatisticsByProjectIdIdAsync(int projectId);
    Task<IEnumerable<SurveyStatisticsVM>> GetStatisticsByOrganizationIdAsync(int organizationId, DateTime start, DateTime end);
}
