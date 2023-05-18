using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts;

public interface IUnitOfWork : IDisposable
{    
    IOrganizationRepository OrganizationRepository { get; }
    IProjectRepository ProjectRepository { get; }
    IActivityRepository ActivityRepository { get; }
    IAppUserRepository AppUserRepository { get; }
    IMemberRepository MemberRepository { get; }
    IGroupRepository GroupRepository { get; }

    void ConfigEntityState<T>(IEnumerable<T> entities, EntityState state);
    Task<int> SaveChangesAsync();
}


