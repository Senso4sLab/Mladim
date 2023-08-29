using Mladim.Domain.Models;

namespace Mladim.Client.ViewModels;

public class DateTimeRangeVM
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? StartTime { get; set; } = TimeSpan.Zero;
    public TimeSpan? EndTime { get; set; } = TimeSpan.Zero;

    public DateTimeRangeVM() { }


    public static DateTimeRangeVM Create(DateTime start, DateTime end,  TimeSpan? startTime, TimeSpan? endTime) =>
        new DateTimeRangeVM() { StartDate = start, EndDate = end, StartTime = startTime, EndTime = endTime };




}

