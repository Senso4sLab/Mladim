using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizations;

public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<OrganizationDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }
    public GetOrganizationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrganizationDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Organization, bool>> predicate = o => o.AppUsers.Any(au => au.Id == request.AppUserId);
        
        var organizations = await this.UnitOfWork.OrganizationRepository
            .GetAllAsync(new[] { predicate });

        return this.Mapper.Map<IEnumerable<OrganizationDto>>(organizations);
    }
        
}
