using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.AddProject;

public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, ProjectDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public AddProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper )
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }    

    public async Task<ProjectDto> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository.GetByIdAsync(request.OrganizationId);

        if (organization == null)
            throw new Exception();

        var project = this.Mapper.Map<Project>(request);

        if (project == null)
            throw new Exception();

        this.UnitOfWork.ConfigEntityState(project.Partners, EntityState.Unchanged);
        this.UnitOfWork.ConfigEntityState(project.ProjectMembers, EntityState.Unchanged);

        organization.Projects.Add(project); 

        await this.UnitOfWork.SaveChangesAsync();
       
        return this.Mapper.Map<ProjectDto>(project);
    }
}
