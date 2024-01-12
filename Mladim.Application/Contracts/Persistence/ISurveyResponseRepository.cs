using Mladim.Domain.Models.Survey.Responses;

namespace Mladim.Application.Contracts.Persistence;

public interface ISurveyResponseRepository : IGenericRepository<AnonymousSurveyResponse>
{
    Task<List<AnonymousSurveyResponse>> GetSurveyResponsesByQuestionIdsAndOrganizationAsync(int organizationId, int? year);
    Task<List<AnonymousSurveyResponse>> GetSurveyResponsesByQuestionIdsAndProjectAsync(int projectId);
}
