namespace Mladim.Domain.Models;

public class MemberProject
{
    public int? Id { get; set; }
    public bool IsLead { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; }
}
