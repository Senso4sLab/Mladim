namespace Mladim.Client.ViewModels.Members.StaffMembers;

public class StaffMemberLeadVM : NamedEntityVM
{
    public List<int> ProjectIds { get; set; } = new List<int>();
    public List<int> ActivityIds { get; set; } = new List<int>();
}
