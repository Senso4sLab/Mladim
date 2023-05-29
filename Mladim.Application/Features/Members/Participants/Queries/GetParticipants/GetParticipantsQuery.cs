using MediatR;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipant;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Participants.Queries.GetParticipants;

public class GetParticipantsQuery : IRequest<IEnumerable<MemberBase>>
{
    public int? OrganizationId { get; set; }    
    public int? ActivityId { get; set; }
    public bool IsActive { get; set; } = true;

    public bool ParentClass { get; set; }
}
