using MediatR;
using Mladim.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Commands.DeleteGroup;

internal class DeleteProjectGroupCommandHandler : IRequestHandler<DeleteProjectGroupCommand, bool>
{
    public IUnitOfWork UnitOfWork { get; }

    public DeleteProjectGroupCommandHandler(IUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteProjectGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await this.UnitOfWork.GroupRepository.FirstOrDefaultAsync(g => g.Id == request.GroupId);

        ArgumentNullException.ThrowIfNull(group);

        this.UnitOfWork.GroupRepository.Remove(group);

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }
}
