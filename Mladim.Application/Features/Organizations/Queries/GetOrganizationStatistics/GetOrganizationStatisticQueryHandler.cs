using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Organizations.Queries.GetOrganizations;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Dtos.Members.Participants;
using Mladim.Domain.Dtos.Organization;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizationStatistics;

public class GetOrganizationStatisticQueryHandler : IRequestHandler<GetOrganizationStatisticQuery, OrganizationStatisticQueryDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; set; }
    public GetOrganizationStatisticQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
    }
    public async Task<OrganizationStatisticQueryDto> Handle(GetOrganizationStatisticQuery request, CancellationToken cancellationToken)
    {
        var organization  = await this.UnitOfWork.OrganizationRepository.FirstOrDefaultAsync(o => o.Id == request.OrganizationId);
       
        ArgumentNullException.ThrowIfNull(organization);

        if (organization.Attributes.CreatedStamp > request.DateTimeRange.EndDate)
            return OrganizationStatisticQueryDto.Empty;

        var currentDate = DateTime.UtcNow;
        try
        {
            // Projekti          

            var projects = await this.UnitOfWork.ProjectRepository
                .GetAllAsync(p => p.OrganizationId == request.OrganizationId);

            projects = projects.Where(p => p.TimeRange.OverlapWith(request.DateTimeRange));  

            var activeProjects = projects.Where(p => p.TimeRange.IsDateTimeInRange(currentDate))
                .Select(p => NamedEntityDto.Create(p.Id, p.Attributes.Name))
                .ToList();

            var pastProjects = projects.Where(p => !p.TimeRange.IsDateTimeInRange(currentDate))
                .Select(p => NamedEntityDto.Create(p.Id, p.Attributes.Name))
                .ToList();

            // Aktivnosti

            var activities = await this.UnitOfWork.ActivityRepository
              .GetActivitiesWithParticipantsAsync(a => a.Project.OrganizationId == request.OrganizationId);

            activities = activities.Where(a => a.TimeRange.OverlapWith(request.DateTimeRange));

            var activeActivities = activities.Where(a => a.TimeRange.IsDateTimeInRange(currentDate))
                .Select(a => NamedEntityDto.Create(a.Id, a.Attributes.Name))
                .ToList();

            var pastActivites = activities.Where(a => !a.TimeRange.IsDateTimeInRange(currentDate))
                .Select(a => NamedEntityDto.Create(a.Id, a.Attributes.Name))
                .ToList();

            // št. participantov

            int anonymousParticipants = activities.Sum(a => a.AnonymousParticipantGroups.Sum(apg => apg.Number));

            int individualParticipants = activities.Sum(a => a.Participants.Count);

            individualParticipants += activities.Sum(a => a.Groups.Sum(g => g.Members.Count));

            //  participant by gender and age group           

            // št. participantov v groupah
            var participantsInGroups = activities.SelectMany(a => a.Groups.SelectMany(g => g.Members.Select(m => m as Participant))).ToList();

            return OrganizationStatisticQueryDto.Create(activeProjects, pastProjects, activeActivities, pastActivites, individualParticipants,
                anonymousParticipants, ParticipantByGender(activities, participantsInGroups), ParticipantsByAgeGroup(activities, participantsInGroups));
        }
        catch(Exception ex)
        {
            return null;
        }
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
