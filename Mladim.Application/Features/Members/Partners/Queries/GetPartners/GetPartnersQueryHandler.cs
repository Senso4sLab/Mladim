using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Queries.GetPartners;

public class GetPartnersQueryHandler : IRequestHandler<GetPartnersQuery, IEnumerable<PartnerDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    
    public GetPartnersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   

    public async Task<IEnumerable<PartnerDto>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Partner, bool>> predicate = null;

        if (request.ActivityId != null)
            predicate = sm => sm.Activities.Any(a => a.Id == request.ActivityId);
        else if (request.ProjectId != null)
            predicate = sm => sm.Projects.Any(p => p.Id == request.ProjectId);
        else if (request.OrganizationId != null)
            predicate = sm => sm.OrganizationId == request.OrganizationId;

        if (predicate == null)
            return Enumerable.Empty<PartnerDto>();

        var partner = await this.UnitOfWork
                .GetRepository<Partner>().GetAllAsync(predicate);

        return this.Mapper.Map<IEnumerable<PartnerDto>>(partner);
    }
}
