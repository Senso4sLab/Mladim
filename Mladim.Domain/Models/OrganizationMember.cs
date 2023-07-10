namespace Mladim.Domain.Models;

public class OrganizationMember
{
    public int MemberId { get; set; }
    public Member Member { get; set; } = default!;

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;

    private OrganizationMember()
    {
        
    }
    private OrganizationMember(Member member)
    {
        this.Member = member;
    }

    public static OrganizationMember Create(Member member) =>
        new OrganizationMember(member);


}
