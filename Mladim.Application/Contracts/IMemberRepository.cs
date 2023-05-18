using Mladim.Domain.Models;

namespace Mladim.Application.Contract;

public interface IMemberRepository
{
    ValueTask<TOut?> GetMemberById<TOut>(object id) where TOut : class;
}