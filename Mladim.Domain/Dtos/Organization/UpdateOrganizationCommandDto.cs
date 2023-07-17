using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class UpdateOrganizationCommandDto
{
    public int Id { get; set; }
    public OrganizationAttributesCommandDto Attributes { get; set; } = default!;
    public SocialMediaUrlsCommandDto SocialMediaUrls { get; set; } = default!;
}
