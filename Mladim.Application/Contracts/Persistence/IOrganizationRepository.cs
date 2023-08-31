using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IOrganizationRepository : IGenericRepository<Organization>
{
    Task<bool> IsUserInOrganizationAsync(string userId, int organizationId);
}