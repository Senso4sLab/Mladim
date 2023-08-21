using Mladim.Domain.Enums;
using System.Runtime.CompilerServices;

namespace Mladim.Domain.Models;








public class ActivityWithProjectName : Activity
{  

    public NamedEntity Project { get; set; } = default!;

    private ActivityWithProjectName(int id, ActivityAttributes attibutes, DateTimeRange timeRange, int projectId, string projectName) =>
        (Id, Attributes, TimeRange, Project) = (id, attibutes, timeRange, NamedEntity.Create(projectId, projectName));

    public static ActivityWithProjectName Create(int projectId, string projectName, Activity activity) =>
        new ActivityWithProjectName(activity.Id, activity.Attributes, activity.TimeRange, projectId, projectName);        
       
}

