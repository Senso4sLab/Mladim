using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using Mladim.Application.Contracts.Persistence;

using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Partners.Queries.GetPartners;

public class GetPartnersQueryHandler : IRequestHandler<GetPartnersQuery, IEnumerable<NamedEntityDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    
    public GetPartnersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<IEnumerable<NamedEntityDto>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<NamedEntity> members = await GetPartnersAsync(request.ActivityId, request.ProjectId, request.OrganizationId, request.IsMemberAbbreviated);
        return this.Mapper.Map<IEnumerable<NamedEntityDto>>(members);       
    }

    private async Task<IEnumerable<NamedEntity>> GetPartnersAsync(int? activityId, int? projectId, int? organizationId, bool isMemberAbbreviated)
    {
        if (activityId is not null)
            return await this.UnitOfWork.PartnerRepository.GetPartnersAsync(p => p.Activities.Any(a => a.Id == activityId), isMemberAbbreviated);

        if (projectId is not null)
            return  await this.UnitOfWork.PartnerRepository.GetPartnersAsync(p => p.Activities.Any(a => a.ProjectId == projectId), isMemberAbbreviated);

        if (organizationId is not null)
            return  await this.UnitOfWork.PartnerRepository.GetPartnersAsync(p => p.OrganizationId == organizationId, isMemberAbbreviated);

        return Enumerable.Empty<NamedEntity>();
    }
}
