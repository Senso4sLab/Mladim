namespace Mladim.Domain.Models;

public class StaffMemberActivity
{
    public int ActivityId { get; set; }
    public Activity Activity { get; set; } = default!;
    public bool IsLead { get; set; }
    public int StaffMemberId { get; set; }
    public StaffMember StaffMember { get; set; } = default!;
    private StaffMemberActivity() {} 
    private StaffMemberActivity(StaffMember staffMember, Activity activity, bool lead) =>
        (StaffMember, Activity, IsLead) = (staffMember, activity, lead);
    public static StaffMemberActivity Create(StaffMember staffMember, Activity activity, bool lead) =>
        new StaffMemberActivity(staffMember, activity, lead);
}
