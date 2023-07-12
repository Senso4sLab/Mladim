
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Persistence;

public interface IParticipantRepository : IGenericRepository<Participant>
{  
    Task<IEnumerable<NamedEntity>> GetParticipantsAsync(Expression<Func<Participant, bool>> predicate, bool memberAbbreviated);
}
