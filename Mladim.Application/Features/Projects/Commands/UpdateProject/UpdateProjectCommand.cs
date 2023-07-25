using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.AttachedFile;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<int>
{
    public int Id { get; set; }
    public ProjectAttributesCommandDto Attributes { get; set; } = default!;
    public DateTimeRangeQueryDto TimeRange { get; set; } = default!;
    public List<StaffMemberCommandDto> Staff { get; set; } = new();
    public List<GroupCommandDto> Groups { get; set; } = new();
    public List<PartnerCommandDto> Partners { get; set; } = new();     
    public List<AttachedFileCommandDto> Files { get;set; } = new();
}
