using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Queries.GetPartners;

public class GetPartnersQueryHandler : IRequestHandler<GetPartnersQuery, IEnumerable<INameableEntity>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    
    public GetPartnersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<IEnumerable<INameableEntity>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<INameableEntity> members = Enumerable.Empty<INameableEntity>();

        if (request.ActivityId is int activityId)
            members = await this.UnitOfWork.PartnerRepository.GetPartnersAsync(p => p.Activities.Any(a => a.Id == activityId), request.IsMemberAbbreviated);

        if (request.ProjectId is int projectId)
            members = await this.UnitOfWork.PartnerRepository.GetPartnersAsync(p => p.Activities.Any(a => a.ProjectId == projectId), request.IsMemberAbbreviated);

        if (request.OrganizationId is int organizationId)
            members = await this.UnitOfWork.PartnerRepository.GetPartnersAsync(p => p.OrganizationId == organizationId, request.IsMemberAbbreviated);

        return this.Mapper.Map<IEnumerable<INameableEntity>>(members);       
    }
}
