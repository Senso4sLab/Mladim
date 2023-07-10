namespace Mladim.Domain.Models;

public class ActivityGroup : Group
{
    private ActivityGroup()
    {

    }
    public ActivityGroup(int id)
    {
        this.Id = id;
    }
   
    public static ActivityGroup Create(int id) =>
        new ActivityGroup(id);
    public List<Activity> Activities { get; set; } = new();
}
