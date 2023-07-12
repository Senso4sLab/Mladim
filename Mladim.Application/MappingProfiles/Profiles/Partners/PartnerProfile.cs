using AutoMapper;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;

using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Partners;

public class PartnerProfile : Profile
{
    public PartnerProfile()
    {
        CreateMap<AddPartnerCommand, Partner>();
        CreateMap<PartnerCommandDto, Partner>();

        //CreateMap<AddPartnerCommand, OrganizationPartner>()
        //    .ForMember(dest => dest.Partner, m => m.MapFrom(src => src));

        CreateMap<UpdatePartnerCommand, Partner>();

        CreateMap<PartnerQueryDetailsDto, Partner>().ReverseMap();

        CreateMap<Partner, PartnerQueryDto>();
    }
}
