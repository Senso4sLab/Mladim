﻿using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Contracts;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class StaffMemberRepository : GenericRepository<StaffMember>, IStaffMemberRepository
{    
    public StaffMemberRepository(ApplicationDbContext context) : base(context) {}     

    public async Task<IEnumerable<IFullName>> GetStaffMemberByFullNameAsync(Expression<Func<StaffMember, bool>> predicate) =>
      await DbSet.Where(predicate).Select(p => p as IFullName).ToListAsync();


}
