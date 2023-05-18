using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.RemoveProject;

public class RemoveProjectCommand : IRequest<bool>
{
    public int ProjectId { get; set; }
}
