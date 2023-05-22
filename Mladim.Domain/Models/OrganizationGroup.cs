namespace Mladim.Domain.Models;

public class OrganizationGroup
{
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public int GroupId { get; set; }

    public Group Group { get; set; }
}
