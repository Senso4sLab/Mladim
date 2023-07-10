using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.AddGroup;

public class AddProjectGroupCommand : IRequest<bool>
{
    public int ProjectId { get; set; }
    public int GroupId { get; set; }
}
