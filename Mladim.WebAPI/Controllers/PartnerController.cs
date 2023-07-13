using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.Partners.Queries.GetPartner;
using Mladim.Application.Features.Members.Partners.Queries.GetPartners;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PartnerController : ControllerBase
    {
        private IMediator Mediator { get; }
        public PartnerController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddAsync(AddPartnerCommand request)
        {
            var response = await this.Mediator.Send(request);
            return Ok(response);
        }


        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(UpdatePartnerCommand request)
        {
            var response = await this.Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NamedEntityDto>>> GetAsync([FromQuery] GetPartnersQuery query)
        {
            var response = await this.Mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("{memId}")]
        public async Task<ActionResult<PartnerQueryDetailsDto?>> GetAsync(int memId)
        {
            var response = await this.Mediator.Send(new GetPartnerQuery { Id = memId });
            return Ok(response);
        }
    }
}
