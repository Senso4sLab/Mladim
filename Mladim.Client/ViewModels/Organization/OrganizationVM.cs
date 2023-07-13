using Mladim.Client.ViewModels.Organization;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Client.ViewModels;

public class OrganizationVM
{
    public int Id { get; set; }
    public SocialMediaUrlsVM SocialMediaUrls { get; set; } = default!;
    public OrganizationAttributesVM Attributes { get; private set; } = default!;    

}
