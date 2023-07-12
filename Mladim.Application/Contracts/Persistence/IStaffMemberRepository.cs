using MediatR;

using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IStaffMemberRepository : IGenericRepository<StaffMember>
{ 
    Task<IEnumerable<NamedEntity>> GetStaffMembersAsync(Expression<Func<StaffMember, bool>> predicate, bool memberAbbreviated);
}