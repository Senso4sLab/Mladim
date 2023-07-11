using Mladim.Domain.Enums;
using System.Runtime.CompilerServices;

namespace Mladim.Domain.Models;

public class ActivityWithProjectName : Activity
{
    public string ProjectName { get; private set; } = string.Empty;

    private ActivityWithProjectName(int id, ActivityAttributes attibutes, DateTimeRange timeRange, string projectName) =>
        (Id, Attributes, TimeRange, ProjectName) = (id, attibutes, timeRange, projectName);

    public static ActivityWithProjectName Create(string projectName, Activity activity) =>
        new ActivityWithProjectName(activity.Id, activity.Attributes, activity.TimeRange, projectName);        
       
}

