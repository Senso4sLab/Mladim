using Mladim.Domain.Enums;

namespace Mladim.Domain.Models;

public class OrganizationAttributes : BaseAttibutes
{   
    public string? Address { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public string? WebpageUrl { get; private set; }
    public string? VatNumber { get; private set; }
    public string? RegistrationNumber { get; private set; }
    public string? LogoUrl { get; set; }
    public string? BannerUrl { get; set; }
    public AgeGroups AgeGroups { get; private set; }
    public YouthSectors YouthSectors { get; private set; }
    public OrganizationTypes Types { get; private set; }
    public OrganizationStatus Status { get; private set; }
    public OrganizationFields Fields { get; private set; }
    public OrganizationRegions Regions { get; private set; }
    private OrganizationAttributes() {}    
}
