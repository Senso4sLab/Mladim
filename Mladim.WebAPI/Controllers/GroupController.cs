using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupController : ControllerBase
{
    private IMediator Mediator { get; }
    public GroupController(IMediator mediator)
    {
        this.Mediator = mediator;
    }
}
