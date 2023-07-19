using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }    

    public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .GetProjectDetailsAsync(request.Id);

        ArgumentNullException.ThrowIfNull(project);

        project = this.Mapper.Map(request, project);

        var partner = this.Mapper.Map<IEnumerable<Partner>>(request.Partners);
        project.Partners.RemoveAll(p => !partner.Any(rp => rp.Equals(p)));

        var addPartner = partner.Where(rp => !project.Partners.Any(p => p.Equals(rp)));
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addPartner);
        project.Partners.AddRange(addPartner);

        var group = this.Mapper.Map<IEnumerable<ProjectGroup>>(request.Groups);
        project.Groups.RemoveAll(g => !group.Any(rp => rp.Equals(g)));

        var addGroup = group.Where(rg => !project.Groups.Any(g => g.Equals(rg)));
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addPartner);
        project.Groups.AddRange(addGroup);

        return await this.UnitOfWork.SaveChangesAsync();      
    }
}
