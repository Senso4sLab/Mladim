namespace Mladim.Domain.Dtos.Attributes;

public class ProjectAttributesCommandDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? WebpageUrl { get; set; }
}
