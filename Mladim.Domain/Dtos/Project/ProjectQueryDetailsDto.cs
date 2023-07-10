using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;


public class ProjectQueryDetailsDto
{
    public int Id { get; set; }
    public ProjectAttributesQueryDto Attributes { get; set; } = default!;
    public DateTimeRangeQueryDto DateTimeRange { get; set; } = default!;
    public List<StaffMemberQueryDto> Staff { get; set; } = new();
    public List<GroupQueryDto> Groups { get; set; } = new();
    public List<PartnerQueryDto> Partners { get; set; } = new();
}




