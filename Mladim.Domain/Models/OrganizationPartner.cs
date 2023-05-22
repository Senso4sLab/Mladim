namespace Mladim.Domain.Models;

public class OrganizationPartner
{
    public int PartnerId { get; set; }
    public Partner Partner { get; set; }

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}
