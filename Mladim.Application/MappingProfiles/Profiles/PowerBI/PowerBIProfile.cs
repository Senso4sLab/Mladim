using AutoMapper;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.PowerBI;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.PowerBI;

public class PowerBIProfile : Profile
{
    public PowerBIProfile()
    {
        CreateMap<EmbeddedReport, EmbeddedReportDto>();
    }
}
