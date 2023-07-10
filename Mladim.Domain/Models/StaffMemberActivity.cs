namespace Mladim.Domain.Models;

public class StaffMemberActivity
{    

    public int ActivityId { get; set; }
    public Activity Activity { get; set; } = default!;

    public bool IsLead { get; set; }

    public int StaffMemberId { get; set; }
    public StaffMember StaffMember { get; set; } = default!;

    private StaffMemberActivity()
    {

    }

    public void SetIsLead(bool isLead) =>
        this.IsLead = isLead;


    private StaffMemberActivity(StaffMember staffMember, int activityId, bool lead = false)
    {
        this.StaffMember = staffMember;
        this.ActivityId = activityId;
    }

    public static StaffMemberActivity Create(StaffMember staffMember, int activityId, bool lead) =>
        new StaffMemberActivity(staffMember, activityId, lead);
}
