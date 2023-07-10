using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IActivityRepository : IGenericRepository<Activity>
{
    Task<IEnumerable<ActivityWithProjectName>> GetActivitiesWithProjectName(int organizationId);
    Task<Activity?> GetActivityDetailsAsync(int activityId, bool tracking = true);
}
