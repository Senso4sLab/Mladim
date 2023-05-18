using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Commands.AddActivity;

public class AddActivityCommand :IRequest<ActivityDto>
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ActivityTypes ActivityTypes { get; set; }
    public List<PartnerDto> Partners { get; set; } = new();
    public List<MemberActivityDto> ActivityMembers { get; set; } = new();
    public List<AnonymousParticipantsDto> AnonymousParticipantGroups { get; set; } = new();
}
