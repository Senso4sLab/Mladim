using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.AddProject;

public class AddProjectCommand : IRequest<ProjectDto>
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string WebpageUrl { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<StaffMemberSubjectBaseDto> Staff { get; set; } = new();
    public List<GroupBaseDto> Groups { get; set; } = new();
    public List<PartnerBaseDto> Partners { get; set; } = new();
}
