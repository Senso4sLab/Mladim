namespace Mladim.Domain.Models;

public class StaffMemberProject
{    
    public bool IsLead { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public int StaffMemberId { get; set; }
    public StaffMember StaffMember { get; set; } = default!;

    private StaffMemberProject()
    {
        
    }

    private StaffMemberProject (int staffMemberId, int projectId, bool lead = false)
    {
       this.StaffMemberId = staffMemberId;
       this.ProjectId = projectId; 
    }

    public static StaffMemberProject Create(int staffMemberId, int projectId, bool lead) =>
          new StaffMemberProject(staffMemberId, projectId, lead);
}
