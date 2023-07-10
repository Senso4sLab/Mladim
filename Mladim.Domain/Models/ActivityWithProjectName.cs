using Mladim.Domain.Enums;
using System.Runtime.CompilerServices;

namespace Mladim.Domain.Models;

public class ActivityWithProjectName : Activity
{
    public string ProjectName { get; private set; } = string.Empty;

    public static ActivityWithProjectName Create(int id, ActivityAttributes attibutes, DateTimeRange dateTimeRange, string projectName) =>
        new ActivityWithProjectName
        {
            Id = id,
            BaseActivityAttributes = attibutes,
            DateTimeRange = dateTimeRange,          
            ProjectName = projectName
        };
       
}

