using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mladim.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private IMediator Mediator { get; }
    public GroupController(IMediator mediator)
    {
        this.Mediator = mediator;
    }
}
