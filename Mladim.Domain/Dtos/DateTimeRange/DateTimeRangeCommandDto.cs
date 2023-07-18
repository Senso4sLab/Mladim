using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.DateTimeRange;

public class DateTimeRangeCommandDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? StartTime { get; set; } = TimeSpan.Zero;
    public TimeSpan? EndTime { get; set; } = TimeSpan.Zero;

    public DateTimeRangeCommandDto()
    {
        
    }
    private DateTimeRangeCommandDto(DateTime startDate, DateTime endDate, TimeSpan? startTime, TimeSpan? endTime) =>
        (StartDate, EndDate, StartTime, EndTime) = (startDate, endDate, startTime, endTime);
   
    public static DateTimeRangeCommandDto Create(DateTime startDate, DateTime endDate,  TimeSpan? startTime, TimeSpan? endTime) =>
        new DateTimeRangeCommandDto(startDate, endDate, startTime, endTime);
}
