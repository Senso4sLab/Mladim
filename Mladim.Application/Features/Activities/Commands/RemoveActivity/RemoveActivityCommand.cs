using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Commands.RemoveActivity;

public class RemoveActivityCommand : IRequest<bool>
{
    public int ActivityId { get; set; }
}
