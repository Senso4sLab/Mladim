using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Activities.Commands.AddActivity;
using Mladim.Application.Features.Activities.Commands.RemoveActivity;
using Mladim.Application.Features.Activities.Commands.UpdateActivity;
using Mladim.Application.Features.Activities.Queries.GetActivity;
using Mladim.Application.Features.Files.Commands.AddOrganizationBannerImage;
using Mladim.Application.Features.Files.Commands.AddOrganizationProfileImage;
using Mladim.Application.Features.Files.Queries;
using Mladim.Domain.Dtos;

namespace Mladim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {        
        private IMediator Mediator { get; }
        public FileController(IMediator mediator)
        {
            this.Mediator = mediator;
        }       

        [HttpGet]
        public async Task<IActionResult> DownloadFile([FromQuery]GetFileQuery getFileQuery)
        {
            var file = await this.Mediator.Send(getFileQuery);

            if (file != null)
                return File(file.Stream, file.ContentType, file.FileName);

            return NotFound();         
           
        }

        [HttpPost("organizationProfile")]
        public async Task<ActionResult<string>> UploadOrganizationProfile(AddOrganizationImageProfileCommand profile)
        {
            var profileUrl = await this.Mediator.Send(profile);

            return Ok(profileUrl);         
        }

        [HttpPost("organizationBanner")]
        public async Task<ActionResult<string>> UploadOrganizationBanner(AddOrganizationImageBannerCommand banner)
        {
            var url = await this.Mediator.Send(banner);

            return Ok(url);
        }

    }
}
