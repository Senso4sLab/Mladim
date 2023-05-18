using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjectDetails;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public GetProjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }    

    public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
       var project= await this.UnitOfWork.ProjectRepository
            .GetByIdAsync(request.ProjectId, p => p.ProjectMembers, p => p.Partners);

        if (project == null)
            throw new Exception();

        return this.Mapper.Map<ProjectDto>(project);
    }
}
