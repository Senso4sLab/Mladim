using Mladim.Domain.Enums;

namespace Mladim.Application.Features.Organizations.Commands.AddOrganization;

public class OrganizationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string WebpageUrl { get; set; }
    public string VatNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public AgeGroups AgeGroups { get; set; }
    public YouthSectors YouthSectors { get; set; }
    public OrganizationTypes Types { get; set; }
    public OrganizationStatus Status { get; set; }
    public OrganizationFields Fields { get; set; }
    public OrganizationRegions Regions { get; set; }
}