using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Organization;

public class OrganizationAttributesVM
{
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public string? Address { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public string? WebpageUrl { get; private set; }
    public string? VatNumber { get; private set; }
    public string? RegistrationNumber { get; private set; }
    public List<AgeGroups> AgeGroups { get; set; } = new();
    public List<YouthSectors> YouthSectors { get; set; } = new();
    public List<OrganizationTypes> Types { get; set; } = new();
    public List<OrganizationStatus> Status { get; set; } = new();
    public List<OrganizationFields> Fields { get; set; } = new();
    public List<OrganizationRegions> Regions { get; set; } = new();
    public List<ActivityTypes> ActivityTypes { get; set; } = new();    

}
