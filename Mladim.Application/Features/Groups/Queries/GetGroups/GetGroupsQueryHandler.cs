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
        try
        {
            var groups = await GetGroupsAsync(request.GroupType, request.IsActive, request.OrganizationId);

            return groups.Select(CreateGroupQueryDto).ToList();
        }
        catch(Exception ex)
        {
            return null;
        }
    }


    public async Task<IEnumerable<Group>> GetGroupsAsync(GroupType groupType, bool isActive, int organizationId)
    {
        if (groupType == GroupType.Project)
            return await this.UnitOfWork.GroupRepository.GetAllGroupsAsync<ProjectGroup>(g => g.IsActive == isActive && g.OrganizationId == organizationId);
        else if(groupType == GroupType.Activity)
            return await this.UnitOfWork.GroupRepository.GetAllGroupsAsync<ActivityGroup>(g => g.IsActive == isActive && g.OrganizationId == organizationId);

        return Enumerable.Empty<Group>();   
    }

    //private Type GroupType(GroupType groupType) => groupType switch
    //    {
    //        Domain.Enums.GroupType.Project => typeof(ProjectGroup),
    //        Domain.Enums.GroupType.Activity => typeof(ActivityGroup),
    //        _ => throw new NotImplementedException(),
    //    };
    


    private GroupQueryDto CreateGroupQueryDto(Group group) =>
        group switch
        {
            ProjectGroup => GroupQueryDto.Create(group.Id, group.FullName, group.Description),
            ActivityGroup => GroupQueryDto.Create(group.Id, group.FullName, group.Description),
            _ => throw new NotImplementedException()
        };   
}
