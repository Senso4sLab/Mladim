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
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    //public List<ActiveMember> ActiveMember { get; set; } = new();


    public List<MemberActivity> ActivityMembers { get; set; } = new();
    public List<AnonymousParticipants> AnonymousParticipantGroups { get; set; } = new();

    public List<Partner> Partners { get; set; } = new();

    public ActivityTypes ActivityTypes { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
}
