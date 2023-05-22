using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Organization
{
    public int? Id { get; set; }
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
    
    
    public List<OrganizationPartner> Partners { get; set; } = new();
    public List<OrganizationMember> Members { get; set; } = new();
    public List<OrganizationGroup> Groups { get; set; } = new();

    public List<Project> Projects { get; set; } = new();  
    public List<AppUser> AppUsers { get; set; } = new();

}
