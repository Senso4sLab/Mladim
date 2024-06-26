﻿using Microsoft.EntityFrameworkCore;
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
    public IPartnerRepository PartnerRepository => 
        partnerRepository ??= new PartnerRepository(this.Context);
    public IParticipantRepository ParticipantRepository =>
        participantRepository ??= new ParticipantRepository(this.Context);

    public IAnonymousParticipantRepository AnonymousParticipantRepository =>
        anonymousParticipantRepository ??= new AnonymousParticipantRepository(this.Context);
    public IAppUserRepository AppUserRepository { get; }

    public ISurveyQuestionRepository SurveyQuestionRepository => 
        surveyQuestionRepository ??= new SurveyQuestionRepository(this.Context);   

    public ISurveyResponseRepository SurveyResponseRepository =>
        surveyResponseRepository ??= new SurveyResponseRepository(this.Context);

    private IOrganizationRepository organizationRepository;
    private IActivityRepository activityRepository;
    private IProjectRepository projectRepository;  
    private IStaffMemberRepository memberRepository;
    private IGroupRepository groupRepository;
    private IParticipantRepository participantRepository;
    private IPartnerRepository partnerRepository;
    private IAnonymousParticipantRepository anonymousParticipantRepository;
    private ISurveyQuestionRepository surveyQuestionRepository;
    private ISurveyResponseRepository surveyResponseRepository;

    public UnitOfWork(IAppUserRepository appUserRepository, ApplicationDbContext context)
    {
        this.Context = context;
        this.AppUserRepository = appUserRepository;        
    }    

    public async Task<int> SaveChangesAsync() =>
        await this.Context.SaveChangesAsync();   

    public void ConfigEntitiesState<T>(EntityState state, IEnumerable<T> entities) where T : class
    {
        foreach (var entity in entities)
            this.Context.Entry(entity).State = state;        
    }

    public void ConfigEntityState<T>(EntityState state, T entity) where T : class
    {       
        this.Context.Entry(entity).State = state;
    }

    public void Dispose() =>    
       this.Context.Dispose();  
    
}