using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;

namespace Mladim.Client.MappingProfiles.Profiles.AppUsers;

public class AppUsers : Profile
{
    public AppUsers()
    {
        CreateMap<AppUserVM, AppUserCommandDto>();
        CreateMap<AppUserQueryDto, AppUserVM>();
    }
}
