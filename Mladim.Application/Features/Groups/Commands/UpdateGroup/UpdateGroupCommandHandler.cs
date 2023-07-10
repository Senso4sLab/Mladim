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

    public UpdateGroupCommandHandler(IUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;
    }
    public async Task<int> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await this.UnitOfWork.GroupRepository.FirstOrDefaultAsync(g => g.Id == request.GroupId);

        ArgumentNullException.ThrowIfNull(group);
    }
}
