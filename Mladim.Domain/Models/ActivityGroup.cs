namespace Mladim.Domain.Models;

public class ActivityGroup : Group
{
    private ActivityGroup()
    {

    }

    private ActivityGroup(int id)
    {
        this.Id= id;
    }
    internal ActivityGroup(string name, string description, IEnumerable<Member> members) : base(name, description, members) 
    { 
    }   

    public List<Activity> Activities { get; set; } = new();


    public static ActivityGroup Create(int id) =>
       new ActivityGroup(id);
}
