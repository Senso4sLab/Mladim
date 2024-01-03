using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;

using Mladim.Domain.Dtos.DateTimeRange;
using Mladim.Domain.Dtos.Members.Participants;
using Mladim.Domain.Dtos.Project;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjectStatistics;

public class GetProjectStatisticQueryHandler : IRequestHandler<GetProjectStatisticQuery, ProjectStatisticsQueryDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetProjectStatisticQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public async Task<ProjectStatisticsQueryDto> Handle(GetProjectStatisticQuery request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository.FirstOrDefaultAsync(p => p.Id == request.ProjectId);

        ArgumentNullException.ThrowIfNull(project);

        // Aktivnosti

        var activities = await this.UnitOfWork.ActivityRepository
          .GetActivitiesWithParticipantsAsync(a => a.ProjectId == request.ProjectId);

        // št. participantov v groupah
        var participantsInGroups = activities.SelectMany(a => a.Groups.SelectMany(g => g.Members.Select(m => m as Participant))).ToList();

        var dateTimeRangeQueryDto = this.Mapper.Map<DateTimeRangeQueryDto>(project.TimeRange);

        return ProjectStatisticsQueryDto.Create(project.Id, project.Attributes.Name, dateTimeRangeQueryDto, ParticipantByGender(activities, participantsInGroups), ParticipantsByAgeGroup(activities, participantsInGroups), activities.Count());

    }


    private IEnumerable<ParticipantsGenderDto> ParticipantByGender(IEnumerable<Activity> activities, IEnumerable<Participant> participants)
    {
        List<ParticipantsGenderDto> participantGenderDtos = new List<ParticipantsGenderDto>();

        var participantsGenders = activities.SelectMany(a => a.AnonymousParticipantGroups.Select(spg => ParticipantsGenderDto.Create(spg.AnonymousParticipant.Gender, spg.Number))).ToList();
        participantGenderDtos.AddRange(participantsGenders);

        participantsGenders = activities.SelectMany(a => a.Participants.Select(p => ParticipantsGenderDto.Create(p.Gender))).ToList();
        participantGenderDtos.AddRange(participantsGenders);

        participantsGenders = participants.Select(p => ParticipantsGenderDto.Create(p.Gender)).ToList();
        participantGenderDtos.AddRange(participantsGenders);

        return participantGenderDtos;
    }

    private IEnumerable<ParticipantsAgeGroupDto> ParticipantsByAgeGroup(IEnumerable<Activity> activities, IEnumerable<Participant> participants)
    {
        List<ParticipantsAgeGroupDto> participantAgeGroupDtos = new List<ParticipantsAgeGroupDto>();

        var participantsGenders = activities.SelectMany(a => a.AnonymousParticipantGroups.Select(spg => ParticipantsAgeGroupDto.Create(spg.AnonymousParticipant.AgeGroup, spg.Number))).ToList();
        participantAgeGroupDtos.AddRange(participantsGenders);

        participantsGenders = activities.SelectMany(a => a.Participants.Select(p => ParticipantsAgeGroupDto.Create(p.AgeGroup))).ToList();
        participantAgeGroupDtos.AddRange(participantsGenders);

        participantsGenders = participants.Select(p => ParticipantsAgeGroupDto.Create(p.AgeGroup)).ToList();
        participantAgeGroupDtos.AddRange(participantsGenders);

        return participantAgeGroupDtos;
    }
}
