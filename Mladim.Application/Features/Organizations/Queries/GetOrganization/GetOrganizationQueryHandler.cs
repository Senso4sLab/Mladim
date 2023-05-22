using AutoMapper;
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

public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, OrganizationDto>
{
    private IUnitOfWork UnitOfWork { get; }
    private IMapper Mapper { get; }

    public GetOrganizationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;    
    }   

    public async Task<OrganizationDto> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);    

        if (organization == null)
            throw new Exception();

        return this.Mapper.Map<OrganizationDto>(organization);  
    }
}
