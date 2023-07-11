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
    public DateTimeRange TimeRange { get; protected set; } = default!;
   
    protected Activity() {}

    private Activity(DateTimeRange timeRange, ActivityAttributes attributes) =>
        (Attributes, TimeRange) = (attributes, TimeRange);
       

    //public bool Exists(Partner partner) =>
    //    this.Partners.Any(p => p == partner);
    //public bool Exists(Group group) =>
    //    this.Groups.Any(g => g == group);

    //public bool Exists(Participant participant) =>
    //    this.Participants.Any(p => p == participant);

    //public bool Exists(AnonymousParticipantGroup anonymousParticipantGroup) =>
    //   this.AnonymousParticipantGroups.Any(apg => apg == anonymousParticipantGroup);

    //public bool Exists(StaffMember other) =>
    //     this.Staff.Any(smp => smp.StaffMember == other);
    public void Add(Partner partner) =>
        this.Partners.Add(partner);

    public void Add(AnonymousParticipantGroup apg) =>
        this.AnonymousParticipantGroups.Add(apg);

    public void Add(Participant participant) =>
     this.Participants.Add(participant);

    public void Add(ActivityGroup group) =>
        this.Groups.Add(group);
    public void Add(StaffMemberRole sm) =>
       this.Staff.Add(StaffMemberActivity.Create(sm.StaffMember, this, sm.IsLead));
    //public void RemoveRange(IEnumerable<Partner> partners)
    //{
    //    foreach (var partner in partners)
    //        this.Partners.Remove(partner);
    //}

    //public void RemoveRange(IEnumerable<AnonymousParticipantGroup> anonymousParticipantsGroups)
    //{
    //    foreach (var anonymousParticipantsGroup in anonymousParticipantsGroups)
    //        this.AnonymousParticipantGroups.Remove(anonymousParticipantsGroup);
    //}
    //public void RemoveRange(IEnumerable<Participant> participants)
    //{
    //    foreach (var participant in participants)
    //        this.Participants.Remove(participant);
    //}

    //public void RemoveRange(IEnumerable<ActivityGroup> groups)
    //{
    //    foreach (var group in groups)
    //        this.Groups.Remove(group);
    //}
    //public void RemoveRange(IEnumerable<StaffMemberActivity> staffMemberActivities)
    //{
    //    foreach (var staffmemberActivity in staffMemberActivities)
    //        this.Staff.Remove(staffmemberActivity);
    //}

    public List<StaffMemberActivity> Staff { get; set; } = new();    
    
    public List<ActivityGroup> Groups { get; set; } = new();
       
    public List<AnonymousParticipantGroup> AnonymousParticipantGroups { get; set; } = new();

    public List<Partner> Partners { get; set; } = new();

    public List<Participant> Participants { get; set; } = new();
   
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
}


