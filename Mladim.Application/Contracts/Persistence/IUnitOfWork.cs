using Microsoft.EntityFrameworkCore;
using System;
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

    //IGenericRepository<T> GetRepository<T>() where T : class;

    void ConfigEntityState<T>(IEnumerable<T> entities, EntityState state);
    Task<int> SaveChangesAsync();
}


