using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.DeleteGroup;

public class DeleteProjectGroupCommand : IRequest<bool>
{
    public int GroupId { get; set; }
}
