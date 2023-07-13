using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Queries.GetGroups;

public class GetGroupsQuery : IRequest<IEnumerable<GroupQueryDto>>
{
    public int OrganizationId { get; set; }
}

public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GroupQueryDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }
    public GetGroupsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) => 
        (UnitOfWork, Mapper) = (unitOfWork, mapper);
   
    public async Task<IEnumerable<GroupQueryDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await this.UnitOfWork.GroupRepository.GetAllAsync(g => g.OrganizationId == request.OrganizationId);      

        return groups.Select(g => CreateGroupQueryDto(g)).ToList() ?? Enumerable.Empty<GroupQueryDto>();    
    }

    private GroupQueryDto CreateGroupQueryDto(Group group) =>
        group switch
        {
            ProjectGroup => GroupQueryDto.Create(group.Id, group.FullName, GroupType.Project),
            ActivityGroup => GroupQueryDto.Create(group.Id, group.FullName, GroupType.Project),
            _ => throw new NotImplementedException()
        };   
}
