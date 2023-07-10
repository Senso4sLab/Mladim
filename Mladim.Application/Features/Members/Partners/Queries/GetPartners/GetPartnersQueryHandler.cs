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

public class GetPartnersQueryHandler : IRequestHandler<GetPartnersQuery, IEnumerable<MemberDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    
    public GetPartnersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<IEnumerable<MemberDto>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Member> members = Enumerable.Empty<Member>();

        if (request.ActivityId is int activityId)
            members = await this.UnitOfWork.PartnerRepository.GetPartnersAsync(sm => sm.Activities.Any(mp => mp.Id == activityId), request.WithDetails);

        if (request.ProjectId is int projectId)
            members = await this.UnitOfWork.PartnerRepository.GetPartnersAsync(sm => sm.Projects.Any(mp => mp.Id == projectId), request.WithDetails);

        return this.Mapper.Map<IEnumerable<MemberDto>>(members);
    }
}
