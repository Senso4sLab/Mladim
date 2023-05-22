using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Members.Participants.Commands.AddParticipant;
using Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipant;
using Mladim.Application.Features.Members.Participants.Queries.GetParticipants;
using Mladim.Domain.Dtos;

namespace Mladim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private IMediator Mediator { get; }
        public ParticipantController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ParticipantDto?>> AddAsync(AddParticipantCommand request)
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
        public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetAsync([FromQuery] GetParticipantsQuery query)
        {
            var response = await this.Mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("{memId}")]
        public async Task<ActionResult<ParticipantDto?>> GetAsync(int memId)
        {
            var response = await this.Mediator.Send(new GetParticipantQuery { ParticipantId = memId });
            return Ok(response);
        }
    }
}
