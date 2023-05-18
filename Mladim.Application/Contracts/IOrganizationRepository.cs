using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contract;

public interface IOrganizationRepository
{
    Task<Organization?> AddAsync(Organization entry);
    Task<bool> AnyAsync(Expression<Func<Organization, bool>> value);
    Task<IEnumerable<Organization>> GetAllAsync(Expression<Func<Organization, bool>> predicate);
    ValueTask<Organization?> GetByIdAsync(int id);
    void Remove(Organization organization);
    void Update(Organization organization);
}