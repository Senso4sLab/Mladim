namespace Mladim.Domain.Models;

public class ProjectGroup : Group
{
    public List<Project> Projects { get; set; } = new();
}
