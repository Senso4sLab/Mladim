using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Client.ViewModels;

public class OrganizationVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? WebpageUrl { get; set; }
    public string? VatNumber { get; set; }
    public string? RegistrationNumber { get; set; }
    public IEnumerable<AgeGroups> AgeGroups { get; set; } = new List<AgeGroups>();
    public IEnumerable<YouthSectors> YouthSectors { get; set; } = new List<YouthSectors>();
    public IEnumerable<OrganizationTypes> Types { get; set; } = new List<OrganizationTypes>();
    public IEnumerable<OrganizationStatus> Status { get; set; } = new List<OrganizationStatus>();
    public IEnumerable<OrganizationFields> Fields { get; set; } = new List<OrganizationFields>();
    public IEnumerable<OrganizationRegions> Regions { get; set; } = new List<OrganizationRegions>();
    public IEnumerable<ActivityTypes> ActivityTypes { get; set; } = new List<ActivityTypes>();

    public SocialMediaUrlsVM SocialMediaUrls { get; set;} = new SocialMediaUrlsVM();
}
