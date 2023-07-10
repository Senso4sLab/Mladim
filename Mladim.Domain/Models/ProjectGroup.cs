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

    internal ProjectGroup(string name, string description, IEnumerable<Member> members) : base(name, description, members)
    {
       
    }

    public List<Project> Projects { get; set; } = new();

    public static ProjectGroup Create(int id) => 
        new ProjectGroup(id);

}
