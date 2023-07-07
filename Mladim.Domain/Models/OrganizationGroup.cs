namespace Mladim.Domain.Models;

public class OrganizationGroup
{
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;

    public int GroupId { get; set; }

    public Group Group { get; set; } = default!;
}
