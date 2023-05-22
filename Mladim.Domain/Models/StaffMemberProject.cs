namespace Mladim.Domain.Models;

public class StaffMemberProject
{    
    public bool IsLead { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public int StaffMemberId { get; set; }
    public StaffMember StaffMember { get; set; }
}
