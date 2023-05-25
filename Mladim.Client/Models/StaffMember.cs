namespace Mladim.Client.Models;

public class StaffMember : Member
{
    public string Email { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRegistered { get; set; }
}
