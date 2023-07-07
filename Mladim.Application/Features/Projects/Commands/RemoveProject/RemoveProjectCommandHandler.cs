using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.RemoveProject;

public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, bool>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
    
    public RemoveProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   

    public async Task<bool> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId);

        ArgumentNullException.ThrowIfNull(project);

        this.UnitOfWork.ProjectRepository.Remove(project);

        return await UnitOfWork.SaveChangesAsync() > 0;
    }
}
