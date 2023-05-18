using Mladim.Application.Contract;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class MemberRepository : IMemberRepository
{
    private ApplicationDbContext Context { get; }
    public MemberRepository(ApplicationDbContext context) 
    {
        Context = context;
    }    
       
    public ValueTask<TOut?> GetMemberById<TOut>(object id) where TOut : class =>    
         this.Context.FindAsync<TOut>(id);
    

    
}
