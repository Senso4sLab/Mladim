using Mladim.Domain.Models;

namespace Mladim.Client.ViewModels;

public class DateTimeRangeVM
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? StartTime { get; set; } = TimeSpan.Zero;
    public TimeSpan? EndTime { get; set; } = TimeSpan.Zero;

    private DateTimeRangeVM() { }  
   
}

