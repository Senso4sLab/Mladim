namespace Mladim.Domain.Models;

public class OrganizationPartner
{
    public int PartnerId { get; set; }
    public Partner Partner { get; set; } = default!;

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;


    private OrganizationPartner()
    {
        
    }

    private OrganizationPartner(Partner partner)
    {
        Partner = partner;
    }
    public static OrganizationPartner Create(Partner partner) => 
        new OrganizationPartner(partner);
   
}
