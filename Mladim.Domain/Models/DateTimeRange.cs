namespace Mladim.Application.Features.Projects.Commands.UpdateProject;

public class DateTimeRange : IEquatable<DateTimeRange>
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    private DateTimeRange()
    {
        
    }

    private DateTimeRange(DateTime start, DateTime stop)
    {
        this.Start = start;
        this.End = stop;
    }

    private DateTimeRange(DateTime start, TimeSpan timeSpan) : this(start, start + timeSpan)
    {       
    }

    public static DateTimeRange Create(DateTime start, DateTime end) => 
        new DateTimeRange(start, end);


    public static DateTimeRange Create(DateTime start, TimeSpan timeSpan) =>
       new DateTimeRange(start, timeSpan);


    public override bool Equals(object? obj) =>
        obj is DateTimeRange && Equals((DateTimeRange)obj);
    

    public bool Equals(DateTimeRange? other) =>
        other?.Start == this.Start && other?.End == this.End;
    
}
