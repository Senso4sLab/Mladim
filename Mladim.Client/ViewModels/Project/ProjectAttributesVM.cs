namespace Mladim.Client.ViewModels.Project;

public class ProjectAttributesVM
{
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public string? WebpageUrl { get; private set; }
}
