using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos.Survey.Responses;

namespace Mladim.Application.Features.Survey.Queries.GetSurveyResponses;

public class GetSurveyResponsesQueryHandler : IRequestHandler<GetSurveyResponsesQuery, IEnumerable<AnonymousSurveyResponseDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public GetSurveyResponsesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<AnonymousSurveyResponseDto>> Handle(GetSurveyResponsesQuery request, CancellationToken cancellationToken)
    {
        var responses = await this.UnitOfWork.SurveyResponseRepository.GetAllAsync(sr => sr.ActivityId == request.ActivityId);

        return this.Mapper.Map<IEnumerable<AnonymousSurveyResponseDto>>(responses);
    }
}
