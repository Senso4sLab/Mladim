using Mladim.Domain.Contracts;

namespace Mladim.Domain.Dtos;

public class StaffMemberQueryDto : INameableEntity
{   
    public bool IsLead { get; set; }
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}
