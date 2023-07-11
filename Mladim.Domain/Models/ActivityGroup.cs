namespace Mladim.Domain.Models;

public class ActivityGroup : Group
{
    private ActivityGroup() { }    
    internal ActivityGroup(int id) : base(id) { }
    internal ActivityGroup(string name, string description, IEnumerable<Member> members) : base(name, description, members) { }
    public List<Activity> Activities { get; set; } = new();      
}
