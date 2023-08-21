using Mladim.Client.ViewModels.Activity;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;

namespace Mladim.Client.ViewModels;

public class ActivityWithProjectNameVM
{
    public int Id { get; set; }
    public NamedEntityVM Project { get; set; } = default!;
    public ActivityAttributesVM Attributes { get; set; } = default!;
    public DateTimeRangeVM TimeRange { get; set; } = default!;   
}



