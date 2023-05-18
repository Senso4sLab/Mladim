using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contract;
using Mladim.Application.Contracts;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext Context { get; }
    public IOrganizationRepository OrganizationRepository =>
        organizationRepository ??= new OrganizationRepository(this.Context);
    public IProjectRepository ProjectRepository =>
        projectRepository ??= new ProjectRepository(this.Context);
    public IActivityRepository ActivityRepository =>
       activityRepository ??= new ActivityRepository(this.Context);    
    public IMemberRepository MemberRepository =>
        memberRepository ??= new MemberRepository(this.Context);
    public IGroupRepository GroupRepository =>
       groupRepository ??= new GroupRepository(this.Context);

    public IAppUserRepository AppUserRepository =>
       appUserRepository ??= new AppUserRepository(this.Context);

    
    private IOrganizationRepository organizationRepository;
    private IActivityRepository activityRepository;
    private IProjectRepository projectRepository;
    private IAppUserRepository appUserRepository;
    private IMemberRepository memberRepository;
    private IGroupRepository groupRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        this.Context = context; 
        
    }    

    public async Task<int> SaveChangesAsync() =>
        await this.Context.SaveChangesAsync();

    public void ConfigEntityState<T>(IEnumerable<T> entities, EntityState state)
    {
        foreach (var entity in entities)
            this.Context.Entry(entity).State = state;
    }

    public void Dispose() =>    
       this.Context.Dispose();  
    
}