using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Group;

namespace Mladim.Client.MappingProfiles.Profiles.Groups;

public class Groups : Profile
{
    public Groups()
    {
        CreateMap<NamedEntityVM, GroupCommandDto>().ReverseMap(); // TODO groupType??

        CreateMap<GroupDetailsQueryDto, GroupVM>(); // TODO groupType??        

        CreateMap<GroupQueryDto, GroupVM>();
        CreateMap<GroupQueryDto, NamedEntityVM>();

        CreateMap<GroupVM, AddGroupCommandDto>()
            .ForMember(dest => dest.Members, m => m.MapFrom(src => src.Members.Select(mem => mem.Id)));
        CreateMap<GroupVM, UpdateGroupCommandDto>()
            .ForMember(dest => dest.Members, m => m.MapFrom(src => src.Members.Select(mem => mem.Id)));
    }
}
