using Mladim.Client.ViewModels;

namespace Mladim.Client.Models;

public record DefaultOrganization(string Name, int Id)
{
    public static DefaultOrganization Create(OrganizationVM organization) =>
        new DefaultOrganization(organization.Attributes.Name, organization.Id);

}
