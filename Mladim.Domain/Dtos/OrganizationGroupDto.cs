namespace Mladim.Domain.Dtos;

public class OrganizationGroupDto
{
    public int OrganizationId { get; set; }
    public OrganizationDto Organization { get; set; }

    public int GroupId { get; set; }

    public GroupDto Group { get; set; }
}
