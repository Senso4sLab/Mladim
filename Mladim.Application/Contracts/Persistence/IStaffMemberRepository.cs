using Mladim.Domain.Models;

namespace Mladim.Application.Contracts.Persistence;

public interface IStaffMemberRepository : IGenericRepository<StaffMember>
{
    //ValueTask<TOut?> GetMemberById<TOut>(object id) where TOut : class;


}