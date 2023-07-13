using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;

namespace Mladim.Application.Features.Groups.Queries.GetGroup;

public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDetailsQueryDto>
{
    public IMapper Mapper { get; }

    public IUnitOfWork UnitOfWork { get; }

    public GetGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) => 
        (Mapper, UnitOfWork) = (mapper, unitOfWork);
   
    public async Task<GroupDetailsQueryDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await this.UnitOfWork.GroupRepository.GetGroupDetailsAsync(request.GroupId);       
        ArgumentNullException.ThrowIfNull(group);
        return this.Mapper.Map<GroupDetailsQueryDto>(group);
    }
}
