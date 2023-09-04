using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;

namespace Mladim.Domain.Dtos;

public class ProjectQueryDto
{
    public int Id { get; set; }
    public ProjectAttributesQueryDto Attributes { get; set; } = default!;
    public DateTimeRangeQueryDto TimeRange { get; set; } = default!;    

}




