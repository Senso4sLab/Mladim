using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsByOrganizationQueryHandler : IRequestHandler<GetProjectsByOrganizationQuery, IEnumerable<ProjectDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public GetProjectsByOrganizationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }
   

    public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var projects =  await this.UnitOfWork.ProjectRepository.GetAllAsync(p => p.OrganizationId == request.OrganizationId);
        return this.Mapper.Map<IEnumerable<ProjectDto>>(projects);
    }
}
