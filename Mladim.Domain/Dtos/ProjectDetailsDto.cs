using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class ProjectDetailsDto : ProjectDto
{
    public List<ActivityDto> Activities { get; set; }
}
