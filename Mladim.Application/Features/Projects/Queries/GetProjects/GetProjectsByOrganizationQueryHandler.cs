using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Expression<Func<Project, bool>> predicate = p => p.OrganizationId == request.OrganizationId;

        var projects =  await this.UnitOfWork.ProjectRepository
            .GetAllAsync(new[] {predicate});
        
        return this.Mapper.Map<IEnumerable<ProjectDto>>(projects);
    }
}
