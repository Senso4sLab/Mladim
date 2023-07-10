namespace Mladim.Domain.Dtos;

public class StaffMemberQueryDto 
{
    public string  Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public bool IsLead { get; set; }
    public int StaffMemberId { get; set; }
}
