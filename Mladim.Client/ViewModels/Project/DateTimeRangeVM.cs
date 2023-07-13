using Mladim.Domain.Models;

namespace Mladim.Client.ViewModels.Project;

public class DateTimeRangeVM
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    private DateTimeRangeVM() { }
    private DateTimeRangeVM(DateTime start, DateTime end) =>
        (Start, End) = (start, end);

    private DateTimeRangeVM(DateTime start, TimeSpan timeSpan) : this(start, start + timeSpan) { }

    public static DateTimeRangeVM Create(DateTime start, DateTime end) =>
        new DateTimeRangeVM(start, end);
    public static DateTimeRangeVM Create(DateTime start, TimeSpan timeSpan) =>
       new DateTimeRangeVM(start, timeSpan);
}

