using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IActivityRepository : IGenericRepository<Activity>
{
    public Task<IEnumerable<Activity>> GetActivitiesWithParticipantsAsync(Expression<Func<Activity, bool>> predicate);
    Task<IEnumerable<ActivityWithProjectName>> GetActivitiesWithProjectName(Expression<Func<Activity, bool>> predicate, int? upcomingActivities);
    Task<Activity?> GetActivityDetailsAsync(int activityId, bool tracking = true);
}
