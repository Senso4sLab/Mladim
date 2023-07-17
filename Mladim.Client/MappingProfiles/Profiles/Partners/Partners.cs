using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

namespace Mladim.Client.MappingProfiles.Profiles.Partners;

public class Partners : Profile
{
    public Partners()
    {
        CreateMap<NamedEntityVM, PartnerCommandDto>();
        CreateMap<PartnerQueryDto, NamedEntityVM>();

        CreateMap<PartnerQueryDetailsDto, PartnerVM>();
        CreateMap<PartnerVM, AddPartnerCommandDto>();
        CreateMap<PartnerVM, UpdatePartnerCommandDto>();      
    }
}
