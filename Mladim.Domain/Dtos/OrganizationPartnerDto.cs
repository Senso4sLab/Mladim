namespace Mladim.Domain.Dtos;

public class OrganizationPartnerDto
{
    public int PartnerId { get; set; }
    public PartnerDto Partner { get; set; }

    public int OrganizationId { get; set; }
    public OrganizationDto Organization { get; set; }
}
