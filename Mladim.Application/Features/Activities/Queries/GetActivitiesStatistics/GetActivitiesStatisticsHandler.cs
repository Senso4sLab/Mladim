using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Dtos.Members.Participants;
using Mladim.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivitiesStatistics;

public class GetActivitiesStatisticsHandler : IRequestHandler<GetActivitiesStatisticsQuery, IEnumerable<ActivityStatisticQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public GetActivitiesStatisticsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.UnitOfWork = unitOfWork;
        this.Mapper = mapper;
    }
    public async Task<IEnumerable<ActivityStatisticQueryDto>> Handle(GetActivitiesStatisticsQuery request, CancellationToken cancellationToken)
    {
        List<ActivityStatisticQueryDto> activityStatistics = new List<ActivityStatisticQueryDto>();

        var activities = await this.UnitOfWork.ActivityRepository.GetActivitiesWithProjectNameAndMembers(a => a.Project.OrganizationId == request.OrganizationId && a.TimeRange.StartDate >= request.Start && a.TimeRange.EndDate <= request.End);

        foreach (var activity in activities)
        {
            var participantsInGroups = activity.Groups.SelectMany(g => g.Members.Select(m => m as Participant)).ToList();
            var participantByGender = ParticipantByGender(activity, participantsInGroups);
            var participantByAgeGroup = ParticipantsByAgeGroup(activity, participantsInGroups);
            var activityAttribute = this.Mapper.Map<ActivityAttributesQueryDto>(activity.Attributes);           

            activityStatistics.Add(ActivityStatisticQueryDto.Create(activityAttribute, activity.TimeRange.StartDate, activity.TimeRange.EndDate,  activity.Project.FullName, participantByAgeGroup, participantByGender));
        }
        return activityStatistics;
    }

    private IEnumerable<ParticipantsGenderDto> ParticipantByGender(Activity activity, IEnumerable<Participant> participants)
    {
        List<ParticipantsGenderDto> participantGenderDtos = new List<ParticipantsGenderDto>();

        var participantsGenders = activity.AnonymousParticipantGroups.Select(spg => ParticipantsGenderDto.Create(spg.AnonymousParticipant.Gender, spg.Number)).ToList();
        
        participantGenderDtos.AddRange(participantsGenders);

        participantsGenders = activity.Participants.Select(p => ParticipantsGenderDto.Create(p.Gender)).ToList();
        participantGenderDtos.AddRange(participantsGenders);

        participantsGenders = participants.Select(p => ParticipantsGenderDto.Create(p.Gender)).ToList();
        participantGenderDtos.AddRange(participantsGenders);

        return participantGenderDtos;
    }

    private IEnumerable<ParticipantsAgeGroupDto> ParticipantsByAgeGroup(Activity activity, IEnumerable<Participant> participants)
    {
        List<ParticipantsAgeGroupDto> participantAgeGroupDtos = new List<ParticipantsAgeGroupDto>();

        var participantsGenders = activity.AnonymousParticipantGroups.Select(spg => ParticipantsAgeGroupDto.Create(spg.AnonymousParticipant.AgeGroup, spg.Number)).ToList();
        participantAgeGroupDtos.AddRange(participantsGenders);

        participantsGenders = activity.Participants.Select(p => ParticipantsAgeGroupDto.Create(p.AgeGroup)).ToList();
        participantAgeGroupDtos.AddRange(participantsGenders);

        participantsGenders = participants.Select(p => ParticipantsAgeGroupDto.Create(p.AgeGroup)).ToList();
        participantAgeGroupDtos.AddRange(participantsGenders);

        return participantAgeGroupDtos;
    }
}
