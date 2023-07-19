using AutoMapper;
using Mladim.Application.Features.Projects.Commands.AddProject;
using Mladim.Application.Features.Projects.Commands.UpdateProject;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.MappingProfiles.Profiles.Projects;

public class ProjectsProfile : Profile
{
    public ProjectsProfile()
    {
        CreateMap<AddProjectCommand, Project>();


        CreateMap<UpdateProjectCommand, Project>()
            .ForMember(dest => dest.Partners, m => m.Ignore())
            .ForMember(dest => dest.Groups, m => m.Ignore());


        CreateMap<Project, ProjectQueryDto>();
        CreateMap<Project, ProjectQueryDetailsDto>();

    }
}
