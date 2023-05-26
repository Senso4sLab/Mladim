using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
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
        List<Expression<Func<Partner, bool>>> predicates = new List<Expression<Func<Partner, bool>>>();

        predicates.Add(sm => sm.IsActive == request.IsActive);

        if (request.ActivityId != null)
            predicates.Add(sm => sm.Activities.Any(a => a.Id == request.ActivityId));
        else if (request.ProjectId != null)
            predicates.Add(sm => sm.Projects.Any(p => p.Id == request.ProjectId));
        else if (request.OrganizationId != null)
            predicates.Add(sm => sm.OrganizationPartners.Any(op => op.OrganizationId == request.OrganizationId));               

        var partner = await this.UnitOfWork.PartnerRepository
               .GetAllAsync(predicates);

        return this.Mapper.Map<IEnumerable<PartnerDto>>(partner);
    }
}
