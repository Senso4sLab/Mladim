using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.AnonymousParticipants.Commands.AddAnonymousParticipant;

public class AddAnonymousParticipantCommand : IRequest<bool>
{
    public int ActivityId { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public int Number { get; set; } = 1;  
}
