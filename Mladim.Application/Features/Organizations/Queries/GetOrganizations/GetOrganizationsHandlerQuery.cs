using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizations;

public class GetOrganizationsHandlerQuery : IRequestHandler<GetOrganizationsQuery, IEnumerable<OrganizationDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }
    public GetOrganizationsHandlerQuery(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrganizationDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var organizations = await this.UnitOfWork.OrganizationRepository
            .GetAllAsync(o => o.AppUsers.Any(au => au.Id == request.AppUserId));

        if(organizations == null)
            return Enumerable.Empty<OrganizationDto>();

        return this.Mapper.Map<IEnumerable<OrganizationDto>>(organizations);
    }
        
}
