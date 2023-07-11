using MediatR;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommand : IRequest<int>
{
    public int GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<int> Members { get; set; } = new();
    public MemberType MemberType { get; set; }
}
