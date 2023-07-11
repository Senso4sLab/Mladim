namespace Mladim.Domain.Models;

public class ProjectGroup : Group
{
    private ProjectGroup() { }
    internal ProjectGroup(int id) : base(id) { }
    internal ProjectGroup(string name, string description, IEnumerable<Member> members) : base(name, description, members) { }  
    public List<Project> Projects { get; set; } = new();
}
