using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.AddOrganization;

public class AddOrganizationCommand : IRequest<OrganizationQueryDto>
{
    public string? AppUserId { get; set; }
    public OrganizationAttributesCommandDto Attributes { get; set; } = default!;   
    public SocialMediaUrlsCommandDto SocialMediaUrls { get; set; } = default!;
}
