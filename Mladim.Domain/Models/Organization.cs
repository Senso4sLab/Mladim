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
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? WebpageUrl { get; set; }
    public string? VatNumber { get; set; }
    public string? RegistrationNumber { get; set; }

    public AgeGroups AgeGroups { get; set; }
    public YouthSectors YouthSectors { get; set; }
    public OrganizationTypes Types { get; set; }
    public OrganizationStatus Status { get; set; }
    public OrganizationFields Fields { get; set; }
    public OrganizationRegions Regions { get; set; }

    public SocialMediaUrls SocialMediaUrls { get; set; } = default!;

    private List<OrganizationPartner> partners { get; set; } = new();
    public IEnumerable<Partner> Partners => 
        partners.Select(op => op.Partner);
        
    private List<OrganizationMember> members { get; set; } = new();
    public IEnumerable<Member> Members =>
        members.Select(om => om.Member);

    private List<OrganizationGroup> groups { get; set; } = new();
    public IEnumerable<Group> Groups => 
        groups.Select(groups => groups.Group);

    public List<Project> Projects { get; set; } = new();  
    public List<AppUser> AppUsers { get; set; } = new();

}
