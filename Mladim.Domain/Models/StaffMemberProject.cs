namespace Mladim.Domain.Models;


public class StaffMemberProject : IEquatable<StaffMemberProject>
{    
    public bool IsLead { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public int StaffMemberId { get; set; }
    public StaffMember StaffMember { get; set; } = default!;
    private StaffMemberProject() { }


    public bool Equals(StaffMemberProject? other) =>
        other != null &&
        other.StaffMemberId == this.StaffMemberId &&
        other.ProjectId == this.ProjectId &&
        other.IsLead == this.IsLead;



    public override bool Equals(object? obj) =>
        obj is StaffMemberProject smp ? smp.Equals(this) : false;
  

    public override int GetHashCode() =>    
        HashCode.Combine(ProjectId, StaffMemberId, IsLead);
    





    //private StaffMemberProject(StaffMember staffMember, Project project, bool lead) =>
    //   (StaffMember, Project, IsLead) = (staffMember, project, lead);
    //public static StaffMemberProject Create(StaffMember staffMember, Project project, bool lead) =>
    //    new StaffMemberProject(staffMember, project, lead);
}
