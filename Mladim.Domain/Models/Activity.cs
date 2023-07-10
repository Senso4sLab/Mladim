using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;



public class Activity : BaseEntity<int>
{
    public ActivityAttributes Attributes { get; protected set; } = default!;
    public DateTimeRange DateTimeRange { get; protected set; } = default!;
   
    protected Activity()
    {
            
    }

    private Activity(DateTimeRange dateTimeRange, ActivityAttributes baseActivityAttibutes)
    {
        this.DateTimeRange = dateTimeRange;
        this.Attributes = baseActivityAttibutes;
    }

    public static Activity Create(DateTime start, DateTime end, string name,
        string description, ActivityTypes activityTypes, IEnumerable<Partner> partners,
        IEnumerable<StaffMemberRole> staffRole, IEnumerable<ActivityGroup> groups, 
        IEnumerable<Participant> participants, IEnumerable<AnonymousParticipantGroup>anonymousParticipantGroups) =>
            new Activity(DateTimeRange.Create(start, end), ActivityAttributes.Create(name, description, activityTypes))
            {
                Groups = groups.ToList(),
                Partners = partners.ToList(),
                Participants = participants.ToList(),
                AnonymousParticipantGroups = anonymousParticipantGroups.ToList(),
                Staff = staffRole.Select(sr => StaffMemberActivity.Create(sr.StaffMember, 0, sr.IsLead)).ToList(),
            };

    public void SetStaffMemberRole(StaffMemberRole smRole) =>
        Staff.FirstOrDefault(s => s.StaffMember == smRole.StaffMember)?
             .SetIsLead(smRole.IsLead);

    public bool Exists(Partner partner) =>
        this.Partners.Any(p => p == partner);
    public bool Exists(Group group) =>
        this.Groups.Any(g => g == group);

    public bool Exists(Participant participant) =>
        this.Participants.Any(p => p == participant);

    public bool Exists(AnonymousParticipantGroup anonymousParticipantGroup) =>
       this.AnonymousParticipantGroups.Any(apg => apg == anonymousParticipantGroup);

    public bool Exists(StaffMember other) =>
         this.Staff.Any(smp => smp.StaffMember == other);
    public void AddRange(IEnumerable<Partner> partners) =>
        this.Partners.AddRange(partners);

    public void AddRange(IEnumerable<AnonymousParticipantGroup> apg) =>
        this.AnonymousParticipantGroups.AddRange(apg);

    public void AddRange(IEnumerable<Participant> participants) =>
     this.Participants.AddRange(participants);


    public void AddRange(IEnumerable<ActivityGroup> group) =>
        this.Groups.AddRange(group);
    public void AddRange(IEnumerable<StaffMemberRole> staff) =>
       this.Staff.AddRange(staff.Select(sm => StaffMemberActivity.Create(sm.StaffMember, this.Id, sm.IsLead)));
    public void RemoveRange(IEnumerable<Partner> partners)
    {
        foreach (var partner in partners)
            this.Partners.Remove(partner);
    }

    public void RemoveRange(IEnumerable<AnonymousParticipantGroup> anonymousParticipantsGroups)
    {
        foreach (var anonymousParticipantsGroup in anonymousParticipantsGroups)
            this.AnonymousParticipantGroups.Remove(anonymousParticipantsGroup);
    }
    public void RemoveRange(IEnumerable<Participant> participants)
    {
        foreach (var participant in participants)
            this.Participants.Remove(participant);
    }

    public void RemoveRange(IEnumerable<ActivityGroup> groups)
    {
        foreach (var group in groups)
            this.Groups.Remove(group);
    }
    public void RemoveRange(IEnumerable<StaffMemberActivity> staffMemberActivities)
    {
        foreach (var staffmemberActivity in staffMemberActivities)
            this.Staff.Remove(staffmemberActivity);
    }


    public List<StaffMemberActivity> Staff { get; set; } = new();    
    
    public List<ActivityGroup> Groups { get; set; } = new();
       
    public List<AnonymousParticipantGroup> AnonymousParticipantGroups { get; set; } = new();

    public List<Partner> Partners { get; set; } = new();

    public List<Participant> Participants { get; set; } = new();
   
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
}


