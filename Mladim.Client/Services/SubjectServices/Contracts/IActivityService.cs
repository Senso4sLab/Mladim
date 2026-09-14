using Mladim.Client.ViewModels;

namespace Mladim.Client.Services.SubjectServices.Contracts;

public interface IActivityService
{
    Task<bool> AddAsync(ActivityVM activity, int projectId);
    Task<IEnumerable<ActivityWithProjectNameVM>> GetByProjectIdAsync(int projectId, int? upcommingActivities = null);
    Task<ActivityVM?> GetByActivityIdAsync(int activityId);
    Task<bool> RemoveAsync(int activityId);
    Task<bool> UpdateAsync(ActivityVM activity);
    Task<IEnumerable<ActivityWithProjectNameVM>> GetByOrganizationIdAsync(int organizationId, int? upcommingActivities = null);

    Task<IEnumerable<ActivityStatisticVM>> GetStatistics(int organizationId, DateTime start, DateTime end);

    Task<ActivitySummaryVM?> GetActivitySummaryAsync(int activityId);
}
