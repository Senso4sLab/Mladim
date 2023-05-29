using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.AddProject;

public class AddProjectCommand : IRequest<ProjectQueryDto>
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? WebpageUrl { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<StaffMemberSubjectCommandDto> Staff { get; set; } = new();
    public List<GroupCommandDto> Groups { get; set; } = new();
    public List<PartnerCommandDto> Partners { get; set; } = new();
}
