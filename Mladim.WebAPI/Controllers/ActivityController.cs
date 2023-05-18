using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Activities.Commands.AddActivity;
using Mladim.Application.Features.Activities.Commands.RemoveActivity;
using Mladim.Application.Features.Activities.Commands.UpdateActivity;
using Mladim.Application.Features.Activities.Queries.GetActivities;
using Mladim.Application.Features.Activities.Queries.GetActivity;
using Mladim.Domain.Dtos;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private IMediator Mediator { get; }
    public ActivityController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ActivityDto?>> AddAsync(AddActivityCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<int>> UpdateAsync(UpdateActivityCommand request)
    {
        var response = await this.Mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> RemoveAsync(int activity)
    {
        var response = await this.Mediator.Send(new RemoveActivityCommand { ActivityId = activity });
        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto?>> GetAsync(int activityId)
    {
        var response = await this.Mediator.Send(new GetActivityQuery { ActivityId = activityId });
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAllByProjectAsync([FromQuery] int projectId)
    {
        var response = await this.Mediator.Send(new GetActivitiesByProjectQuery { ProjectId = projectId });
        return Ok(response);
    }


}
