using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.DateTimeRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.AddProject;

public class AddProjectCommand : IRequest<ProjectQueryDetailsDto>
{
    public int OrganizationId { get; set; }
    public ProjectAttributesCommandDto Attributes { get; set; } = default!;
    public DateTimeRangeCommandDto DateTimeRange { get; set; } = default!;
    public List<StaffMemberCommandDto> Staff { get; set; } = new();
    public List<GroupCommandDto> Groups { get; set; } = new();
    public List<PartnerCommandDto> Partners { get; set; } = new();
}
