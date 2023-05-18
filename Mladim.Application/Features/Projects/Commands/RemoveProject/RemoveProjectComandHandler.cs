using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.RemoveProject;

public class RemoveProjectComandHandler : IRequestHandler<RemoveProjectCommand, bool>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
    
    public RemoveProjectComandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   

    public async Task<bool> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository.GetByIdAsync(request.ProjectId);

        if (project == null)
            throw new Exception();

        this.UnitOfWork.ProjectRepository.Remove(project);

        return await UnitOfWork.SaveChangesAsync() > 0;
    }
}
