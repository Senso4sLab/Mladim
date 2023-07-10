﻿using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivity;

public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityQueryDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetActivityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<ActivityQueryDto> Handle(GetActivityQuery request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository
            .FirstOrDefaultAsync(a => a.Id == request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);       

        return this.Mapper.Map<ActivityQueryDto>(activity);
    }
}
