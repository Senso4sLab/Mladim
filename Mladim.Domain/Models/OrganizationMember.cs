namespace Mladim.Domain.Models;

public class OrganizationMember
{
    public int MemberId { get; set; }
    public Member Member { get; set; }

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}
