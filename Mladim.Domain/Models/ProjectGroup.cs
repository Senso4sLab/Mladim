namespace Mladim.Domain.Models;

public class ProjectGroup : Group
{
    private ProjectGroup()
    {

    }
    public ProjectGroup(int id)
    {
        this.Id = id;   
    }

    private ProjectGroup(string name, string description, IEnumerable<Member> members)
    {
        this.Name = name;
        this.Description = description;
        this.Members = members.ToList();
    }

    public List<Project> Projects { get; set; } = new();

    public static ProjectGroup Create(int id) =>
        new ProjectGroup(id);

    public static ProjectGroup Create(string name, string description, IEnumerable<Member> members) =>
        new ProjectGroup(name, description, members);


}
