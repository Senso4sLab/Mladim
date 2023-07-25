using AutoMapper;
using Mladim.Domain.Dtos.AttachedFile;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.AttachedFiles;

public class AttachedFileProfile : Profile
{
    public AttachedFileProfile()
    {
        CreateMap<AttachedFileCommandDto, AttachedFile>();
      

        CreateMap<AttachedFile, AttachedFileQueryDto>();
    }
}
