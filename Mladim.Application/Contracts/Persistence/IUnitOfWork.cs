using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IOrganizationRepository OrganizationRepository { get; }
    IProjectRepository ProjectRepository { get; }
    IActivityRepository ActivityRepository { get; }
    IAppUserRepository AppUserRepository { get; }
    IStaffMemberRepository StaffMemberRepository { get; }
    IGroupRepository GroupRepository { get; }
    IPartnerRepository PartnerRepository { get; }
    IParticipantRepository ParticipantRepository { get; }

    IAnonymousParticipantRepository AnonymousParticipantRepository { get; }

    //IGenericRepository<T> GetRepository<T>() where T : class;

    
    void ConfigEntityState<T>(EntityState state, T entity) where T : class;
    Task<int> SaveChangesAsync();
}


