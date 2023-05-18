using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class ProjectDto
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


