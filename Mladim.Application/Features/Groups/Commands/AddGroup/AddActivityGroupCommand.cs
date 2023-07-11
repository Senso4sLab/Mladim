using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.AddGroup;

public class AddActivityGroupCommand : IRequest<bool>
{
    public int ActivityId { get; set; }
    public int GroupId { get; set; }
}
