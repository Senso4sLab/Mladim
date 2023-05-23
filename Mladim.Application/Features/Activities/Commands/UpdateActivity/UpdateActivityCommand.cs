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
    public ActivityTypes ActivityTypes { get; set; }
    public List<PartnerBaseDto> Partners { get; set; } = new();
    public List<StaffMemberSubjectBaseDto> Staff { get; set; } = new();
    public List<ParticipantBaseDto> Participants { get; set; } = new();    
    public List<AnonymousParticipantActivityBaseDto> AnonymousParticipants { get; set; } = new();
    public List<GroupBaseDto> Groups { get; set; } = new();

}
