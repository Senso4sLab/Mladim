namespace Mladim.Domain.Dtos;

public class OrganizationMemberDto
{
    public int MemberId { get; set; }
    public MemberDto Member { get; set; }

    public int OrganizationId { get; set; }
    public OrganizationDto Organization { get; set; }
}
