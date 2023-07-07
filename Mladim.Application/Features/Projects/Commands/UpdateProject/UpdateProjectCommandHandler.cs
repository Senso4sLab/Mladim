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
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        ArgumentNullException.ThrowIfNull(project);

        this.Mapper.Map(request, project);

        

        project.Partners.Where(p => !request.Partners.Any(pc => pc.Id == p.Id))
           .ToList().ForEach(rp =>
           {
               this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, rp);
               project.Partners.Remove(rp);
           });

        request.Partners.Where(pc => !project.Partners.Any(p => p.Id == pc.Id))
            .Select(apb => this.Mapper.Map<Partner>(apb)).ToList().ForEach(ap =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, ap);
                project.Partners.Add(ap);
            });

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
        project.Staff.Where(sm => !request.Staff.Any(smc => smc.StaffMemberId == sm.StaffMemberId && smc.IsLead == sm.IsLead))
            .ToList().ForEach(rsmp =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Deleted, rsmp);
                project.Staff.Remove(rsmp);
            });
        // add
        request.Staff.Where(smc => !project.Staff.Any(sm => sm.StaffMemberId == smc.StaffMemberId && sm.IsLead == smc.IsLead))
             .Select(smc => this.Mapper.Map<StaffMemberProject>(smc)).ToList().ForEach(asmc =>
             {
                 this.UnitOfWork.ConfigEntityState(EntityState.Added, asmc);
                 project.Staff.Add(asmc); 
             });

        return await this.UnitOfWork.SaveChangesAsync();       
    }
}
