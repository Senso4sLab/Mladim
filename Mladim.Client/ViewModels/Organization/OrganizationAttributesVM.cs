using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels.Organization;

public class OrganizationAttributesVM
{
    public string Name { get;  set; } = string.Empty;
    public string Description { get;  set; } = string.Empty;
    public string? Address { get;  set; }
    public string? PhoneNumber { get;  set; }
    public string? Email { get;  set; }
    public string? WebpageUrl { get;  set; }
    public string? VatNumber { get;  set; }
    public string? RegistrationNumber { get;  set; }
    public string? LogoUrl { get; set; }
    public string? BannerUrl { get;set; }

    public DateTime CreatedStamp { get; set; } = DateTime.UtcNow;
    public IEnumerable<AgeGroups> AgeGroups { get; set; } = new List<AgeGroups>();

    public IEnumerable<OrganizationNPMAims> NPMAims { get; set; } = new List<OrganizationNPMAims>();
    public IEnumerable<YouthSectors> YouthSectors { get; set; } = new List<YouthSectors>();
    public IEnumerable<OrganizationTypes> Types { get; set; } = new List<OrganizationTypes>();
    public IEnumerable<OrganizationStatus> Status { get; set; } = new List<OrganizationStatus>();
    public IEnumerable<OrganizationFields> Fields { get; set; } = new List<OrganizationFields>();
    public IEnumerable<OrganizationRegions> Regions { get; set; } = new List<OrganizationRegions>();
 

}
