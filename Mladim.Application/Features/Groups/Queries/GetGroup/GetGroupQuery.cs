using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Queries.GetGroup;

public class GetGroupQuery : IRequest<GroupDetailsQueryDto>
{
    public int GroupId { get; set; }
}

public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDetailsQueryDto>
{
    public IMapper Mapper { get; }

    public IUnitOfWork UnitOfWork { get; }

    public GetGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) => 
        (Mapper, UnitOfWork) = (mapper, unitOfWork);
   
    public async Task<GroupDetailsQueryDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await this.UnitOfWork.GroupRepository.FindAsync(request.GroupId);

        ArgumentNullException.ThrowIfNull(group);

        
    }
}
