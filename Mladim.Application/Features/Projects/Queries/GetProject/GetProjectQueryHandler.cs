using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjectDetails;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectQueryDetailsDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public GetProjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }    

    public async Task<ProjectQueryDetailsDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .GetProjectDetailsAsync(request.ProjectId, false);               

        ArgumentNullException.ThrowIfNull(project);    
        
        return this.Mapper.Map<ProjectQueryDetailsDto>(project);
       
    }
}
