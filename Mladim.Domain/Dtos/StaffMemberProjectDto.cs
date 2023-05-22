namespace Mladim.Domain.Dtos;

public class StaffMemberProjectDto
{
    public int? Id { get; set; }
    public bool IsLead { get; set; }
    public int StaffMemberId { get; set; }
    public StaffMemberDto StaffMember { get; set; }
}
