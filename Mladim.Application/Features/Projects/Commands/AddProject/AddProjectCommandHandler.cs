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

public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, ProjectQueryDetailsDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public AddProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper )
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }    

    public async Task<ProjectQueryDetailsDto> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var project = this.Mapper.Map<Project>(request);
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, project.Groups);
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, project.Partners);       

        organization.Projects.Add(project);
     
        await this.UnitOfWork.SaveChangesAsync();
        
        return this.Mapper.Map<ProjectQueryDetailsDto>(project);     

    }
}
