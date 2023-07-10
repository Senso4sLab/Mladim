namespace Mladim.Domain.Dtos.Attributes;

public class ProjectAttributesCommandDto
{
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public string? WebpageUrl { get; set; }
}
