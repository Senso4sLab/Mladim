using Mladim.Client.ViewModels.Activity;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;

namespace Mladim.Client.ViewModels;

public class ActivityWithProjectNameVM
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public ActivityAttributesVM Attributes { get; set; } = default!;
    public DateTimeRangeVM TimeRange { get; set; } = default!;



    public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-10);
    public DateTime EndDate { get; set; } = DateTime.Now;
    public string ActivityName { get; set; } = "sasasa";


   
}
