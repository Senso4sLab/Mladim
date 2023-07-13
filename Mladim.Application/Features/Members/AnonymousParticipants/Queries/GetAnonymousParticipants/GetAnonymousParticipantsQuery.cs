using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.AnonymousParticipants.Queries.GetAnonymousParticipants;

public class GetAnonymousParticipantsQuery : IRequest<IEnumerable<AnonymousParticipantQueryDto>>
{
    public int ActivityId { get; set; }

    //public int? ProjectId { get; set; } TODO
}
