﻿using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class AnonymousParticipantRepository : GenericRepository<AnonymousParticipant>, IAnonymousParticipantRepository
{
    public AnonymousParticipantRepository(ApplicationDbContext context) : base(context)
    {
    }

    
}
