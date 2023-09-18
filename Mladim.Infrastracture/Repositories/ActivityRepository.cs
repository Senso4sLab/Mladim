using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Enums;
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
    public ActivityRepository(ApplicationDbContext context) : base(context) { }

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


    public async Task<IEnumerable<Activity>> GetActivitiesWithParticipantsAsync(Expression<Func<Activity, bool>> predicate)
    {
        var activities = await this.DbSet
            .Where(predicate)
            .Include(p => p.Groups)
            .ThenInclude(ag => ag.Members)
            .Include(p => p.Participants)
            .AsNoTracking()
            .ToListAsync();

        return activities;
    }


    public async Task<IEnumerable<ActivityWithProjectName>> GetActivitiesWithProjectName(Expression<Func<Activity, bool>> predicate, int? upcomingActivities)
    {
        var sequence = this.DbSet.Where(predicate);

        if (upcomingActivities is int numActivities)
        {
            var currentDate = DateTime.UtcNow;
            sequence = sequence.Where(a => a.TimeRange.StartDate > currentDate)
                .OrderByDescending(a => a.TimeRange.StartDate)
                .Take(numActivities);
        }

        return await sequence.Select(a => ActivityWithProjectName.Create(a.ProjectId, a.Project.Attributes.Name, a))
            .ToListAsync();
    }


    public async Task<IEnumerable<ActivityWithProjectName>> GetActivitiesWithProjectNameAndStaffMember(Expression<Func<Activity, bool>> predicate,  int? upcomingActivities)
    {
        var sequence = this.DbSet
            .Include(a => a.Project)
            .ThenInclude(p => p.Staff)
            .ThenInclude(s => s.StaffMember)
            .Where(predicate);

        if (upcomingActivities is int numActivities)
        {
            var currentDate = DateTime.UtcNow;
            sequence = sequence.Where(a => a.TimeRange.StartDate > currentDate)
                .OrderByDescending(a => a.TimeRange.StartDate)
                .Take(numActivities);
        }

        return await sequence.Select(a => ActivityWithProjectName.Create(a.ProjectId, a.Project.Attributes.Name, a))
            .ToListAsync();
    }



}

