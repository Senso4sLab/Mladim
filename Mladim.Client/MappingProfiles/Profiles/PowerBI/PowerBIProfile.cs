using AutoMapper;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.PowerBI;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.PowerBI;

namespace Mladim.Client.MappingProfiles.Profiles.PowerBI;

public class PowerBIProfile : Profile
{
    public PowerBIProfile()
    {
        CreateMap<EmbeddedReportDto, EmbeddedReportVM>();
    }
}
