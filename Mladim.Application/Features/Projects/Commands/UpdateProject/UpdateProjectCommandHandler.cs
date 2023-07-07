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
        var project = await this.UnitOfWork.ProjectRepository.GetProjectDetailsAsync(request.Id);            

        ArgumentNullException.ThrowIfNull(project);
      
        project.PeriodOfImplementation(request.Start, request.End);
        project.SetBaseAttributes(request.Name, request.Description, request.WebpageUrl);

        // Add Partners
        var partnersToAdd = request.Partners.Where(p => !project.ExistsPartner(p.Id))
            .Select(p => Partner.Create(p.Id))
            .ToList();
        
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, partnersToAdd);
        project.AddPartners(partnersToAdd);

        // Remove Partners
        project.RemovePartnersIfNotExistIn(request.Partners);


        // Add Groups
        var partnersToAdd = request.Partners.Where(p => !project.ExistsPartner(p.Id))
            .Select(p => Partner.Create(p.Id))
            .ToList();

        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, partnersToAdd);
        project.AddPartners(partnersToAdd);

        // Remove Groups
        project.RemovePartnersIfNotExistIn(request.Partners);



        project.Groups.Where(g => !request.Groups.Any(gc => gc.Id == g.Id))
            .ToList().ForEach(rg =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, rg);
                project.Groups.Remove(rg);
            });
       
        request.Groups.Where(gc => !project.Groups.Any(g => g.Id == gc.Id))
            .Select(gc => this.Mapper.Map<ProjectGroup>(gc)).ToList().ForEach(ag =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, ag);
                project.Groups.Add(ag);
            });

        // delete
        project.Staff.Where(sm => !request.Staff.Any(smc => smc.StaffMemberId == sm.Id))
            .ToList().ForEach(rsmp =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Deleted, rsmp);
                project.Staff.Remove(rsmp);
            });
        // add
        request.Staff.Where(smc => !project.Staff.Any(sm => sm.Id == smc.StaffMemberId))
             .Select(smc => this.Mapper.Map<StaffMemberProject>(smc)).ToList().ForEach(asmc =>
             {
                 this.UnitOfWork.ConfigEntityState(EntityState.Added, asmc);
                 project.Staff.Add(asmc); 
             });

        return await this.UnitOfWork.SaveChangesAsync();       
    }
}
