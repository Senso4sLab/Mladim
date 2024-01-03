using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models.Survey.Responses;
using Mladim.Infrastracture.Persistance;

namespace Mladim.Infrastracture.Repositories;

public class SurveyResponseRepository : GenericRepository<AnonymousSurveyResponse>, ISurveyResponseRepository
{
    public SurveyResponseRepository(ApplicationDbContext context) : base(context)
    {
    }


    public async Task<List<AnonymousSurveyResponse>> GetSurveyResponseByOrganizationIdAsync(int organizationId)
    {
        var responses = this.DbSet
          .Include(r => r.Responses)
          .Where(r => r.Activity.Project.OrganizationId == organizationId);

        return await responses.AsNoTracking().ToListAsync();
    }

    public async Task<List<AnonymousSurveyResponse>> GetSurveyResponseByProjectIdAsync(int projectId)
    {
        var responses = this.DbSet
           .Include(r => r.Responses)
           .Where(r => r.Activity.ProjectId == projectId);

        return await responses.AsNoTracking().ToListAsync();           
    }


}
