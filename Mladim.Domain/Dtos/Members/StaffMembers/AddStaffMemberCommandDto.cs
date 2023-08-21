using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class AddStaffMemberCommandDto
{
    public int OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public int? YearOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRegistered { get; set; }
    public string Email { get; set; } = string.Empty;
    public ApplicationClaim Claim { get; set; }
}
