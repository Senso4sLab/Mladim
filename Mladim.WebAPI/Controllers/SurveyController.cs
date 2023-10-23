using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Files.Queries;
using Mladim.Application.Features.Survey.Queries.GetSurvey;

namespace Mladim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private IMediator Mediator { get; }

        public SurveyController(IMediator mediator)
        {
            this.Mediator = mediator;   
        }


        [HttpGet]
        public async Task<IActionResult> GetSurvey([FromQuery] GetSurveyQuery getSurveyQuery)
        {
            var surveyQuestionnairy = await this.Mediator.Send(getSurveyQuery);            

            return Ok(surveyQuestionnairy);

        }



    }
}
