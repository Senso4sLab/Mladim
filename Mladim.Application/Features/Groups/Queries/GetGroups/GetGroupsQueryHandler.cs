using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Application.Features.Groups.Queries.GetGroups;

public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GroupQueryDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }
    public GetGroupsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) => 
        (UnitOfWork, Mapper) = (unitOfWork, mapper);
   
    public async Task<IEnumerable<GroupQueryDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await this.UnitOfWork.GroupRepository.GetAllAsync(g => g.GetType() == GroupType(request.GroupType) && g.IsActive == request.IsActive && g.OrganizationId == request.OrganizationId);      

        return groups.Select(CreateGroupQueryDto).ToList() ?? Enumerable.Empty<GroupQueryDto>();    
    }

    private Type GroupType(GroupType gt) => gt switch
        {
            Domain.Enums.GroupType.Project => typeof(ProjectGroup),
            Domain.Enums.GroupType.Activity => typeof(ActivityGroup),
            _ => throw new NotImplementedException(),
        };
    


    private GroupQueryDto CreateGroupQueryDto(Group group) =>
        group switch
        {
            ProjectGroup => GroupQueryDto.Create(group.Id, group.FullName, group.Description, Domain.Enums.GroupType.Project),
            ActivityGroup => GroupQueryDto.Create(group.Id, group.FullName, group.Description, Domain.Enums.GroupType.Project),
            _ => throw new NotImplementedException()
        };   
}
