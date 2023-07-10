using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Commands.RemoveActivity;

public class RemoveActivityCommandHandler : IRequestHandler<RemoveActivityCommand, bool>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public RemoveActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
   

    public async Task<bool> Handle(RemoveActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository
            .FirstOrDefaultAsync(a => a.Id == request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);

        this.UnitOfWork.ActivityRepository.Remove(activity);

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }
}
