using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos.Attributes;

public class ActivityAttributesQueryDto
{
    public ActivityTypes ActivityTypes { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsGroup { get; set; }
    public bool IsRepetitive { get; set; }
    public ActivityRepetitiveInterval RepetitiveInterval { get; set; }
    public int NumOfRepetitions { get; set; }

}
