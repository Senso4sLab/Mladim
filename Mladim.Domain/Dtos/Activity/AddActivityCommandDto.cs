using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class AddActivityCommandDto
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ActivityTypes ActivityTypes { get; set; }
    public List<PartnerCommandDto> Partners { get; set; } = new();
    public List<GroupCommandDto> Groups { get; set; } = new();
    public List<ParticipantCommandDto> Participants { get; set; } = new();
    public List<StaffMemberSubjectCommandDto> Staff { get; set; } = new();
    public List<AnonymousParticipantCommandDto> AnonymousParticipants { get; set; } = new();


}
