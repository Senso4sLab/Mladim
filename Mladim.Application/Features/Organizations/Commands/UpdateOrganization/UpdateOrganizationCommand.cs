using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommand : IRequest<int>
{
    public int Id { get; set; }
    public OrganizationAttributesCommandDto Attributes { get; set; } = default!;
    public SocialMediaUrlsCommandDto SocialMediaUrls { get; set; } = default!;
}
