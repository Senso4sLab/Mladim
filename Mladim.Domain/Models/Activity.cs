using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; } =string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public TimeSpan? StartHour { get; set; }
    public TimeSpan? EndHour { get; set; }

    public ActivityTypes ActivityTypes { get; set; }

    public IEnumerable<StaffMember> LeadStaff =>
        staff.Where(s => s.IsLead).Select(s => s.StaffMember);

    public IEnumerable<StaffMember> Staff => 
        staff.Where(s => !s.IsLead).Select(s => s.StaffMember);    


    private List<StaffMemberActivity> staff { get; set; } = new();    
    
    public List<ActivityGroup> Groups { get; set; } = new();
       
    public List<AnonymousParticipantGroup> AnonymousParticipantGroups { get; set; } = new();

    public List<Partner> Partners { get; set; } = new();

    public List<Participant> Participants { get; set; } = new();

    //public int OrganizationId { get; set; }
    //public Organization Organization { get; set; } = default!;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
}


