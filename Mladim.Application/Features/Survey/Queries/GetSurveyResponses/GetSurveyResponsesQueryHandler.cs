using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos.Survey.Responses;

namespace Mladim.Application.Features.Survey.Queries.GetSurveyResponses;

public class GetSurveyResponseQueryHandler : IRequestHandler<GetSurveyResponseQuery, IEnumerable<AnonymousSurveyResponseDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public GetSurveyResponseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<AnonymousSurveyResponseDto>> Handle(GetSurveyResponseQuery request, CancellationToken cancellationToken)
    {
        var responses = await this.UnitOfWork.SurveyResponseRepository.GetAllAsync(sr => sr.ActivityId == request.ActivityId);

        return this.Mapper.Map<IEnumerable<AnonymousSurveyResponseDto>>(responses);
    }
}
