using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Queries.GetGroup;

public class GetGroupQuery : IRequest<GroupQueryDto>
{
    public int GroupId { get; set; }
}
