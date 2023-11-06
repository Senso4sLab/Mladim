using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivityName;

public class GetActivityNameQuery : IRequest<string>
{
    public int ActivityId { get; set; }
}
