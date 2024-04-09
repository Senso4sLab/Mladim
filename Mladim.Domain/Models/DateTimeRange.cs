using Mladim.Domain.Enums;

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

    public static DateTimeRange Create(DateTime start, DateTime end) => 
        new DateTimeRange(start, end, TimeSpan.Zero, TimeSpan.Zero);
       
    public void OffsetDateTimeForRepetitiveInterval(ActivityRepetitiveInterval repetitiveInterval)
    {
        switch (repetitiveInterval)
        {
            case ActivityRepetitiveInterval.Daily:
                StartDate = StartDate.AddDays(1);
                EndDate = EndDate.AddDays(1);
                break;
            case ActivityRepetitiveInterval.Monthly:
                StartDate = StartDate.AddMonths(1);
                EndDate = EndDate.AddMonths(1);                
                break;
            case ActivityRepetitiveInterval.Weekly:                
                StartDate = StartDate.AddDays(7);
                EndDate = EndDate.AddDays(7);                
                break;
            default:
                throw new NotImplementedException();
        }
    }


    public DateTimeRange Clone()
    {
        return (DateTimeRange)this.MemberwiseClone();
    }



    public bool IsDateTimeInRange(DateTime dateTime) => 
        StartDate <= dateTime && EndDate >= dateTime;

    public bool OverlapWith(DateTimeRange range) =>
       range.StartDate <= this.StartDate && range.EndDate >= this.EndDate;
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
