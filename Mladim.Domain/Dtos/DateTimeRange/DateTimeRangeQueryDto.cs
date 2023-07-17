using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.DateTimeRange;

public class DateTimeRangeQueryDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? StartTime { get; set; } = TimeSpan.Zero;
    public TimeSpan? EndTime { get; set; } = TimeSpan.Zero;
}
