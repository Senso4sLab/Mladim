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

    public AgeGroups AgeGroups { get; private set; }
    public YouthSectors YouthSectors { get; private set; }
    public OrganizationTypes Types { get; private set; }
    public OrganizationStatus Status { get; private set; }
    public OrganizationFields Fields { get; private set; }
    public OrganizationRegions Regions { get; private set; }

    private OrganizationAttributes()
    {
        
    }
    private OrganizationAttributes(string name,
                                      string description,
                                      string? address,
                                      string? phoneNumber,
                                      string? email,
                                      string? webpageUrl,
                                      string? vatNumber,
                                      string? registrationNumber,
                                      AgeGroups ageGroups,
                                      YouthSectors youthSectors,
                                      OrganizationTypes types,
                                      OrganizationStatus status,
                                      OrganizationFields fields,
                                      OrganizationRegions regions)
    {
        Name = name;
        Description = description;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        WebpageUrl = webpageUrl;
        VatNumber = vatNumber;
        RegistrationNumber = registrationNumber;
        AgeGroups = ageGroups;
        YouthSectors = youthSectors;
        Types = types;
        Status = status;
        Fields = fields;
        Regions = regions;
    }

    public static OrganizationAttributes Create(string name, 
                                      string description, 
                                      string? address,
                                      string? phoneNumber,
                                      string? email,
                                      string? webpageUrl,
                                      string? vatNumber,
                                      string? registrationNumber,
                                      AgeGroups ageGroups,
                                      YouthSectors youthSectors,
                                      OrganizationTypes types,
                                      OrganizationStatus status,
                                      OrganizationFields fields,
                                      OrganizationRegions regions) =>
        new OrganizationAttributes(name, description, address, phoneNumber,email, webpageUrl,vatNumber, registrationNumber, ageGroups, youthSectors, types, status, fields, regions);
}
