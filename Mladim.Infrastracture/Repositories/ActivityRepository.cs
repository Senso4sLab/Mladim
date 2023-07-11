using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
{
    public ActivityRepository(ApplicationDbContext context) : base(context) {}

    public async Task<Activity?> GetActivityDetailsAsync(int activityId, bool tracking = true)
    {
        var activity = this.DbSet
            .Include(p => p.Staff)
            .Include(p => p.Partners)
            .Include(p => p.Groups)
            .Include(p => p.Participants);

        return tracking ? await activity.AsTracking().FirstOrDefaultAsync(a => a.Id == activityId)
            : await activity.AsNoTracking().FirstOrDefaultAsync(a => a.Id == activityId);
    }

    public async Task<IEnumerable<ActivityWithProjectName>> GetActivitiesWithProjectName(int organizationId) =>    
         await this.DbSet.Where(a => a.Project.OrganizationId == organizationId)
            .Select(a => ActivityWithProjectName.Create(a.Project.Attributes.Name, a))
            .ToListAsync();

}

