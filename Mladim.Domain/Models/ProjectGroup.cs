namespace Mladim.Domain.Models;

public class ProjectGroup : Group
{
    private ProjectGroup() { }
    internal ProjectGroup(int id) : base(id) { }
    public ProjectGroup(string name, string description, IEnumerable<Member> members, int organizationId) 
        : base(name, description, members, organizationId) { }
    public List<Project> Projects { get; set; } = new();
}
