namespace Mladim.Domain.Models;

public class ActivityGroup : Group
{
    public List<Activity> Activities { get; set; } = new();
}
