namespace Mladim.Domain.Models;

public class ActivityGroup : Group
{
    private ActivityGroup() { }    
    internal ActivityGroup(int id) : base(id) { }
    public ActivityGroup(string name, string description, IEnumerable<Member> members, int organizationId)
        : base(name, description, members, organizationId) { }

    public List<Activity> Activities { get; set; } = new();      
}
