using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Commands.UpdateActivity;

public class UpdateActivityCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan? StartHour { get; set; }
    public TimeSpan? EndHour { get; set; }
    public ActivityTypes ActivityTypes { get; set; }
    public List<PartnerCommandDto> Partners { get; set; } = new();
    public List<StaffMemberCommandDto> Staff { get; set; } = new();
    public List<ParticipantCommandDto> Participants { get; set; } = new();    
    public List<AnonymousParticipantCommandDto> AnonymousParticipantActivities { get; set; } = new();
    public List<GroupCommandDto> Groups { get; set; } = new();

}
