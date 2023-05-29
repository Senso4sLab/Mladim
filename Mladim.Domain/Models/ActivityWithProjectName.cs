using Mladim.Domain.Enums;
using System.Runtime.CompilerServices;

namespace Mladim.Domain.Models;

public class ActivityWithProjectName : Activity
{
    public string ProjectName { get; set; }

    public static ActivityWithProjectName Create(Activity activity, string projectName) =>
        new ActivityWithProjectName
        {
            Id = activity.Id,
            Name = activity.Name,
            Description = activity.Description,
            Start = activity.Start,
            End = activity.End,
            ActivityTypes = activity.ActivityTypes,
            ProjectName = projectName
        };
       
}

