using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;

public class AddStaffMemberVM
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public int Year { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRegistered { get; set; }
    public string Email { get; set; }
}
