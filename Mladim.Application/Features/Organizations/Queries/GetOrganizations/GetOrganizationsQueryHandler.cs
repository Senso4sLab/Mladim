using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizations;

public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<OrganizationQueryDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }
    public GetOrganizationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrganizationQueryDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    { 
        var organizations = await this.UnitOfWork.OrganizationRepository
                .GetAllAsync(o => o.AppUsers.Any(a => a.Id == request.AppUserId), false);

        return organizations == null ? Enumerable.Empty<OrganizationQueryDto>() :
            this.Mapper.Map<IEnumerable<OrganizationQueryDto>>(organizations);        
    }
        
}
