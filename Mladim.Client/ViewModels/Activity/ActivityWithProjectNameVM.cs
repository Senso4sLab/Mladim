using Mladim.Client.ViewModels.Activity;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;

namespace Mladim.Client.ViewModels;

public class ActivityWithProjectNameVM
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public ActivityAttributesVM Attributes { get; set; } = default!;
    public DateTimeRangeVM DateTimeRange { get; set; } = default!;
}
