﻿using Mladim.Domain.IdentityModels;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IAppUserRepository : IGenericRepository<AppUser>
{

}