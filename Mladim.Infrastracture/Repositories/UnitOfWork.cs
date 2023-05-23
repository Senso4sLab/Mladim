using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public IStaffMemberRepository StaffMemberRepository =>
        memberRepository ??= new StaffMemberRepository(this.Context);
    public IGroupRepository GroupRepository =>
       groupRepository ??= new GroupRepository(this.Context);
    public IAppUserRepository AppUserRepository =>
       appUserRepository ??= new AppUserRepository(this.Context);

    public IPartnerRepository PartnerRepository => 
        partnerRepository ??= new PartnerRepository(this.Context);
    public IParticipantRepository ParticipantRepository =>
        participantRepository ??= new ParticipantRepository(this.Context);

    private IOrganizationRepository organizationRepository;
    private IActivityRepository activityRepository;
    private IProjectRepository projectRepository;
    private IAppUserRepository appUserRepository;
    private IStaffMemberRepository memberRepository;
    private IGroupRepository groupRepository;
    private IParticipantRepository participantRepository;
    private IPartnerRepository partnerRepository;


    //private Hashtable repositories = new Hashtable();


    //public IGenericRepository<T> GetRepository<T>() where T : class
    //{
    //    var name = typeof(T).Name;
        
    //    if(!repositories.ContainsKey(name))
    //    {
    //        var genericType = typeof(GenericRepository<>);
    //        var instance = Activator.CreateInstance(genericType.MakeGenericType(typeof(T)), this.Context);
    //        repositories.Add(name, instance);
    //    }

    //    return (IGenericRepository<T>) repositories[name]!;
    //}

    public UnitOfWork(ApplicationDbContext context)
    {
        this.Context = context; 
        
    }    

    public async Task<int> SaveChangesAsync() =>
        await this.Context.SaveChangesAsync();

    public void ConfigEntityState<T>(EntityState state, IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            ConfigEntityState<T>(state, entity);
    }

    public void ConfigEntityState<T>(EntityState state, T entity)
    {
        this.Context.Entry(entity).State = state;
    }

    public void Dispose() =>    
       this.Context.Dispose();  
    
}