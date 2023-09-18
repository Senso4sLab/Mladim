using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Projects.Commands.AddProject;
using Mladim.Application.Features.Projects.Commands.RemoveProject;
using Mladim.Application.Features.Projects.Commands.UpdateProject;
using Mladim.Application.Features.Projects.Queries.GetProjectDetails;
using Mladim.Application.Features.Projects.Queries.GetProjects;
using Mladim.Application.Features.Projects.Queries.GetProjectStatistics;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Project;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectController : ControllerBase
{
    private IMediator Mediator { get; }
    public ProjectController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddAsync(AddProjectCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<int>> UpdateAsync(UpdateProjectCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{projectId}")]
    public async Task<ActionResult<bool>> RemoveAsync(int projectId)
    {
       
        var response = await this.Mediator.Send(new RemoveProjectCommand { ProjectId = projectId });
        return Ok(response);
    }


    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectQueryDetailsDto?>> GetAsync(int projectId)
    {
        var response = await this.Mediator.Send(new GetProjectQuery { ProjectId = projectId });
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectQueryDto>>> GetAllByOrganizationAsync([FromQuery] int organizationId)
    {
        var response = await this.Mediator.Send(new GetProjectsByOrganizationQuery { OrganizationId = organizationId });
        return Ok(response);
    }


    [HttpGet("{projectId}/statistics")]
    public async Task<ActionResult<ProjectStatisticsQueryDto?>> GetStatistics(int projectId)
    {
        var query = new GetProjectStatisticQuery { ProjectId = projectId };

        var response = await this.Mediator.Send(query);

        return Ok(response);
    }
}
