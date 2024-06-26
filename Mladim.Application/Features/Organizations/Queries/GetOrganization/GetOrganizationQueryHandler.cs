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

namespace Mladim.Application.Features.Organizations.Queries.GetOrganization;

public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, OrganizationQueryDto>
{
    private IUnitOfWork UnitOfWork { get; }
    private IMapper Mapper { get; }

    public GetOrganizationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;    
    }   

    public async Task<OrganizationQueryDto> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId,false);

        ArgumentNullException.ThrowIfNull(organization);

        return this.Mapper.Map<OrganizationQueryDto>(organization);  
    }
}
