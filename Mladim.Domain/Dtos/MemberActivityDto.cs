namespace Mladim.Domain.Dtos;

public class MemberActivityDto
{
    public int? Id { get; set; }
    public bool IsLead { get; set; }  

    public int MemberId { get; set; }
    public MemberDto Member { get; set; }
}
