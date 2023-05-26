namespace Mladim.Client.ViewModels;

public record DefaultOrganization(string Name, int Id)
{
    public static DefaultOrganization Create(OrganizationVM organization) =>
        new DefaultOrganization(organization.Name, organization.Id);       
    
}
