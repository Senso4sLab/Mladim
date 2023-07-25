using AutoMapper;
using Mladim.Client.ViewModels.AttachedFile;
using Mladim.Domain.Dtos.AttachedFile;

namespace Mladim.Client.MappingProfiles.Profiles.AttachedFiles;

public class AttachedFiles : Profile
{
    public AttachedFiles()
    {
        CreateMap<AttachedFileVM, AttachedFileCommandDto>();

        CreateMap<AttachedFileQueryDto, AttachedFileVM>();
    }   
}
