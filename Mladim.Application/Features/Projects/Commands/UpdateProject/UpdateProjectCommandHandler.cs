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
        try
        {
            var projectDb = await this.UnitOfWork.ProjectRepository
                .GetProjectDetailsAsync(request.Id);

            this.UnitOfWork.ConfigEntitiesState(EntityState.Detached, projectDb.Partners);
            this.UnitOfWork.ConfigEntitiesState(EntityState.Detached, projectDb.Groups);



            ArgumentNullException.ThrowIfNull(projectDb);
           
            var uProjectDb = this.Mapper.Map(request, projectDb);



            //foreach (var p in projectDb.Partners)
            //{
            //    if (projectDb.Partners.Any(s => s.Equals(p)))
            //    {
            //        uProjectDb.Partners.Remove(p);
            //    }
            //}   //this.UnitOfWork.ProjectRepository.Update(uProjectDb);


            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, uProjectDb.Partners);
            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, uProjectDb.Groups);

            //foreach (var smp in uProjectDb.Staff)
            //{
            //    if (projectDb.Staff.Any(s => s.Equals(smp)))
            //        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, smp);
            //}

            


            // Add Partners
            //var partnersToAdd = otherPartners
            //    .Where(p => !project.Exists(p))
            //    .ToList();

            //this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, partnersToAdd);
            //project.AddPartners(partnersToAdd);

            //// Remove Partners
            //var removePartners = project.Partners
            //    .Where(p => !otherPartners.Any(rp => rp == p))
            //    .ToList();

            //project.RemoveAll(removePartners);

            //// Groups
            //var otherGroups = request.Groups
            //    .Select(other => ProjectGroup.Create(other.Id))
            //    .ToList();

            //// Add Groups
            //var groupsToAdd = otherGroups
            //    .Where(p => !project.Exists(p))            
            //    .ToList();

            //this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, groupsToAdd);
            //project.AddGroups(groupsToAdd);

            //// Remove Groups
            //var removeGroups = project.Groups
            //    .Where(g => !otherGroups.Any(rg => rg == g))
            //    .ToList();

            //project.RemoveAll(removeGroups);

            //// StaffRole
            //var otherStaff = request.Staff
            //    .Select(other => StaffMemberRole.Create(other.StaffMemberId, other.IsLead))
            //    .ToList();        

            ////Remove Staff
            //var removeStaff =  project.Staff
            //    .Where(smp => !otherStaff.Any(o => o.StaffMember == smp.StaffMember))
            //    .ToList();

            //this.UnitOfWork.ConfigEntityState(EntityState.Deleted, removeStaff);
            //project.RemoveAll(removeStaff);

            //// Modify StaffMemberProjects              
            //otherStaff.ForEach(project.SetStaffMemberRole); 

            //// Add StaffMemberProjects
            //var addStaffMembers = otherStaff            
            //   .Where(sm => !project.Exists(sm.StaffMember))
            //   .ToList();        

            //project.AddStaff(addStaffMembers);    

            return await this.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}
