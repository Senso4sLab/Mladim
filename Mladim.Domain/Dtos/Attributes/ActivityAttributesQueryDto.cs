using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos.Attributes;

public class ActivityAttributesQueryDto
{
    public ActivityTypes ActivityTypes { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
   
}
