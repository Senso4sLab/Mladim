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

        CreateMap<UpdatePartnerCommand, Partner>();

        CreateMap<Partner, PartnerQueryDetailsDto>()
            .ReverseMap();

        CreateMap<Partner, PartnerQueryDto>();
    }
}
