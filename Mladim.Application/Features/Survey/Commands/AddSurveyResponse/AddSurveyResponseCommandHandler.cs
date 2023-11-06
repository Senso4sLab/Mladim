using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Survey.Queries.GetSurvey;
using Mladim.Domain.Dtos.Survey.Questions;
using Mladim.Domain.Dtos.Survey.Responses;
using Mladim.Domain.Models.Survey.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Survey.Commands.AddSurveyResponse;

public class AddSurveyResponseCommandHandler : IRequestHandler<AddSurveyResponseCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public AddSurveyResponseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }    

    public async Task<bool> Handle(AddSurveyResponseCommand request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository.FirstOrDefaultAsync(a => a.Id == request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);

        var anonymousSurveyResponse = this.Mapper.Map<AnonymousSurveyResponse>(request.AnonymousSurveyResponse);

        activity.AnonymousSurveyResponses.Add(anonymousSurveyResponse);

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }
}
