using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.AddGroup;

public class AddActivityGroupCommandHandler : IRequestHandler<AddActivityGroupCommand, bool>
{
    public IMapper Mapper { get; }

    public IUnitOfWork UnitOfWork { get; }

    public AddActivityGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AddActivityGroupCommand request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository.FirstOrDefaultAsync(o => o.Id == request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);

        var group = await this.UnitOfWork.GroupRepository.FirstOrDefaultAsync(o => o.Id == request.GroupId) as ActivityGroup;

        ArgumentNullException.ThrowIfNull(group);

        activity.Add(group);       

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }
}
