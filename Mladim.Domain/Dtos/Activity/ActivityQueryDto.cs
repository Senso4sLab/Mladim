using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class ActivityQueryDto
{
    public int Id { get; set; }
    public ActivityAttributesQueryDto  Attributes { get;  set; } = default!;
    public DateTimeRangeQueryDto DateTimeRange { get;  set; } = default!;
}
