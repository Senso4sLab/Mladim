using Mladim.Domain.Models.Survey.Responses;

namespace Mladim.Application.Contracts.Persistence;

public interface ISurveyResponseRepository : IGenericRepository<AnonymousSurveyResponse>
{
    Task<List<AnonymousSurveyResponse>> GetSurveyResponseByOrganizationIdAsync(int organizationId);
    Task<List<AnonymousSurveyResponse>> GetSurveyResponseByProjectIdAsync(int projectId);
}
