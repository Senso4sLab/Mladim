using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Members.AnonymousParticipants.Queries.GetAnonymousParticipants;
using Mladim.Application.Features.Members.Participants.Commands.AddParticipant;
using Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipant;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipants;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos;
using Mladim.Application.Features.Members.AnonymousParticipants.Commands.AddAnonymousParticipant;

namespace Mladim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousParticipantController : ControllerBase
    {
        private IMediator Mediator { get; }
        public AnonymousParticipantController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddAsync(AddAnonymousParticipantCommand request)
        {
            var response = await this.Mediator.Send(request);
            return Ok(response);
        }       

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnonymousParticipantQueryDto>>> GetAsync([FromQuery] GetAnonymousParticipantsQuery query)
        {
            var response = await this.Mediator.Send(query);
            return Ok(response);
        }
       
    }
}
