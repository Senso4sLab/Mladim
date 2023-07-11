using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, int>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }

    public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }
    public async Task<int> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await this.UnitOfWork.GroupRepository.GetGroupDetailsAsync(request.GroupId);

        ArgumentNullException.ThrowIfNull(group);

        group = this.Mapper.Map(request, group);

        this.UnitOfWork.GroupRepository.Update(group);

        return await this.UnitOfWork.SaveChangesAsync();
    }
}
