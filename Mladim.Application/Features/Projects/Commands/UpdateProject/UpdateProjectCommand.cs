using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string WebpageUrl { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<MemberProjectDto> ProjectMembers { get; set; } = new();
    public List<PartnerDto> Partners { get; set; } = new();
}
