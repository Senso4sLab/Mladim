using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Dtos.Members;
using MudBlazor;

namespace Mladim.Client.MappingProfiles.Profiles.Other;

public class Other : Profile
{
    public Other()
    {
        CreateMap<SocialMediaUrlsVM, SocialMediaUrlsCommandDto>();
        CreateMap<SocialMediaUrlsQueryDto, SocialMediaUrlsVM>();

        CreateMap<DateTimeRangeVM, DateTimeRangeCommandDto>();
        CreateMap<DateTimeRangeQueryDto, DateTimeRangeVM>();

        

        CreateMap<NamedEntityDto, NamedEntityVM>()
            .ReverseMap();
    }
}
