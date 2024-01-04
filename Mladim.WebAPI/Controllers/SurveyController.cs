using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mladim.Application.Features.Survey.Commands.AddSurveyResponse;
using Mladim.Application.Features.Survey.Queries.GetSurvey;
using Mladim.Application.Features.Survey.Queries.GetSurveyResponses;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;

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


        [HttpGet("questionnaire")]
        public async Task<ActionResult<IEnumerable<SurveyQuestionQueryDto>>> GetSurvey([FromQuery] GetSurveyQuestionsQuery getSurveyQuery)
        {
            var surveyQuestionnairy = await this.Mediator.Send(getSurveyQuery);            

            return Ok(surveyQuestionnairy);

        }

        [HttpPost("{activityId}")]
        public async Task<ActionResult<bool>> PostSurvey(int activityId, [FromBody] AnonymousSurveyResponseDto surveyResponse)
        {
            var response = await this.Mediator.Send(new AddSurveyResponseCommand { ActivityId = activityId, AnonymousSurveyResponse = surveyResponse, } );

            return Ok(response);

        }



        [Authorize]
        [HttpGet("responses/{activityId}")]
        public async Task<ActionResult<IEnumerable<AnonymousSurveyResponseDto>>> GetSurveyResponses(int activityId)
        {
            var surveyQuestionnairy = await this.Mediator.Send(new GetSurveyResponseQuery() {ActivityId = activityId });

            return Ok(surveyQuestionnairy);

        }
    }
}
