using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Group;

namespace Mladim.Client.MappingProfiles.Profiles.Groups;

public class Groups : Profile
{
    public Groups()
    {
        CreateMap<NamedEntityVM, GroupCommandDto>(); // TODO groupType??

        CreateMap<GroupDetailsQueryDto, GroupVM>(); // TODO groupType??
        

        CreateMap<GroupQueryDto, GroupVM>();
        CreateMap<GroupVM, AddGroupCommandDto>();
        CreateMap<GroupVM, UpdateGroupCommandDto>();
    }
}
