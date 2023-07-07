﻿using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<Project?> FirstOrDefaultWithoutIncludeAsync(Expression<Func<Project, bool>> predicate, bool tracking = true);
    Task<IEnumerable<ActivityWithProjectName>> GetActivitiesWithProjectNameByOrganizationId(int organizationId);

    Task<Project?> GetProjectDetailsAsync(int projectId, bool tracking = true);
}