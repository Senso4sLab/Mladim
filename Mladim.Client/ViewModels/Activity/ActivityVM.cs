using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using MudBlazor;

namespace Mladim.Client.ViewModels;

public class ActivityVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
    public DateTime End { get; set; } = DateTime.Now;

    public TimeSpan? StartHour { get; set; }
    public TimeSpan? EndHour { get; set; }


    private DateRange activityDateRange;
    public DateRange ActivityDateRange
    {
        get => activityDateRange ??= new DateRange(Start, End);
        set
        {
            activityDateRange = value;
            this.Start = activityDateRange.Start.Value;
            this.End = activityDateRange.End.Value;
        }
    }

    public List<StaffMemberSubjectVM> Staff { get; set; } = new();

    private IEnumerable<MemberBaseVM> leadStaff;
    public IEnumerable<MemberBaseVM> LeadStaff
    {
        get => leadStaff ??= Staff.Where(smp => smp.IsLead)
                                  .Select(smp => new MemberBaseVM { Id = smp.StaffMemberId, Name = smp.Name })
                                  .ToList();

        set => leadStaff = value;
    }

    private IEnumerable<MemberBaseVM> administrators;
    public IEnumerable<MemberBaseVM> Administrators
    {
        get => administrators ??= Staff.Where(smp => !smp.IsLead)
                                  .Select(smp => new MemberBaseVM { Id = smp.StaffMemberId, Name = smp.Name })
                                  .ToList();

        set => administrators = value;
    }

    public IEnumerable<ActivityTypes> ActivityTypes { get; set; } = new List<ActivityTypes>();
    public IEnumerable<MemberBaseVM> Partners { get; set; } = new List<MemberBaseVM>();
    public List<GroupBaseVM> Groups { get; set; } = new();
    public IEnumerable<MemberBaseVM> Participants { get; set; } = new List<MemberBaseVM>();

    public List<AnonymousParticipantsVM> AnonymousParticipantActivities { get; set; } = new();
}
