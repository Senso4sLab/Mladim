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

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectQueryDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public GetProjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
    }    

    public async Task<ProjectQueryDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId);

        if (project == null)
            throw new Exception();
        try
        {

            return this.Mapper.Map<ProjectQueryDto>(project);
        }
        catch (Exception ex) 
        { 
            string message = ex.Message;
            return null;
        }
    }
}
