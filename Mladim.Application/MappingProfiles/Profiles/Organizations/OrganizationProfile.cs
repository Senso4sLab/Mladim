using AutoMapper;
using Mladim.Application.Features.Organizations.Commands.AddOrganization;
using Mladim.Application.Features.Organizations.Commands.UpdateOrganization;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Organizations;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<AddOrganizationCommand, Organization>();             
        CreateMap<UpdateOrganizationCommand, Organization>();
        CreateMap<Organization, OrganizationQueryDto>();
    }
}
