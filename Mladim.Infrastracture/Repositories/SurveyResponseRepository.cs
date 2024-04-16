using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Infrastracture.Persistance;

namespace Mladim.Infrastracture.Repositories;

public class SurveyResponseRepository : GenericRepository<AnonymousSurveyResponse>, ISurveyResponseRepository
{
    public SurveyResponseRepository(ApplicationDbContext context) : base(context) {}
    public async Task<List<AnonymousSurveyResponse>> GetSurveyResponsesByQuestionIdsAndOrganizationAsync(int organizationId, DateTimeRange? dateRange = null)
    {
        var responses = this.DbSet
          .Where(r => r.Activity.Project.OrganizationId == organizationId).AsQueryable(); 
        
        if (dateRange is not null)
            responses = responses.Where(r => (r.Activity.TimeRange.StartDate >= dateRange.StartDate) && (r.Activity.TimeRange.EndDate <= dateRange.EndDate));

        return await responses.AsNoTracking().ToListAsync();
    }

    public async Task<List<AnonymousSurveyResponse>> GetSurveyResponsesByQuestionIdsAndProjectAsync(int projectId)
    {
        var responses = this.DbSet
           .Where(r => r.Activity.ProjectId == projectId);         

        return await responses.AsNoTracking().ToListAsync();           
    }
}
