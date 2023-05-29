using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class ActivityQueryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ActivityTypes ActivityTypes { get; set; }
    public List<PartnerQueryDto> Partners { get; set; } = new();
    public List<GroupQueryDto> Groups { get; set; } = new(); 
    public List<ParticipantQueryDto> Participants { get; set; } = new();
    public List<StaffMemberSubjectQueryDto> Staff { get; set; } = new(); 
    public List<AnonymousParticipantDetailsQueryDto> AnonymousParticipants { get; set; } = new();  

}
