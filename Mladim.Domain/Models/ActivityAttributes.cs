using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public class ActivityAttributes : BaseAttibutes
{
    public ActivityTypes ActivityTypes { get; protected set; }
    
   
    private ActivityAttributes(string name, string description, ActivityTypes activityTypes)
    {
        this.Name = name;
        this.Description = description;
        this.ActivityTypes = activityTypes;
       
    }

    public static ActivityAttributes Create(string name, string description, ActivityTypes activityTypes) =>
        new ActivityAttributes(name, description, activityTypes);
}


