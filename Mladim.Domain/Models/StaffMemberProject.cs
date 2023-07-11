namespace Mladim.Domain.Models;


public class StaffMemberProject
{    
    public bool IsLead { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public int StaffMemberId { get; set; }
    public StaffMember StaffMember { get; set; } = default!;
    private StaffMemberProject() { }
    private StaffMemberProject(StaffMember staffMember, Project project, bool lead) =>
       (StaffMember, Project, IsLead) = (staffMember, project, lead);
    public static StaffMemberProject Create(StaffMember staffMember, Project project, bool lead) =>
        new StaffMemberProject(staffMember, project, lead);
}
