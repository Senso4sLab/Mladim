namespace Mladim.Domain.Models;

public class MemberActivity
{
    public int? Id { get; set; }
    public bool IsLead { get; set; }

    public int ActivityId { get; set; }
    public Activity Activity { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; }
}
