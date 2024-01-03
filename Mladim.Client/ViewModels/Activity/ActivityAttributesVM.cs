using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Activity;

public class ActivityAttributesVM
{
    public string Name { get;  set; } = string.Empty;
    public string Description { get;  set; } = string.Empty;    
    public bool IsGroup { get; set; } 
    public bool IsRepetitive { get; set; }
    public ActivityRepetitiveInterval RepetitiveInterval { get; set; }
    public int NumOfRepetitions { get; set; }
    public IEnumerable<ActivityTypes> ActivityTypes { get; set; } = new List<ActivityTypes>();    
}
