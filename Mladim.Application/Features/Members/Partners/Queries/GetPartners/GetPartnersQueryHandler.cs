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

public class GetPartnersQueryHandler : IRequestHandler<GetPartnersQuery, IEnumerable<MemberDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    
    public GetPartnersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<IEnumerable<IFullName>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
    {
       

        if (request.ActivityId is int activityId)
            return await this.UnitOfWork.PartnerRepository.GetAllAsync(sm => sm.Activities.Any(mp => mp.Id == activityId), false);

        if (request.ProjectId is int projectId)
            members = await this.UnitOfWork.PartnerRepository.GetAllAsync(sm => sm.Projects.Any(mp => mp.Id == projectId), false);

        return this.Mapper.Map<IEnumerable<MemberDto>>(members);
    }
}
