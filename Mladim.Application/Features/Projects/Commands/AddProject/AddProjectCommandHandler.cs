using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.AddProject;

public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, ProjectQueryDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public AddProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper )
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }    

    public async Task<ProjectQueryDto> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        if (organization == null)
            throw new Exception();

        var project = this.Mapper.Map<Project>(request);

        if (project == null)
            throw new Exception();

        this.UnitOfWork.ConfigEntityState<Partner>(EntityState.Unchanged, project.Partners);
        this.UnitOfWork.ConfigEntityState<ProjectGroup>(EntityState.Unchanged, project.Groups);       

        organization.Projects.Add(project);
     
        await this.UnitOfWork.SaveChangesAsync();
        
        return this.Mapper.Map<ProjectQueryDto>(project);
      

    }
}
