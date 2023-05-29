using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Members.Partners.Commands.AddPartner;
using Mladim.Application.Features.Members.Partners.Queries.GetPartner;
using Mladim.Application.Features.Members.Partners.Queries.GetPartners;
using Mladim.Application.Features.Members.StaffMembers.Commands.UpdatePartner;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private IMediator Mediator { get; }
        public PartnerController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<PartnerQueryDetailsDto?>> AddAsync(AddPartnerCommand request)
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
        public async Task<ActionResult<IEnumerable<MemberBase>>> GetAsync([FromQuery] GetPartnersQuery query)
        {
            var response = await this.Mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("{memId}")]
        public async Task<ActionResult<PartnerQueryDetailsDto?>> GetAsync(int memId)
        {
            var response = await this.Mediator.Send(new GetPartnerQuery { PartnerId = memId });
            return Ok(response);
        }
    }
}
