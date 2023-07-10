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

    public void SetIsLead(bool isLead) => 
        this.IsLead = isLead;


    

    private StaffMemberProject(StaffMember staffMember, int projectId, bool lead = false)
    {
        this.StaffMember = staffMember;
        this.ProjectId = projectId;
    }   

    public static StaffMemberProject Create(StaffMember staffMember, int projectId, bool lead) =>
        new StaffMemberProject(staffMember, projectId, lead);
}
