namespace Mladim.Domain.Models;

public class DateTimeRange : IEquatable<DateTimeRange>
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public TimeSpan? StartTime { get;  private set; } = TimeSpan.Zero;
    public TimeSpan? EndTime { get; private set; } = TimeSpan.Zero;

    private DateTimeRange() { }
    private DateTimeRange(DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime) =>
        (StartDate, EndDate, StartTime, EndTime) = (startDate, endDate, startTime, endTime);   

    public static DateTimeRange Create(DateTime start, DateTime end, int startHour = 0, int endHour = 0) => 
        new DateTimeRange(start, end, TimeSpan.FromHours(startHour), TimeSpan.FromHours(endHour));
    
    public override bool Equals(object? obj) =>
        obj is DateTimeRange && Equals((DateTimeRange)obj);
    public bool Equals(DateTimeRange? other) =>
        other?.StartDate == this.StartDate &&
        other?.EndDate == this.EndDate &&
        other?.StartTime == this.StartTime &&
        other?.EndTime == this.EndTime;
    public override int GetHashCode() =>
        HashCode.Combine(this.StartDate, this.EndDate, this.StartTime, this.EndTime);
    
}
