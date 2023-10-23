using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Members.AnonymousParticipants.Queries.GetAnonymousParticipants;
using Mladim.Application.Features.Members.Participants.Commands.AddParticipant;
using Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipant;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipants;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParticipantController : ControllerBase
    {
        private IMediator Mediator { get; }
        public ParticipantController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ParticipantDetailsQueryDto?>> AddAsync(AddParticipantCommand request)
        {
            var response = await this.Mediator.Send(request);
            return Ok(response);
        }


        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(UpdateParticipantCommand request)
        {
            var response = await this.Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NamedEntityDto>>> GetAsync([FromQuery] GetParticipantsQuery query)
        {
            var response = await this.Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("anonymous")]
        public async Task<ActionResult<IEnumerable<AnonymousParticipantGroupQueryDto>>> GetAsync()
        {
            var response = await this.Mediator.Send(new GetAnonymousParticipantsQuery());
            return Ok(response);
        }


        [HttpGet("{memId}")]
        public async Task<ActionResult<ParticipantDetailsQueryDto?>> GetAsync(int memId)
        {
            var response = await this.Mediator.Send(new GetParticipantQuery { Id = memId });
            return Ok(response);
        }
    }
}
