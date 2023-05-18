using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }    

    public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .GetByIdAsync(request.Id, p => p.Partners, p => p.ProjectMembers);

        if (project == null)
            throw new Exception();

        project = this.Mapper.Map(request,project);

        return await this.UnitOfWork.SaveChangesAsync();       
    }
}
