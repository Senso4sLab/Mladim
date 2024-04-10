using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.Responses;

namespace Mladim.Application.Contracts.Persistence;

public interface ISurveyResponseRepository : IGenericRepository<AnonymousSurveyResponse>
{
    Task<List<AnonymousSurveyResponse>> GetSurveyResponsesByQuestionIdsAndOrganizationAsync(int organizationId, DateTimeRange? dateRange = null);
    Task<List<AnonymousSurveyResponse>> GetSurveyResponsesByQuestionIdsAndProjectAsync(int projectId);
}
