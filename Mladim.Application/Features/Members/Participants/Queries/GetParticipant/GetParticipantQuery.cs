using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Participants.Queries.GetParticipant;

public class GetParticipantQuery : IRequest<ParticipantDto>
{
    public int ParticipantId { get; set; }
}
